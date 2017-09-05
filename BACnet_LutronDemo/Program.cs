using BACnet_LutronDemo.Model;
using System;
using System.Collections.Generic;
using System.IO.BACnet;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BACnet_LutronDemo
{
    static class Program
    {
        static BacnetClient moBacnetClient;

        //// Global list for all the present Bacnet Device List
        static BACnetDeviceModel loBACnetDeviceModel = new BACnetDeviceModel();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StartBACnetServerActivity();
            Thread.Sleep(1000); //// Wait a fiew time for WhoIs responses (managed in handler_OnIam)
            InsertBACnetDeviceDetailInDB();

            moBacnetClient.Dispose();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LutronLightFloor());

            
        }


        /// <summary>
        /// Start bacnet client and setup handler
        /// </summary>
        static void StartBACnetServerActivity()
        {
            //// Bacnet on UDP/IP/Ethernet
            moBacnetClient = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, false));
            //// or Bacnet Mstp on COM4 à 38400 bps, own master id 8
            //// m_bacnet_client = new BacnetClient(new BacnetMstpProtocolTransport("COM4", 38400, 8);

            moBacnetClient.Start();  // go

            //// Send WhoIs in order to get back all the Iam responses :  
            moBacnetClient.OnIam += new BacnetClient.IamHandler(handler_OnIam);
            moBacnetClient.WhoIs();
        }


        /// <summary>
        /// handler_OnIam handler to get available device and create global list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="adr"></param>
        /// <param name="device_id"></param>
        /// <param name="max_apdu"></param>
        /// <param name="segmentation"></param>
        /// <param name="vendor_id"></param>
        static void handler_OnIam(BacnetClient sender, BacnetAddress adr, uint device_id, uint max_apdu, BacnetSegmentations segmentation, ushort vendor_id)
        {
            //// OnIam get current device and add into list to process bunch of device in DBs
            lock (loBACnetDeviceModel.loBACnetDeviceList)
            {
               
                if (!loBACnetDeviceModel.loBACnetDeviceList.Any(x => x.inDeviceID == device_id))
                {
                    //// Not already in the list

                    loBACnetDeviceModel.loBACnetDeviceList.Add(new BACnetDeviceNew
                    {
                        loBACnetAddress = adr,
                        inDeviceID = device_id,
                        inInstanceID = 0
                    });   //// add it
                }
            }
        }


        /// <summary>
        /// Get all object & object's instance detail of available devices 
        /// Insert network, device, object & object's instance into DB
        /// </summary>
        static void InsertBACnetDeviceDetailInDB()
        {
            if (loBACnetDeviceModel != null && loBACnetDeviceModel.loBACnetDeviceList.Count > 0)
            {
                using (var loESDLutronEntities = new ESDLutronEntities())
                {
                    //// Declare list to instert device in DB
                    List<BACnetDevice> loInsertBACnetDeviceList = new List<BACnetDevice>();
                    List<BACnetDeviceMapping> loInsertBACnetDeviceMapping = new List<BACnetDeviceMapping>();

                    //// Remove all exists device on each application run
                    loESDLutronEntities.BACnetDevices.RemoveRange(loESDLutronEntities.BACnetDevices.AsEnumerable());
                    loESDLutronEntities.BACnetDeviceMappings.RemoveRange(loESDLutronEntities.BACnetDeviceMappings.AsEnumerable());

                    foreach (var loBacnetDevice in loBACnetDeviceModel.loBACnetDeviceList)
                    {
                        //// Get all bacnet object available for device
                        IList<BacnetValue> loObjectValueList;
                        moBacnetClient.ReadPropertyRequest(loBacnetDevice.loBACnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, loBacnetDevice.inDeviceID), BacnetPropertyIds.PROP_OBJECT_LIST, out loObjectValueList);

                        foreach (BacnetValue loObjectValue in loObjectValueList)
                        {
                            //// Get each object's instance details and object name to store in DB
                            IList<BacnetValue> loObjectNameList;
                            moBacnetClient.ReadPropertyRequest(loBacnetDevice.loBACnetAddress,
                                new BacnetObjectId((BacnetObjectTypes)((BacnetObjectId)loObjectValue.Value).Type, ((BacnetObjectId)loObjectValue.Value).Instance),
                                BacnetPropertyIds.PROP_OBJECT_NAME, out loObjectNameList);

                            //string lsObjectType = ((BacnetObjectId)loObjectValue.Value).type.ToString();
                            //string lsObjectInstanceNumber = ((BacnetObjectId)loObjectValue.Value).Instance.ToString();

                            //// add to entity model for DB bulk insert
                            loInsertBACnetDeviceList.Add(
                            new BACnetDevice
                            {
                                network_id = loBacnetDevice.loBACnetAddress.ToString(),
                                device_id = Convert.ToInt32(loBacnetDevice.inDeviceID),
                                object_type = ((BacnetObjectId)loObjectValue.Value).type.ToString(),
                                object_instance = Convert.ToInt32(((BacnetObjectId)loObjectValue.Value).Instance.ToString()),
                                object_name = loObjectNameList != null && loObjectNameList.Count > 0 ? loObjectNameList[0].Value.ToString() : null,
                                routed_source = loBacnetDevice.loBACnetAddress.RoutedSource.ToString(),
                                routed_net = loBacnetDevice.loBACnetAddress.RoutedSource.net
                            });

                            int? liSuiteID = null, liRoomID = null;

                            if(((BacnetObjectId)loObjectValue.Value).type.ToString().ToUpper() == "OBJECT_ANALOG_VALUE")
                            {
                                if(Convert.ToInt32(((BacnetObjectId)loObjectValue.Value).Instance.ToString()) < 4)
                                {
                                    liSuiteID = 1;
                                }
                                else
                                {
                                    liSuiteID = 2;
                                }

                                liRoomID = Convert.ToInt32(((BacnetObjectId)loObjectValue.Value).Instance.ToString());
                            }

                            loInsertBACnetDeviceMapping.Add(
                            new BACnetDeviceMapping
                            {
                                device_id = Convert.ToInt32(loBacnetDevice.inDeviceID),
                                object_instance = Convert.ToInt32(((BacnetObjectId)loObjectValue.Value).Instance.ToString()),
                                floor_id = Convert.ToInt32(loBacnetDevice.inDeviceID),
                                suite_id = liSuiteID,
                                room_id = liRoomID,
                            });
                        }
                    }

                    //// Insert into DB (table: BACnetDevices)
                    if (loInsertBACnetDeviceList != null && loInsertBACnetDeviceList.Count > 0)
                    {
                        loESDLutronEntities.BACnetDevices.AddRange(loInsertBACnetDeviceList);
                        loESDLutronEntities.BACnetDeviceMappings.AddRange(loInsertBACnetDeviceMapping);
                        loESDLutronEntities.SaveChanges();
                    }
                }
            }
        }
    }
}

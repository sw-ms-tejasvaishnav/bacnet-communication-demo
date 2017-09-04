using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.BACnet;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BACnet_LutronDemo.Common.CommonConstant;

namespace BACnet_LutronDemo
{
    public partial class LutronLight : Form
    {
        static BacnetClient moBacnetClient;

        delegate void SetTextCallback(string fsPresentValue);

        /// <summary>
        /// InIt bacnet and UI
        /// </summary>
        public LutronLight()
        {

            InitializeComponent();

            chkLightOnOff.Appearance = Appearance.Button;
            chkLightOnOff.TextAlign = ContentAlignment.MiddleCenter;
            chkLightOnOff.MinimumSize = new Size(75, 25); //To prevent shrinkage!

            // Bacnet on UDP/IP/Ethernet
            moBacnetClient = new BacnetClient(new BacnetIpUdpProtocolTransport(47808, false));// (0xBAC0, false));
            moBacnetClient.Start();    // go

            //// Set handler for OnCOVNotification
            moBacnetClient.OnCOVNotification += new BacnetClient.COVNotificationHandler(handler_OnCOVNotification);

            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                ddlDeviceList.DataSource = loESDLutronEntities.BACnetDevices.Select(x => new { id = x.device_id, name = x.device_id }).Distinct().OrderBy(x => x.id).ToList();
                ddlDeviceList.ValueMember = "id";
                ddlDeviceList.DisplayMember = "name";
            }

            RegisterCOVForObjectsOfDevice((Int32)ddlDeviceList.SelectedValue);
        }


        /// <summary>
        /// Change Lighting State with checkboc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLightOnOff.Checked)
            {
                chkLightOnOff.Text = "Light on";
                chkLightOnOff.BackColor = Color.Green;
                ToggleLutronLight((Int32)ddlDeviceList.SelectedValue, true);
            }
            else
            {
                chkLightOnOff.Text = "Light off";
                chkLightOnOff.BackColor = Color.Gray;
                ToggleLutronLight((Int32)ddlDeviceList.SelectedValue, false);
            }
        }


        /// <summary>
        /// Toggle Lighting State
        /// </summary>
        /// <param name="fiDeviceID"></param>
        /// <param name="fbToggleStatus"></param>
        private void ToggleLutronLight(int fiDeviceID, bool fbToggleStatus)
        {
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_instance == (int?)LutronObjectType.Lighting_State)
                                             .Select(x => x).FirstOrDefault();

                if (loBACnetDeviceDetail != null && loBACnetDeviceDetail.object_instance != null)
                {
                    BacnetAddress loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                    loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                    IList<BacnetValue> loBacnetValueList;
                    moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, out loBacnetValueList);


                    BacnetValue newValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED, fbToggleStatus == true ? 1 : 0);
                    BacnetValue[] loWriteValue = { newValue };

                    moBacnetClient.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, loWriteValue);

                    //BacnetValue[] loNewBacnetValue = { new BacnetValue(Convert.ToSingle(111)) };

                    ////bacnet_client.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_ACTION, loNewBacnetValue);
                    ////bacnet_client.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_INPUT, 0), BacnetPropertyIds.PROP_PRESENT_VALUE, loNewBacnetValue);
                    //bacnet_client.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_INPUT, 0), BacnetPropertyIds.PROP_PRESENT_VALUE, loNewBacnetValue);
                }
            }

            //bool ret = WriteScalarValue(1, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, 2), BacnetPropertyIds.PROP_PRESENT_VALUE, loNewValue);
        }


        /// <summary>
        /// Mouse up event for Set Lighting Level as per trackbar value between 1 to 100 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarLightingLevel_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeLutronLightLevel((Int32)ddlDeviceList.SelectedValue);
            //lblCurrentLightLevelValue.Text = GetLutronLightLevel((Int32)ddlDeviceList.SelectedValue);
        }


        /// <summary>
        /// Set Lighting Level as per trackbar value between 1 to 100 
        /// </summary>
        /// <param name="fiDeviceID"></param>
        private void ChangeLutronLightLevel(Int32 fiDeviceID)
        {
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_instance == (int?)LutronObjectType.Lighting_Level)
                                             .Select(x => x).FirstOrDefault();


                BacnetAddress loBacnetAddress;
                loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                BacnetValue loNewBacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, Convert.ToSingle(trackBarLightingLevel.Value));
                BacnetValue[] loWriteNewBacnetValueValue = { loNewBacnetValue };
                moBacnetClient.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, loWriteNewBacnetValueValue);
            }
        }


        /// <summary>
        /// Get current lightlign level from simulator will be between 1 to 100
        /// </summary>
        /// <param name="fiDeviceID"></param>
        /// <returns></returns>
        private string GetLutronLightLevel(Int32 fiDeviceID)
        {
            IList<BacnetValue> loBacnetValueList;
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_instance == (int?)LutronObjectType.Lighting_Level)
                                             .Select(x => x).FirstOrDefault();


                BacnetAddress loBacnetAddress;
                loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, out loBacnetValueList);
            }

            if (loBacnetValueList != null && loBacnetValueList.Count > 0)
            {
                return loBacnetValueList[0].Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Read current light level from simulator AV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadLutronLightLevel_Click(object sender, EventArgs e)
        {
            string lsLutronLightLevel = GetLutronLightLevel((Int32)ddlDeviceList.SelectedValue);
            lblCurrentLightLevelValue.Text = lsLutronLightLevel;
            trackBarLightingLevel.Value = Convert.ToInt32(lsLutronLightLevel);
        }


        /// <summary>
        /// Register COV event which we want to get, Here AV is registered for COV notification
        /// </summary>
        /// <param name="fiDeviceID"></param>
        private void RegisterCOVForObjectsOfDevice(Int32 fiDeviceID)
        {
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_instance == (int?)LutronObjectType.Lighting_Level)
                                             .Select(x => x).FirstOrDefault();


                BacnetAddress loBacnetAddress;
                loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                moBacnetClient.SubscribeCOVRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, (uint)loBACnetDeviceDetail.object_instance), 0, false, false, 60);
            }
        }


        /// <summary>
        /// Handler to get COV notification and process further
        /// </summary>
        private void handler_OnCOVNotification(BacnetClient foBacnetClient, BacnetAddress foBacnetAddress, byte foInvokeid,
          uint fisubscriberProcessIdentifier, BacnetObjectId foInitiatingDeviceIdentifier, BacnetObjectId foMonitoredObjectIdentifier,
          uint fiTimeRemaining, bool fbNeedConfirm, ICollection<BacnetPropertyValue> foBacnetPropertyValue, BacnetMaxSegments foBacnetMaxSegments)
        {

            foreach (BacnetPropertyValue value in foBacnetPropertyValue)
            {
                switch ((BacnetPropertyIds)value.property.propertyIdentifier)
                {
                    case BacnetPropertyIds.PROP_PRESENT_VALUE:
                        COVChangesToUI(value.value[0].ToString());
                        break;
                    default:
                        //got something else? ignore it
                        break;
                }
            }
        }


        /// <summary>
        /// Show changes of present value in simulator via COV into the UI
        /// </summary>
        /// <param name="fsCOVPresentValue"></param>
        private void COVChangesToUI(string fsCOVPresentValue)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblCurrentLightLevelValue.InvokeRequired)
            {
                SetTextCallback loSetTextCallback = new SetTextCallback(COVChangesToUI);
                this.Invoke(loSetTextCallback, new object[] { fsCOVPresentValue });
            }
            else
            {
                this.lblCurrentLightLevelValue.Text = fsCOVPresentValue;
                this.trackBarLightingLevel.Value = Convert.ToInt32(fsCOVPresentValue);
            }
        }


        /// <summary>
        /// Temporary method
        /// </summary>
        /// <param name="fiDeviceID"></param>
        /// <param name="foBacnetObjectId"></param>
        /// <param name="foBacnetPropertyIds"></param>
        /// <param name="foBacnetValue"></param>
        /// <returns></returns>
        static bool WriteScalarValue(int fiDeviceID, BacnetObjectId foBacnetObjectId, BacnetPropertyIds foBacnetPropertyIds, BacnetValue foBacnetValue)
        {
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_instance == (int?)LutronObjectType.Lighting_State)
                                             .Select(x => x).FirstOrDefault();


                BacnetAddress adr;

                // Looking for the device
                adr = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                if (adr == null) return false;  // not found
                adr.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                // Property Write


                IList<BacnetValue> NoScalarValue;
                moBacnetClient.ReadPropertyRequest(adr, foBacnetObjectId, foBacnetPropertyIds, out NoScalarValue);

                //var loWirteList = NoScalarValue.ToList();
                //loWirteList[0].Value = 5;

                //NoScalarValue[0].Value = 5;
                //List<BacnetValue> loWriteValue = new List<BacnetValue>();
                //loWriteValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, 5));

                BacnetValue newValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, Convert.ToSingle(foBacnetValue.Value));
                BacnetValue[] loWriteValue = { newValue };

                moBacnetClient.WritePropertyRequest(adr, foBacnetObjectId, foBacnetPropertyIds, loWriteValue);

                return true;

            }
        }

    }
}

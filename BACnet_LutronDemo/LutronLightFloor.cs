using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.BACnet;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BACnet_LutronDemo.Common.CommonConstant;

namespace BACnet_LutronDemo
{
    public partial class LutronLightFloor : Form
    {
        static BacnetClient moBacnetClient;
        private const int liMinLightness = 1;
        private const int liMaxLightness = 100;
        private const float ldMinLightnessCoef = 1f;
        private const float ldMaxLightnessCoef = 0.80f;

        delegate void SetAlarmEventCallback(string fsDeviceID, string fsInstance, string fsEventState);

        #region init

        /// <summary>
        /// InIt bacnet and UI
        /// </summary>
        public LutronLightFloor()
        {
            InitializeComponent();

            //// Bacnet on UDP/IP/Ethernet
            moBacnetClient = new BacnetClient(new BacnetIpUdpProtocolTransport(47808, false));// (0xBAC0, false));

            //// Below 2 are needed for alarm event
            moBacnetClient.OnEventNotify += new BacnetClient.EventNotificationCallbackHandler(handler_OnEventNotify);
            moBacnetClient.OnWhoIs += new BacnetClient.WhoIsHandler(handler_OnWhoIs);

            moBacnetClient.Start();    // go
            moBacnetClient.WhoIs();    // go

            FillDropDown();
            
        }

        /// <summary>
        /// Fill static floor dropdown
        /// </summary>
        public void FillDropDown()
        {
            ddlFloor.Items.Clear();

            Dictionary<string, string> loDictionary = new Dictionary<string, string>();
            loDictionary.Add("1", "First Floor");
            loDictionary.Add("2", "Second Floor");

            ddlFloor.DataSource = new BindingSource(loDictionary, null);
            ddlFloor.DisplayMember = "Value";
            ddlFloor.ValueMember = "Key";
        }

        #endregion


        #region BACnet handler

        /// <summary>
        /// handler to assign alarm enrolment instance on event notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="adr"></param>
        /// <param name="low_limit"></param>
        /// <param name="high_limit"></param>
        static void handler_OnWhoIs(BacnetClient sender, BacnetAddress adr, int low_limit, int high_limit)
        {
            if (low_limit != -1 && 1 < low_limit)
                return;
            else if (high_limit != -1 && 1 > high_limit)
                return;

            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                Int32? liAlarmEnrolment = loESDLutronEntities.BACnetDevices
                                          .Where(x => x.device_id == 1
                                                && x.object_type.ToUpper() == LutronFloorObjectType.ALERT_ENROLLMENT)
                                           .Select(x => x.object_instance).FirstOrDefault();

                if(liAlarmEnrolment != null)
                {
                    sender.Iam((uint)liAlarmEnrolment, new BacnetSegmentations());
                }
            }
        }


        /// <summary>
        /// alarm event handler to perform task basis on event state changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="adr"></param>
        /// <param name="EventData"></param>
        public void handler_OnEventNotify(BacnetClient sender, BacnetAddress adr, BacnetEventNotificationData EventData)
        {
            string val = adr.ToString() + ":" + EventData.initiatingObjectIdentifier.type + ":" + EventData.initiatingObjectIdentifier.instance + ":" + EventData.eventObjectIdentifier.type + ":" + EventData.eventObjectIdentifier.instance;
            Console.WriteLine(val + " " + EventData.fromState + " to " + EventData.toState + " " + EventData.notifyType.ToString());

            string lsDeviceDetail = EventData.initiatingObjectIdentifier.type + ":" + EventData.initiatingObjectIdentifier.instance;
            string lsInstanceDetail = EventData.eventObjectIdentifier.type + ":" + EventData.eventObjectIdentifier.instance;
            string lsEventState = EventData.fromState + " to " + EventData.toState;

            AlarmEventChangesToUI(lsDeviceDetail, lsInstanceDetail, lsEventState);
        }
        #endregion



        #region Light toggle light 1/light 2

        /// <summary>
        /// Get light state (BV) on Light 1 tadio button select/unselect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbLight1_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLight1.Checked)
            {
                bool lbCurrentLightState = GetLutronLightState(rbLight1.Checked ? 1 : 2);

                if (lbCurrentLightState)
                    lblLight1.BackColor = Color.Green;
                else
                    lblLight1.BackColor = Color.Gray;
            }
            else
            {
                lblLight1.BackColor = Color.DarkGray;
            }
        }

        
        /// <summary> 
        /// Get light state (BV) on Light 2 tadio button select/unselect (light state)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbLight2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLight2.Checked)
            {
                bool lbCurrentLightState = GetLutronLightState(rbLight1.Checked ? 1 : 2);

                if (lbCurrentLightState)
                    lblLight2.BackColor = Color.Green;
                else
                    lblLight2.BackColor = Color.Gray;
            }
            else
            {
                lblLight2.BackColor = Color.DarkGray;
            }
        }
      
        
        /// <summary>
        /// toggle BV for device 1, device 2 (light state)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if(rbLight1.Checked || rbLight2.Checked)
            {
                bool lbCurrentLightState = GetLutronLightState(rbLight1.Checked ? 1 : 2);

                if (rbLight1.Checked)
                {
                    if (lbCurrentLightState)
                        lblLight1.BackColor = Color.Green;
                    else
                        lblLight1.BackColor = Color.Gray;
                }
                else
                {
                    if (lbCurrentLightState)
                        lblLight2.BackColor = Color.Green;
                    else
                        lblLight2.BackColor = Color.Gray;
                }


                ToggleLutronLight(rbLight1.Checked ? 1 : 2, lbCurrentLightState == true ? false : true);


                if(rbLight1.Checked)
                {
                    if (!lbCurrentLightState)
                        lblLight1.BackColor = Color.Green;
                    else
                        lblLight1.BackColor = Color.Gray;
                }
                else
                {
                    if (!lbCurrentLightState)
                        lblLight2.BackColor = Color.Green;
                    else
                        lblLight2.BackColor = Color.Gray;
                }
            }
            else
            {
                MessageBox.Show("Please select light");
            }
        }


        /// <summary>
        /// Get current light state for device
        /// </summary>
        /// <param name="fiDeviceID"></param>
        /// <returns></returns>
        private bool GetLutronLightState(Int32 fiDeviceID)
        {
            IList<BacnetValue> loBacnetValueList;
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                            .Where(x => x.device_id == fiDeviceID
                                                  && x.object_type.ToUpper() == LutronFloorObjectType.Lighting_State)
                                             .Select(x => x).FirstOrDefault();


                BacnetAddress loBacnetAddress;
                loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, out loBacnetValueList);
            }

            if (loBacnetValueList != null && loBacnetValueList.Count > 0)
            {
                return Convert.ToBoolean(Convert.ToInt32(loBacnetValueList[0].Value));
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// toggle BV (light state)
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

                    //IList<BacnetValue> loBacnetValueList;
                    //moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, out loBacnetValueList);

                    BacnetValue loBacnetNewValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED, fbToggleStatus == true ? 1 : 0);
                    BacnetValue[] loWriteValue = { loBacnetNewValue };

                    moBacnetClient.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_BINARY_VALUE, (uint)loBACnetDeviceDetail.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, loWriteValue);
                }
            }
        }

        #endregion



        #region Light sense Floor 1/Floor 2

        /// <summary>
        /// Change light level for floor (AV) at UI level only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbkLightSense_Scroll(object sender, EventArgs e)
        {
            lblSensPer.Text = trbkLightSense.Value.ToString();

            if (Convert.ToInt32(ddlFloor.SelectedValue) == (int)BuildingFloor.FirstFloor)
            {
                ChangeLightBrightnessForFirstFloor(Convert.ToInt32(trbkLightSense.Value));
            }
            else
            {
                ChangeLightBrightnessForSecondFloor(Convert.ToInt32(trbkLightSense.Value));
            }
        }

        /// <summary>
        ///  Change light level for floor (AV) at device level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbkLightSense_MouseUp(object sender, MouseEventArgs e)
        {
            if(ddlFloor.SelectedValue != null)
            {
                ChangeLutronLightLevel(Convert.ToInt32(ddlFloor.SelectedValue));
            }
            else
            {
                MessageBox.Show("Please select floor");
            }

        }

        /// <summary>
        ///  Change light level for floor (AV)
        /// </summary>
        /// <param name="fiFloorID"></param>
        private void ChangeLutronLightLevel(Int32 fiFloorID)
        {
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                //var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                //                            .Where(x => x.device_id == fiDeviceID
                //                                  && x.object_type == LutronFloorObjectType.Lighting_Level)
                //                             .Select(x => x).FirstOrDefault();


                var loBACnetDeviceDetail = (from DM in loESDLutronEntities.BACnetDeviceMappings
                                            join D in loESDLutronEntities.BACnetDevices on DM.device_id equals D.device_id
                                            where DM.floor_id == fiFloorID
                                            && D.object_type.ToUpper() == LutronFloorObjectType.Lighting_Level
                                            && D.object_instance != 7 //// 7 is for alarm event
                                            select new
                                            {
                                                network_id = D.network_id,
                                                device_id = D.device_id,
                                                object_instance = D.object_instance,
                                                object_name = D.object_name,
                                                routed_source = D.routed_source,
                                                routed_net = D.routed_net
                                            }).Distinct().ToList();


                if(loBACnetDeviceDetail != null && loBACnetDeviceDetail.Count > 0)
                {
                    foreach(var loData in loBACnetDeviceDetail)
                    {
                        BacnetAddress loBacnetAddress;
                        loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loData.network_id);
                        loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loData.routed_source, (ushort)loData.routed_net);

                        BacnetValue loNewBacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, Convert.ToSingle(trbkLightSense.Value));
                        BacnetValue[] loWriteNewBacnetValueValue = { loNewBacnetValue };
                        moBacnetClient.WritePropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, (uint)loData.object_instance), BacnetPropertyIds.PROP_PRESENT_VALUE, loWriteNewBacnetValueValue);
                    }
                }
            }
        }

        /// <summary>
        /// get current light level for floor
        /// </summary>
        /// <param name="fiFloorID"></param>
        /// <returns></returns>
        private string GetLutronLightLevel(Int32 fiFloorID)
        {
            IList<BacnetValue> loBacnetValueList;
            using (var loESDLutronEntities = new ESDLutronEntities())
            {
                var loBACnetDeviceDetail = (from DM in loESDLutronEntities.BACnetDeviceMappings
                                            join D in loESDLutronEntities.BACnetDevices on DM.device_id equals D.device_id
                                            where DM.floor_id == fiFloorID
                                            && D.object_type.ToUpper() == LutronFloorObjectType.Lighting_Level
                                            select new
                                            {
                                                network_id = D.network_id,
                                                device_id = D.device_id,
                                                object_instance = D.object_instance,
                                                object_name = D.object_name,
                                                routed_source = D.routed_source,
                                                routed_net = D.routed_net
                                            }).Distinct().FirstOrDefault();


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
        /// change color pattern as per light level scrolled
        /// </summary>
        /// <param name="foColor"></param>
        /// <param name="fiLightness"></param>
        /// <returns></returns>
        private static Color SetLightness(Color foColor, int fiLightness)
        {
            if (fiLightness < liMinLightness)
                fiLightness = liMinLightness;
            else if (fiLightness > liMaxLightness)
                fiLightness = liMaxLightness;

            float ldCOEF = ldMinLightnessCoef +
              (
                (fiLightness - liMinLightness) *
                  ((ldMaxLightnessCoef - ldMinLightnessCoef) / (liMaxLightness - liMinLightness))
              );

            return Color.FromArgb(foColor.A, (int)(foColor.R * ldCOEF), (int)(foColor.G * ldCOEF),
                (int)(foColor.B * ldCOEF));
        }

        /// <summary>
        /// Change light brightness for floor 1 as per light level selected (UI Level)
        /// </summary>
        /// <param name="fiLightness"></param>
        private void ChangeLightBrightnessForFirstFloor(int fiLightness)
        {
            Color loColor = SetLightness(Color.Yellow, fiLightness);
            lbl1.BackColor = loColor;
            lbl2.BackColor = loColor;
            lbl3.BackColor = loColor;
            lbl4.BackColor = loColor;
            lbl5.BackColor = loColor;
            lbl6.BackColor = loColor;
        }

        /// <summary>
        /// Change light brightness for floor 2 as per light level selected (UI Level)
        /// </summary>
        /// <param name="fiLightness"></param>
        private void ChangeLightBrightnessForSecondFloor(int fiLightness)
        {
            Color loColor = SetLightness(Color.Yellow, fiLightness);
            lbl21.BackColor = loColor;
            lbl22.BackColor = loColor;
            lbl23.BackColor = loColor;
            lbl24.BackColor = loColor;
            lbl25.BackColor = loColor;
            lbl26.BackColor = loColor;
        }

        #endregion



        /// <summary>
        /// Get light level for trackbar as per floor selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsPresentValue = "0";
            int liSelectedFloor;
            if (ddlFloor.SelectedValue.GetType() == typeof(KeyValuePair<string, string>))
            {
                System.Reflection.PropertyInfo loPropertyInfo = ddlFloor.SelectedValue.GetType().GetProperty("Key");
                String lsSelectedValue = (String)(loPropertyInfo.GetValue(ddlFloor.SelectedValue, null));
                liSelectedFloor = Convert.ToInt32(lsSelectedValue);
                
                string lsSecondFloorPresentValue = GetLutronLightLevel(2);
                ChangeLightBrightnessForSecondFloor(Convert.ToInt32(lsSecondFloorPresentValue));
            }
            else
            {
                liSelectedFloor = Convert.ToInt32(ddlFloor.SelectedValue);
            }

            lsPresentValue = GetLutronLightLevel(liSelectedFloor);

            if (Convert.ToInt32(lsPresentValue) > 100)
            {
                lsPresentValue = "100";
            }

            trbkLightSense.Value = Convert.ToInt32(lsPresentValue);
            lblSensPer.Text = lsPresentValue;

            if(Convert.ToInt32(liSelectedFloor) == (int)BuildingFloor.FirstFloor)
            {
                ChangeLightBrightnessForFirstFloor(Convert.ToInt32(lsPresentValue));
            }
            else
            {
                ChangeLightBrightnessForSecondFloor(Convert.ToInt32(lsPresentValue));
            }
        }


        /// <summary>
        /// Delegate handler for alarm notification to UI
        /// </summary>
        /// <param name="fsDeviceID"></param>
        /// <param name="fsInstance"></param>
        /// <param name="fsEventState"></param>
        public void AlarmEventChangesToUI(string fsDeviceID, string fsInstance, string fsEventState)
        {
            //// InvokeRequired required compares the thread ID of the
            //// calling thread to the thread ID of the creating thread.
            //// If these threads are different, it returns true.
            if (this.lblAlarmDevice.InvokeRequired)
            {
                SetAlarmEventCallback loSetTextCallback = new SetAlarmEventCallback(AlarmEventChangesToUI);
                this.Invoke(loSetTextCallback, new object[] { fsDeviceID, fsInstance, fsEventState });
            }
            else
            {
                this.lblAlarmDevice.Text = fsDeviceID;
                this.lblAlarmInstance.Text = fsInstance;
                this.lblAlarmEventState.Text = fsEventState;
            }
        }
    }
}

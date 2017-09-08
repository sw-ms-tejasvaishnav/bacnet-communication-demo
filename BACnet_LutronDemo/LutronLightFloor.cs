using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.BACnet;
using System.IO.BACnet.Serialize;
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
            moBacnetClient.OnCreateObjectRequest += new BacnetClient.CreateObjectRequestHandler(handler_OnCreateObjectRequest);

            moBacnetClient.Start();    // go
            moBacnetClient.WhoIs();    // go

            FillDropDown();
            FillAvailableDevicesForSchedule();

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



            var loWeekDay = new BindingList<KeyValuePair<string, string>>();

            loWeekDay.Add(new KeyValuePair<string, string>("0", "Monday"));
            loWeekDay.Add(new KeyValuePair<string, string>("1", "Tuesday"));
            loWeekDay.Add(new KeyValuePair<string, string>("2", "Wednesday"));
            loWeekDay.Add(new KeyValuePair<string, string>("3", "Thursday"));
            loWeekDay.Add(new KeyValuePair<string, string>("4", "Friday"));
            loWeekDay.Add(new KeyValuePair<string, string>("5", "Saturday"));
            loWeekDay.Add(new KeyValuePair<string, string>("6", "Sunday"));

            ddlScheduleDay.DataSource = loWeekDay;
            ddlScheduleDay.ValueMember = "Key";
            ddlScheduleDay.DisplayMember = "Value";


            SetPlaceHolder(txtScheduleHours, "Hours");
            SetPlaceHolder(txtScheduleMinutes, "Minutes");
            SetPlaceHolder(txtScheduleSeconds, "Seconds");
        }

        public void FillAvailableDevicesForSchedule()
        {
            var loAvailableDevices = new BindingList<KeyValuePair<string, string>>();
            loAvailableDevices.Add(new KeyValuePair<string, string>("-1", "Select device"));

            using (var loDataContext = new ESDLutronEntities())
            {
                var loDeviceList = loDataContext
                                    .BACnetDevices
                                    .Where(x => x.object_type == LutronFloorObjectType.Device)
                                    .Select(x => new { x.object_name, x.device_id }).Distinct().ToList();

                if(loDeviceList != null && loDeviceList.Count > 0)
                {
                    foreach(var loDevice in loDeviceList)
                    {
                        loAvailableDevices.Add(new KeyValuePair<string, string>(Convert.ToString(loDevice.device_id), Convert.ToString(loDevice.object_name)));
                    }
                }
            }

            ddlScheduleDevice.DataSource = loAvailableDevices;
            ddlScheduleDevice.ValueMember = "Key";
            ddlScheduleDevice.DisplayMember = "Value";
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

                if (liAlarmEnrolment != null)
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
            if (rbLight1.Checked)
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
            if (rbLight1.Checked || rbLight2.Checked)
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


                if (rbLight1.Checked)
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
            if (ddlFloor.SelectedValue != null)
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


                if (loBACnetDeviceDetail != null && loBACnetDeviceDetail.Count > 0)
                {
                    foreach (var loData in loBACnetDeviceDetail)
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

            if (Convert.ToInt32(liSelectedFloor) == (int)BuildingFloor.FirstFloor)
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


        public void SetPlaceHolder(Control control, string PlaceHolderText)
        {
            control.Text = PlaceHolderText;
            control.GotFocus += delegate (object sender, EventArgs args)
            {
                if (control.Text == PlaceHolderText)
                {
                    control.Text = "";
                }
            };
            control.LostFocus += delegate (object sender, EventArgs args)
            {
                if (control.Text.Length == 0)
                {
                    control.Text = PlaceHolderText;
                }
            };
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            Int32 liDeviceID = Convert.ToInt32(ddlScheduleDevice.SelectedValue);
            Int32 liDaySelected = Convert.ToInt32(ddlScheduleDay.SelectedValue);

            if (liDeviceID > -1)
            {
                using (var loESDLutronEntities = new ESDLutronEntities())
                {
                    var loBACnetDeviceDetail = loESDLutronEntities.BACnetDevices
                                               .Where(x => x.device_id == liDeviceID
                                                     && x.object_type.ToUpper() == LutronFloorObjectType.Device)
                                               .Select(x => x).FirstOrDefault();


                    int? liTopInstanceID = loESDLutronEntities.BACnetDevices
                                               .Where(x => x.device_id == liDeviceID
                                                     && x.object_type.ToUpper() == LutronFloorObjectType.SCHEDULE)
                                               .Select(x => x.object_instance).OrderByDescending(x=> x.Value).FirstOrDefault();


                    BacnetAddress loBacnetAddress;
                    loBacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.network_id);
                    loBacnetAddress.RoutedSource = new BacnetAddress(BacnetAddressTypes.IP, loBACnetDeviceDetail.routed_source, (ushort)loBACnetDeviceDetail.routed_net);

                    //var deviceObjId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, (uint)liDeviceID);
                    //var objectIdList = await moBacnetClient.ReadPropertyAsync(loBacnetAddress, deviceObjId, BacnetPropertyIds.PROP_OBJECT_LIST);

                    //IList<BacnetValue> loDeviceObject;
                    //moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, (uint)liDeviceID), BacnetPropertyIds.PROP_OBJECT_LIST, out loDeviceObject);

                    //IList<BacnetValue> loScheduleValues;
                    //moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_SCHEDULE, (uint)11), BacnetPropertyIds.PROP_LIST_OF_OBJECT_PROPERTY_REFERENCES, out loScheduleValues);


                    //IList<BacnetValue> loWeekValues;
                    //moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_SCHEDULE, (uint)11), BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, out loWeekValues);



                    //IList<BacnetValue> loObjectName;
                    //moBacnetClient.ReadPropertyRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_SCHEDULE, (uint)11), BacnetPropertyIds.PROP_OBJECT_NAME, out loObjectName);


                    //var loData = values
                    //              .Where(x => ((BacnetObjectId)values[0].Value).Type == BacnetObjectTypes.OBJECT_SCHEDULE)
                    //              .Select(x => x.Value.GetType().GetProperty("Instance"))




                    ICollection<BacnetPropertyValue> loBacnetPropertyValueList = new List<BacnetPropertyValue>();

                    BacnetPropertyValue loNewPropertyValue = new BacnetPropertyValue();
                    List<BacnetValue> loBacnetValue = new List<BacnetValue>();

                    
                    #region set schedule
                    //Create new instance id based on largest available
                    liTopInstanceID = liTopInstanceID != null ? liTopInstanceID + 1 : 1;

                    //// Set schedule object name
                    loBacnetValue = new List<BacnetValue>();
                    loNewPropertyValue = new BacnetPropertyValue();
                    loBacnetValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_CHARACTER_STRING, "Schedule" + " " + liTopInstanceID));

                    loNewPropertyValue.value = loBacnetValue;
                    loNewPropertyValue.property = new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_OBJECT_NAME, System.IO.BACnet.Serialize.ASN1.BACNET_ARRAY_ALL);

                    loBacnetPropertyValueList.Add(loNewPropertyValue);


                    //// Set effective period for schedule object
                    loBacnetValue = new List<BacnetValue>();
                    loNewPropertyValue = new BacnetPropertyValue();
                    loBacnetValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_DATE, DateTime.Today.AddDays(-1)));
                    loBacnetValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_DATE, DateTime.Today.AddMonths(1)));

                    loNewPropertyValue.value = loBacnetValue;
                    loNewPropertyValue.property = new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_EFFECTIVE_PERIOD, System.IO.BACnet.Serialize.ASN1.BACNET_ARRAY_ALL);

                    loBacnetPropertyValueList.Add(loNewPropertyValue);
                    #endregion


                    //// Set object reference to update it's value with schedule object
                    loBacnetValue = new List<BacnetValue>();
                    loNewPropertyValue = new BacnetPropertyValue();

                    BacnetDeviceObjectPropertyReference loPropertyReference = new BacnetDeviceObjectPropertyReference();
                    loPropertyReference.ArrayIndex = -1;
                    loPropertyReference.DeviceId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, (uint)liDeviceID);
                    loPropertyReference.ObjectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_VALUE, (uint)1);
                    loPropertyReference.PropertyId = BacnetPropertyIds.PROP_PRESENT_VALUE;


                    loBacnetValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_OBJECT_PROPERTY_REFERENCE,
                                      loPropertyReference));

                    loNewPropertyValue.value = loBacnetValue;
                    loNewPropertyValue.property = new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_LIST_OF_OBJECT_PROPERTY_REFERENCES, System.IO.BACnet.Serialize.ASN1.BACNET_ARRAY_ALL);

                    loBacnetPropertyValueList.Add(loNewPropertyValue);





                    //// Add weekly schedule for object
                    loBacnetValue = new List<BacnetValue>();
                    loNewPropertyValue = new BacnetPropertyValue();


                    BacnetWeeklySchedule loBacnetWeeklySchedule = new BacnetWeeklySchedule();
                    loBacnetWeeklySchedule.days[liDaySelected] = new List<DaySchedule>();
                    loBacnetWeeklySchedule.days[liDaySelected].Add(new DaySchedule(DateTime.Now.AddMinutes(1),
                          Convert.ToSingle((new Random()).Next(100, 999))));

                    loBacnetValue.Add(new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_WEEKLY_SCHEDULE, loBacnetWeeklySchedule));


                    loNewPropertyValue.value = loBacnetValue;
                    loNewPropertyValue.property = new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, System.IO.BACnet.Serialize.ASN1.BACNET_ARRAY_ALL);

                    loBacnetPropertyValueList.Add(loNewPropertyValue);

                    moBacnetClient.CreateObjectRequest(loBacnetAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_SCHEDULE, (uint)liTopInstanceID), loBacnetPropertyValueList);
                }
            }
            else
            {
                MessageBox.Show("Please select device");
            }
        }


        static void handler_OnCreateObjectRequest(BacnetClient sender, BacnetAddress adr, byte invoke_id, BacnetObjectId object_id, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {

        }

        [Serializable]
        class DaySchedule
        {
            public DateTime dt;
            public object Value;

            public DaySchedule(DateTime dt, object Value)
            {
                this.dt = dt;
                this.Value = Value;
            }
        }


        [Serializable]
        class BacnetWeeklySchedule : ASN1.IEncode
        {
            public List<DaySchedule>[] days = new List<DaySchedule>[7];

            public void Encode(EncodeBuffer buffer)
            {
                for (int i = 0; i < 7; i++)
                {
                    ASN1.encode_opening_tag(buffer, 0);
                    if (days[i] != null)
                    {
                        List<DaySchedule> dsl = days[i];
                        foreach (DaySchedule ds in dsl)
                        {
                            ASN1.bacapp_encode_application_data(buffer, new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_TIME, ds.dt));
                            ASN1.bacapp_encode_application_data(buffer, new BacnetValue(ds.Value));

                        }
                    }
                    ASN1.encode_closing_tag(buffer, 0);
                }
            }
        }

    }
}

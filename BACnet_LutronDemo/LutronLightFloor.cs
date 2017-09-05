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

        #region init

        public LutronLightFloor()
        {
            InitializeComponent();

            //// Bacnet on UDP/IP/Ethernet
            moBacnetClient = new BacnetClient(new BacnetIpUdpProtocolTransport(47808, false));// (0xBAC0, false));
            moBacnetClient.Start();    // go

            FillDropDown();
            
        }

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


        #region Light toggle light 1/light 2

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
    }
}

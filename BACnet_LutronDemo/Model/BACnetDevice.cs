using System;
using System.Collections.Generic;
using System.IO.BACnet;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACnet_LutronDemo.Model
{
    public class BACnetDeviceModel
    {
        public BACnetDeviceModel()
        {
            loBACnetDeviceList = new List<BACnetDeviceNew>();
        }

        public List<BACnetDeviceNew> loBACnetDeviceList { get; set; }
    }

    public class BACnetDeviceNew
    {
        public BacnetAddress loBACnetAddress { get; set; }
        public uint inDeviceID{ get; set; }
        public uint inInstanceID { get; set; }
    }
}

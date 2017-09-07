using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACnet_LutronDemo.Common
{
    class CommonConstant
    {
        public enum LutronObjectType : uint
        {
            Lighting_Level = 2,
            Lighting_State = 3
        }

        public static class LutronFloorObjectType
        {
            public static string Lighting_Level = "OBJECT_ANALOG_VALUE";
            public static string Lighting_State = "OBJECT_BINARY_VALUE";
            public static string Device = "OBJECT_DEVICE";
            public static string NOTIFICATION_CLASS = "OBJECT_NOTIFICATION_CLASS";
            public static string ALERT_ENROLLMENT = "OBJECT_ALERT_ENROLLMENT";
        }

        public enum BuildingFloor : int
        {
            FirstFloor = 1,
            SecondFloor = 2
        }
    }
}

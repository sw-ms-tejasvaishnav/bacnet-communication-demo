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
    }
}

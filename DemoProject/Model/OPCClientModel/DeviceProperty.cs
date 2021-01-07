using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OPCClientModel
{
    public class DeviceProperty
    {
        public DeviceProperty()
        {
            Children = new List<DeviceProperty>();
        }

        public string Channel
        {
            get;set;
        }

        public string Device
        {
            get;set;
        }

        public string TagName
        {
            get;set;
        }

        public string DataType
        {
            get;set;
        }

        public List<DeviceProperty> Children { get; set; }
    }
}

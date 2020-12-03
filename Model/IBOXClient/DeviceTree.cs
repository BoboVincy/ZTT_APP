using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IBOXClient
{
    public class DeviceTree
    {
        public DeviceTree()
        {
            Children = new List<DeviceTree>();
        }

        public string TreeNode
        {
            get;set;
        }

        public string TreeItem
        {
            get;set;
        }

        public List<DeviceTree> Children
        {
            get;set;
        }
    }
}

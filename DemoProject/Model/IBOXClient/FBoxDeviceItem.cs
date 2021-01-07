using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IBOXClient
{
    public class FBoxDeviceItem
    {
        public FBoxDeviceItem()
        {
            Children = new List<FBoxDeviceItem>();
        }

        public string GroupItem
        {

            get;set;
        }

        public string GroupName
        {
            get;set;
        }

        public string BoxName
        {
            get;set;
        }
        public string BoxNO
        {
            get; set;
        }

        public string BoxState
        {
            get;set;
        }

        public List<FBoxDeviceItem> Children
        {
            get;set;
        }


    }
}

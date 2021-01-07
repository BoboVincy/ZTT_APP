using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IBOXClient
{
    public class FBoxTagInfo
    {
        public string GroupName
        {
            get;set;
        }

        public string TagName
        {
            get;set;
        }

        public string TagMainAddress
        {
            get;set;
        }

        public string TagValue
        {
            get; set;
        } = "0";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MQTTModel
{
    public class SqlInfo
    {
        public string TagName {
            get;set;
        }

        public string TagValue {
            get;set;
        }

        public DateTime InserTime {
            get;set;
        }
    }
}

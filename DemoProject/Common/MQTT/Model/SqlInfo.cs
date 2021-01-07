using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MQTT.Model
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

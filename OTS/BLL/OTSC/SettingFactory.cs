using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;
using Common.Log4net;

namespace BLL.OTSC
{
    public class SettingFactory
    {
        GetServer OPCClient;
        SqlHelper SqlClient;
        OTSUIProperty UIProperty;
        OTSDataInfo DataInfo;
        public SettingFactory(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient, OTSDataInfo DataInfo)
        {
            this.UIProperty = UIProperty;
            this.UIProperty = UIProperty;
            this.OPCClient = OPCClient;
            this.SqlClient = SqlClient;
            this.DataInfo = DataInfo;
        }


        public SettingVirClass GetSettingClass(string Type)
        {
            switch (Type)
            {
                case "横向表":
                    return new SettingOperation1(UIProperty, OPCClient, SqlClient, DataInfo);
                case "纵向表":
                    return new SettingOperation2(UIProperty, OPCClient, SqlClient, DataInfo);
            }
            return null;
        }
    }
}

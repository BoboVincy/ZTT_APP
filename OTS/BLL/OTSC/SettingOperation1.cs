using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;
using Common.Log4net;

namespace BLL.OTSC
{
    public class SettingOperation1:SettingVirClass
    {

        public SettingOperation1(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient, OTSDataInfo DataInfo) :base(UIProperty,OPCClient,SqlClient,DataInfo)
        {

        }


       
    }
}

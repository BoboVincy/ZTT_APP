using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.MQTTModel;


namespace BLL.MQTTCLientBLL
{
    public class MQTTSql
    {
        public static bool DataInsert(List<SqlInfo> DataList)
        {
            bool OpenResult = SqlHelper.SqlOpen("server=127.0.0.1;uid=admin;pwd=chen3724913416;Integrated Security=True;DataBase=test");
            if (OpenResult == true)
            {
                bool ModifyResult = false;
                foreach (SqlInfo Item in DataList)
                {
                    ModifyResult = SqlHelper.SqlModify(string.Format(@"insert into MQTTTest(TagName,TagValue,InsertTime) values('@0',@1,'@2')", Item.TagName, Item.TagValue, Item.InserTime));
                    if (ModifyResult == false)
                    {
                        break;

                    }
                }
                if (ModifyResult==true)
                {
                    if (SqlHelper.SqlClose())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}

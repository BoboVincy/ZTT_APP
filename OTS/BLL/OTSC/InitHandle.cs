using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;
using BLL.OTSC;
using Common.XML;
using Common.Log4net;

namespace BLL.OTSC
{
    public class InitHandle
    {
        OTSUIProperty UIProperty;
        OTSDataInfo DataInfo;

        public InitHandle(OTSUIProperty UIProperty,OTSDataInfo DataInfo)
        {
            this.UIProperty = UIProperty;
            this.DataInfo = DataInfo;
        }

        /// <summary>
        /// 读取配置档
        /// </summary>
        public void ReadConfig()
        {
            try
            {
                DataInfo.BaseToServerDic = new Dictionary<string, Dictionary<string, string>>();
                DataInfo.PortraitTableToTagListDic = new Dictionary<string, List<string>>();
                XMLProcess Xml = new XMLProcess(@"OTSV/Config.ini");
                UIProperty.WriteFileNum = Convert.ToInt32(Xml.Read("OTSConfig/WriteFileNum/FileNum"));
                UIProperty.ProcExeTime = Convert.ToInt32(Xml.Read("OTSConfig/ProcExeTime/ExeTime"));
                UIProperty.CheckTime= Convert.ToInt32(Xml.Read("OTSConfig/CheckIsNormalTime/CheckTime"));
                XMLProcess SavedXml = new XMLProcess(@"OTSV/SavedConfig.ini");
                int TableNum = SavedXml.ReadAllChild("SavaedConfig/Tables").Count;
                UIProperty.OPCServerIP = SavedXml.Read("SavaedConfig/SavedUIProperty/KepServerIP");
                UIProperty.DataBaseIP = SavedXml.Read("SavaedConfig/SavedUIProperty/DataBaseIP");
                UIProperty.SavedDataBaseIP = UIProperty.DataBaseIP;
                UIProperty.DataBaseUserName = SavedXml.Read("SavaedConfig/SavedUIProperty/DataBaseUserName");
                UIProperty.DataBasePassWord = SavedXml.Read("SavaedConfig/SavedUIProperty/DataBasePassWord");
                UIProperty.DataBaseName = SavedXml.Read("SavaedConfig/SavedUIProperty/DataBaseName");
                UIProperty.SavedTableType = SavedXml.Read("SavaedConfig/SavedUIProperty/TableType");
                for (int i = 1; i <= TableNum; i++)
                {
                    Dictionary<string, string> FieldToTagNameDic = new Dictionary<string, string>();
                    List<string> TagNameList = new List<string>();
                    string TableName = SavedXml.ReadAttribute("SavaedConfig/Tables/Table" + i.ToString(), "Value");
                    int FieldNum = SavedXml.ReadAllChild("SavaedConfig/Tables/Table" + i.ToString()).Count;
                    for (int m = 1; m <= FieldNum; m++)
                    {
                        string FieldName = SavedXml.ReadAttribute("SavaedConfig/Tables/Table" + i + "/Field" + m, "Value");
                        string TagName1 = SavedXml.ReadAttribute("SavaedConfig/Tables/Table" + i + "/Field" + m + "/TagName", "Value");
                        string TagName2 = SavedXml.Read("SavaedConfig/Tables/Table" + i + "/TagName" + m);
                        if (FieldName != "")
                        {
                            FieldToTagNameDic.Add(FieldName, TagName1);
                        }
                        TagNameList.Add(TagName2);
                    }
                    if (!FieldToTagNameDic.Values.Contains(""))
                    {
                        DataInfo.BaseToServerDic.Add(TableName, FieldToTagNameDic);
                    }
                    if (!TagNameList.Contains(""))
                    {
                        DataInfo.PortraitTableToTagListDic.Add(TableName, TagNameList);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("读取配置档失败！" + ex.Message);
            }
        }

        
    }
}

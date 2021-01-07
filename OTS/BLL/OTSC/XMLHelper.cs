using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Log4net;
using Model.OTSM;

namespace BLL.OTSC
{
    public class XMLHelper
    {
        /// <summary>
        /// 拼接XML字符串
        /// </summary>
        /// <param name="FieldNameList">字段名称列表</param>
        /// <param name="IndexDic">索引字典</param>
        /// <param name="ValueList">值数组</param>
        /// <returns></returns>
        public static string GetXmlStr(List<string> FieldNameList,Dictionary<string,int> IndexDic,string[] ValueArray)
        {
            try
            {
                string Result = "";
                foreach (string Field in FieldNameList)
                {
                    Result = Result + Field + "=\"" + ValueArray[IndexDic[Field]] + "\" ";
                }
                string XmlStr = "<Param " + Result + "createtime=\""+DateTime.Now.ToString()+"\"/>";
                return XmlStr;
            }
            catch(Exception EX)
            {
                LogHelp.Log.Error("拼接XML字符串出错！" + EX.Message);
                return "";
            }
        }

        public static string GetPortraitXMLStr (List<string> TagList,Dictionary<int,string> DescriptionToFieldDic, Dictionary<string, int> IndexDic, string[] ValueArray)
        {
            try
            {
                string Result = "";
                foreach (string Tag in TagList)
                {
                    string MyTag = Tag;
                    if (Tag.Contains("."))
                    {
                        char[] Separator = { '.' };
                        string[] StrArray = Tag.Split(Separator);
                        MyTag = StrArray[2];
                    }
                    Result = Result + "<Param "+DescriptionToFieldDic[1] + "=\"" + MyTag + "\"" + " " + DescriptionToFieldDic[2] + "=\"" + ValueArray[IndexDic[Tag]] + "\" InsertTime=\"" +DateTime.Now.ToString()+ "\"/>";
                }
                return Result;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("拼接XML字符串出错！" + EX.Message);
                return "";
            }
        }

        public static string GetSaveXMLStr1(Dictionary<string,Dictionary<string,string>> SavedDataDic,ConfigInfo SavedUIProperty)
        {
            try
            {
                string TopicXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<SavaedConfig>\n<SavedUIProperty>\n";
                string KepServerIPXML = "<KepServerIP>"+SavedUIProperty.KepServerIP+"</KepServerIP>\n";
                string DataBaseIPXML= "<DataBaseIP>" + SavedUIProperty.DataBaseIP+ "</DataBaseIP>\n";
                string DataBaseUserNameXML = "<DataBaseUserName>" + SavedUIProperty.DataBaseUserName + "</DataBaseUserName>\n";
                string DataBasePassWordXML = "<DataBasePassWord>" + SavedUIProperty.DataBasePassword + "</DataBasePassWord>\n";
                string DataBaseNameXML = "<DataBaseName>" + SavedUIProperty.DataBaseName + "</DataBaseName>\n";
                string TableTypeXML = "<TableType>" + SavedUIProperty.TableType + "</TableType>\n</SavedUIProperty>\n";
                string HeadXML = "<Tables>\n";
                int TableIndex = 1;
                string ContentXML = "";
                if (SavedDataDic != null)
                {
                    foreach (string Table in SavedDataDic.Keys)
                    {
                        string XML1 = "<Table" + TableIndex + " Value=\"" + Table + "\">\n";
                        string XML2 = "";
                        int FieldIndex = 1;
                        foreach (string Item in SavedDataDic[Table].Keys)
                        {
                            XML2 = XML2 + "<Field" + FieldIndex + " Value=\"" + Item + "\">\n" + "<TagName Value=\"" + SavedDataDic[Table][Item] + "\"/>\n" + "</Field" + FieldIndex + ">\n";
                            FieldIndex++;
                        }
                        ContentXML = ContentXML + XML1 + XML2 + "</Table" + TableIndex + ">\n";
                        TableIndex++;
                    }
                }
                string EndXML = "</Tables>\n</SavaedConfig>\n";
                string TableXml = TopicXML + KepServerIPXML + DataBaseIPXML + DataBaseUserNameXML + DataBasePassWordXML + DataBaseNameXML + TableTypeXML + HeadXML + ContentXML + EndXML;
                return TableXml;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("拼接XML字符串出错！" + ex.Message);
                return "";
            }
        }

        public static string GetSaveXMLStr2(Dictionary<string, List<string>> SavedDataDic, ConfigInfo SavedUIProperty)
        {
            try
            {

                string TopicXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<SavaedConfig>\n<SavedUIProperty>\n";
                string KepServerIPXML = "<KepServerIP>" + SavedUIProperty.KepServerIP + "</KepServerIP>\n";
                string DataBaseIPXML = "<DataBaseIP>" + SavedUIProperty.DataBaseIP + "</DataBaseIP>\n";
                string DataBaseUserNameXML = "<DataBaseUserName>" + SavedUIProperty.DataBaseUserName + "</DataBaseUserName>\n";
                string DataBasePassWordXML = "<DataBasePassWord>" + SavedUIProperty.DataBasePassword + "</DataBasePassWord>\n";
                string DataBaseNameXML = "<DataBaseName>" + SavedUIProperty.DataBaseName + "</DataBaseName>\n";
                string TableTypeXML = "<TableType>" + SavedUIProperty.TableType + "</TableType>\n</SavedUIProperty>\n";
                string HeadXML = "<Tables>\n";
                int TableIndex = 1;
                string ContentXML = "";
                if (SavedDataDic != null)
                {
                    foreach (string Table in SavedDataDic.Keys)
                    {
                        string XML1 = "<Table" + TableIndex + " Value=\"" + Table + "\">\n";
                        string XML2 = "";
                        int FieldIndex = 1;
                        foreach (string Item in SavedDataDic[Table])
                        {
                            XML2 = XML2 + "<TagName" + FieldIndex + ">" + Item + "</TagName" + FieldIndex + ">\n";
                            FieldIndex++;
                        }
                        ContentXML = ContentXML + XML1 + XML2 + "</Table" + TableIndex + ">\n";
                        TableIndex++;
                    }
                }
                string EndXML = "</Tables>\n</SavaedConfig>\n";
                string TableXml = TopicXML + KepServerIPXML + DataBaseIPXML + DataBaseUserNameXML + DataBasePassWordXML + DataBaseNameXML+ TableTypeXML + HeadXML + ContentXML + EndXML;
                return TableXml;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("拼接XML字符串出错！" + ex.Message);
                return "";
            }
        }
    }
}

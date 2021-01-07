using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;
using Common.Log4net;

namespace BLL.OTSC
{
    public class SettingOperation2 : SettingVirClass
    {
        /// <summary>
        /// 字典：键：字段描述标签 值：字段值
        /// </summary>
        Dictionary<int, string> IndexToField;
        /// <summary>
        /// 字典：键：字段描述 值：字段描述标签
        /// </summary>
        Dictionary<string, int> DescriptionIndexDic=new Dictionary<string, int>();
        /// <summary>
        /// 字典：键：字段描述标签 值：字段描述
        /// </summary>
        Dictionary<int, string> IndedxDescriptionDic = new Dictionary<int, string>();
        /// <summary>
        /// 点名称列表
        /// </summary>
        List<string> TagNameList;

        public SettingOperation2(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient, OTSDataInfo DataInfo) : base(UIProperty, OPCClient, SqlClient, DataInfo)
        {
            DataInfo.TableToDescriptionToFieldDicDic = new Dictionary<string, Dictionary<int, string>>();
            DescriptionIndexDic.Add("点名称", 1);
            DescriptionIndexDic.Add("点值", 2);
            IndedxDescriptionDic.Add(1, "点名称");
            IndedxDescriptionDic.Add(2, "点值");
        }

        public override void TypeSave()
        {
            if (UIProperty.SelectedField == null || UIProperty.SelectedDescription == null)
            {
                MyDelegate.dShowMessageBox("请先选择关联项！");
            }
            else
            {
                if (!DataInfo.TableToDescriptionToFieldDicDic.Keys.Contains(UIProperty.SelectedTable))
                {
                    IndexToField = new Dictionary<int, string>();
                    DataInfo.TableToDescriptionToFieldDicDic.Add(UIProperty.SelectedTable, IndexToField);
                }
                if (!IndexToField.Keys.Contains(DescriptionIndexDic[UIProperty.SelectedDescription]))
                {
                    IndexToField.Add(DescriptionIndexDic[UIProperty.SelectedDescription], UIProperty.SelectedField);
                    DataInfo.TableToDescriptionToFieldDicDic[UIProperty.SelectedTable] = IndexToField;
                    UIProperty.DescriptionByFieldList.Add(UIProperty.SelectedDescription + "==" + UIProperty.SelectedField);
                }
                else
                {
                    MyDelegate.dShowMessageBox("此字段已被关联！\n 关联字段:\n" + UIProperty.SelectedDescription + "=" + IndexToField[DescriptionIndexDic[UIProperty.SelectedDescription]]);
                }
            }
        }

        public override void TypeDescriptionChanged()
        {
            char[] Separator = { '=' };
            if (UIProperty.SelectedTypeDescription != null)
            {
                string[] StrArray = UIProperty.SelectedTypeDescription.Split(Separator);
                UIProperty.WillDeleteItem = StrArray[0];
            }
        }

        public override void TypeDelete()
        {
            if (UIProperty.WillDeleteItem == null)
            {
                MyDelegate.dShowMessageBox("请先选择待删除项！");
            }
            else
            {
                UIProperty.DescriptionByFieldList.Remove(UIProperty.WillDeleteItem + "==" + DataInfo.TableToDescriptionToFieldDicDic[UIProperty.SelectedTable][DescriptionIndexDic[UIProperty.WillDeleteItem]]);
                DataInfo.TableToDescriptionToFieldDicDic[UIProperty.SelectedTable].Remove(DescriptionIndexDic[UIProperty.WillDeleteItem]);
            }
        }


        public override void TableChanged()
        {
            UIProperty.ShowItemList = new List<string>();
            UIProperty.DescriptionByFieldList = new List<string>();
            UIProperty.FieldNameList = new List<string>();
            if (UIProperty.SelectedTable == null)
            {
                return;
            }
            if (DataInfo.TableToDescriptionToFieldDicDic.Keys.Contains(UIProperty.SelectedTable))
            {
                List<string> ShowDescriptionList = new List<string>();
                IndexToField = DataInfo.TableToDescriptionToFieldDicDic[UIProperty.SelectedTable];
                foreach (int Item in IndexToField.Keys)
                {
                    ShowDescriptionList.Add(IndedxDescriptionDic[Item] + "==" + IndexToField[Item]);
                }
                UIProperty.DescriptionByFieldList = ShowDescriptionList;
            }
            if (DataInfo.PortraitTableToTagListDic.Keys.Contains(UIProperty.SelectedTable))
            {
                List<string> ShowFieldList = new List<string>();
                TagNameList = DataInfo.PortraitTableToTagListDic[UIProperty.SelectedTable];
                foreach (string Item in TagNameList)
                {
                    ShowFieldList.Add(UIProperty.SelectedTable + "==" + Item);
                }
                UIProperty.ShowItemList = ShowFieldList;
            }
        }

        public override void AddItem()
        {
            if (DataInfo.TableToDescriptionToFieldDicDic[UIProperty.SelectedTable].Keys.Count != 2)
            {
                MyDelegate.dShowMessageBox("请先将点值和点名称定义完毕！");
                return;
            }
            if (UIProperty.SelectedTable == null || UIProperty.SelectedTag == null)
            {
                MyDelegate.dShowMessageBox("请先选择关联项！");
            }
            else
            {
                if (!DataInfo.PortraitTableToTagListDic.Keys.Contains(UIProperty.SelectedTable))
                {
                    TagNameList = new List<string>();
                    DataInfo.PortraitTableToTagListDic.Add(UIProperty.SelectedTable, TagNameList);
                }
                if (!TagNameList.Contains(UIProperty.SelectedTag))
                {
                    TagNameList.Add(UIProperty.SelectedTag);
                    DataInfo.PortraitTableToTagListDic[UIProperty.SelectedTable] = TagNameList;
                    UIProperty.ShowItemList.Add(UIProperty.SelectedTable + "==" + UIProperty.SelectedTag);
                }
                else
                {
                    MyDelegate.dShowMessageBox("此字段已配置入表中!");
                }
            }
        }

        public override void SelectedResult()
        {
            char[] Separator = { '=' };
            if (UIProperty.SelectedView != null)
            {
                string[] StrArray = UIProperty.SelectedView.Split(Separator);
                UIProperty.WillDeleteItem = StrArray[2];
            }
        }

        public override void DeleteItem()
        {
            if (UIProperty.WillDeleteItem==null)
            {
                MyDelegate.dShowMessageBox("请先选择待删除项！");
            }
            else
            {
                UIProperty.ShowItemList.Remove(UIProperty.SelectedTable + "==" + UIProperty.WillDeleteItem);
                TagNameList.Remove(UIProperty.WillDeleteItem);
                DataInfo.PortraitTableToTagListDic[UIProperty.SelectedTable]=TagNameList;
            }
        }

        public override void Save()
        {
            DataInfo.GatherFieldDic = new Dictionary<string, int>();
            //Dictionary<string, decimal> TagValueDic = new Dictionary<string, decimal>();
            DataInfo.SavedPortraitTableToTagListDic = DataInfo.PortraitTableToTagListDic;
            DataInfo.GatherTagList = new List<string>();
            DataInfo.TableToFieldIndexDic = new Dictionary<string, Dictionary<string, int>>();
            //List<string> GatherFieldList = new List<string>();
            DataInfo.GatherTableToFeildList = new Dictionary<string, List<string>>();
            //UIProperty.AllItemList = new List<string>();
            TagFieldList = new List<TableFieldInfo>();
            DataInfo.TableToTempDataStr = new Dictionary<string, List<string>>();
            DataInfo.TableToDataStr = new Dictionary<string, string>();
            int TagIndex = 0;
            foreach (string Table in DataInfo.SavedPortraitTableToTagListDic.Keys)
            {
                DataInfo.TableToTempDataStr.Add(Table, null);
                DataInfo.TableToDataStr.Add(Table, "");
                List<string> TagList = new List<string>();
                List<string> OneTableField = new List<string>();
                foreach (string Item in DataInfo.TableToDescriptionToFieldDicDic[Table].Values)
                {
                    OneTableField.Add(Item);
                }
                foreach (string TagName in DataInfo.SavedPortraitTableToTagListDic[Table])
                {
                    DataInfo.GatherTagList.Add(TagName);
                    //UIProperty.AllItemList.Add(Table + "==" + TagName);
                    TagFieldList.Add(new TableFieldInfo
                    {
                        TableName = Table,
                        TagName = TagName
                    });
                    DataInfo.GatherFieldDic.Add(TagName, TagIndex);
                    TagIndex++;
                }
                DataInfo.TableToFieldIndexDic.Add(Table, DataInfo.GatherFieldDic);
                DataInfo.GatherTableToFeildList.Add(Table, OneTableField);
            }
            MyDelegate.dShowMainPage();
            //MyDelegate.dUpdateViewList();
            MyDelegate.dShowItem(TagFieldList);
        }

        public override void WriteToDataPool()
        {
            foreach (string Table in DataInfo.SavedPortraitTableToTagListDic.Keys)
            {
                List<string> TempDataStrList = new List<string>();
                if (DataInfo.TableToTempDataStr[Table] == null)
                {
                    DataInfo.TableToTempDataStr[Table] = TempDataStrList;
                }
                DataInfo.TableToTempDataStr[Table].Add(XMLHelper.GetPortraitXMLStr(DataInfo.SavedPortraitTableToTagListDic[Table],DataInfo.TableToDescriptionToFieldDicDic[Table],DataInfo.TableToFieldIndexDic[Table],OPCClient.TagValueArray));
                if (IsReadFlag == false)
                {
                    if (DataInfo.TableToTempDataStr[Table].Count > UIProperty.WriteFileNum)
                    {
                        foreach (string item in DataInfo.TableToTempDataStr[Table])
                        {
                            DataInfo.TableToDataStr[Table] = DataInfo.TableToDataStr[Table] + item;
                        }
                        if (FileHelper.WriteToFile("C:/OTS/DataLogTempPool/" + Table + ".txt", "<root>" + DataInfo.TableToDataStr[Table] + "</root>"))
                        {
                            DataInfo.TableToTempDataStr[Table].Clear();
                        }
                    }
                }
            }
        }

        public override void ReadFile()
        {
            IsReadFlag = true;
            foreach (string Table in DataInfo.SavedPortraitTableToTagListDic.Keys)
            {
                string XmlStr = "";
                string TableField = "";
                string XmlFormat = "";
                for (int i = 1; i <= DataInfo.TableToDescriptionToFieldDicDic[Table].Keys.Count; i++)
                {
                    TableField = TableField + DataInfo.TableToDescriptionToFieldDicDic[Table][i] + ",";
                }
                
                XmlFormat = DataInfo.TableToDescriptionToFieldDicDic[Table][1] + " varchar(30)," + DataInfo.TableToDescriptionToFieldDicDic[Table][2] + " decimal(18,2),InsertTime datetime";
                string MyTableField = TableField+" InsertTime";
                //string MyXmlFormat = XmlFormat.Remove(XmlFormat.Count() - 1, 1);
                bool Result = FileHelper.ReadFromFile("C:/OTS/DataLogTempPool/" + Table + ".txt", out XmlStr);
                if (Result && XmlStr.Count() > 0)
                {
                    List<ProcParamModel> ParamList = new List<ProcParamModel>();
                    ParamList.Add(new ProcParamModel
                    {
                        ParamName = "@XmlString",
                        ParamValue = XmlStr

                    });
                    ParamList.Add(new ProcParamModel
                    {
                        ParamName = "@TableName",
                        ParamValue = Table

                    });
                    ParamList.Add(new ProcParamModel
                    {
                        ParamName = "@TableField",
                        ParamValue = MyTableField
                    });
                    ParamList.Add(new ProcParamModel
                    {
                        ParamName = "@XmlFormat",
                        ParamValue = XmlFormat
                    });
                    bool SqlResult = SqlClient.ExecutePro("ProcOPCToTable", ParamList);
                    if (SqlResult)
                    {
                        DataInfo.TableToDataStr[Table] = "";
                        FileHelper.WriteToFile("C:/OTS/DataLogTempPool/" + Table + ".txt", "");
                    }
                }
            }
            IsReadFlag = false;
        }

        public override void WriteSavedConfig()
        {
            ConfigInfo Config = new ConfigInfo();
            Config.KepServerIP = UIProperty.OPCServerIP;
            Config.DataBaseIP = UIProperty.DataBaseIP;
            Config.DataBaseUserName = UIProperty.DataBaseUserName;
            Config.DataBasePassword = UIProperty.DataBasePassWord;
            Config.DataBaseName = UIProperty.DataBaseName;
            Config.TableType = UIProperty.SelectTableType;
            string Result = XMLHelper.GetSaveXMLStr2(DataInfo.SavedPortraitTableToTagListDic, Config);
            FileHelper.WriteToFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + "OTSV/SavedConfig.ini", Result);
        }


    }
}

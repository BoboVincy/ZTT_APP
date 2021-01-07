using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;
using Common.Log4net;

namespace BLL.OTSC
{
    public class SettingVirClass
    {
        public GetServer OPCClient;
        public SqlHelper SqlClient;
        public OTSUIProperty UIProperty;
        public OTSDataInfo DataInfo;
        List<string> TagList;//该KepServer节点上的所有点
        List<string> FieldList;//单张表的所有点
        Dictionary<string, string> FieldToTagDic;
        List<string> DeviceTagList;
        List<string> TableFieldList;
        List<string> NeverRelationFieldList;
        Dictionary<string, Dictionary<string, List<string>>> ChannelToDeviceDic;
        public bool IsReadFlag = false;
        public List<TableFieldInfo> TagFieldList;

        public SettingVirClass(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient, OTSDataInfo DataInfo)
        {
            this.UIProperty = UIProperty;
            this.OPCClient = OPCClient;
            this.SqlClient = SqlClient;
            this.DataInfo = DataInfo;
            MyDelegate.dReturnExample += ReturnExample;
            MyDelegate.dWriteToFile = null;
            MyDelegate.dReadToFile = null;
            MyDelegate.dWriteToFile += WriteToDataPool;
            MyDelegate.dReadToFile += ReadFile;
            MyDelegate.dWriteToSaveFile += WriteSavedConfig;
            FieldToTagDic = new Dictionary<string, string>();
        }
        public virtual void ReadTable()
        {
            FieldList = SqlClient.GetSqlResult(string.Format("SELECT NAME FROM SYSCOLUMNS WHERE ID=OBJECT_ID('{0}')", UIProperty.SelectedTable));
            UIProperty.FieldNameList = FieldList;
        }

        public virtual void ReadServer()
        {
            TagList = OPCClient.GetDevice(UIProperty.SelectedServerNode, UIProperty.OPCServerIP);
            if (TagList.Count > 0)
            {
                try
                {
                    Task.Run(() =>
                    {
                        ChannelToDeviceDic = new Dictionary<string, Dictionary<string, List<string>>>();
                        List<string> MyTagList;
                        Dictionary<string, List<string>> MyDeviceToTagDic;
                        foreach (string Item in TagList)
                        {
                            char[] Separator = { '.' };
                            string[] StrArray = Item.Split(Separator);
                            string MyChannel = StrArray[0];
                            string MyDevice = StrArray[1];
                            string MyTag = Item;
                            if (!ChannelToDeviceDic.Keys.Contains(MyChannel))
                            {
                                MyDeviceToTagDic = new Dictionary<string, List<string>>();
                                ChannelToDeviceDic.Add(MyChannel, MyDeviceToTagDic);
                                MyTagList = new List<string>();
                                MyDeviceToTagDic.Add(MyDevice, MyTagList);
                                MyTagList.Add(MyTag);
                            }
                            else
                            {
                                MyDeviceToTagDic = ChannelToDeviceDic[MyChannel];
                                if (!MyDeviceToTagDic.Keys.Contains(MyDevice))
                                {
                                    MyTagList = new List<string>();
                                    MyDeviceToTagDic.Add(MyDevice, MyTagList);
                                    MyTagList.Add(MyTag);
                                }
                                else
                                {
                                    MyTagList = MyDeviceToTagDic[MyDevice];
                                    MyTagList.Add(MyTag);
                                }
                            }
                        }
                        List<string> MyChannelList = new List<string>();
                        foreach (string Channel in ChannelToDeviceDic.Keys)
                        {
                            MyChannelList.Add(Channel);
                        }
                        UIProperty.ChannelList = MyChannelList;

                    });
                }
                catch (Exception EX)
                {
                    LogHelp.Log.Error("读取服务出错！" + EX.Message);
                }

            }
            UIProperty.TagNameList = TagList;
        }

        public virtual void AddItem()
        {
            if (UIProperty.SelectedField == null || UIProperty.SelectedTag == null)
            {
                MyDelegate.dShowMessageBox("请先选择关联项！");
            }
            else
            {
                if (!DataInfo.BaseToServerDic.Keys.Contains(UIProperty.SelectedTable))
                {
                    FieldToTagDic = new Dictionary<string, string>();
                    DataInfo.BaseToServerDic.Add(UIProperty.SelectedTable, FieldToTagDic);
                }
                if (!FieldToTagDic.Keys.Contains(UIProperty.SelectedField))
                {
                    FieldToTagDic.Add(UIProperty.SelectedField, UIProperty.SelectedTag);
                    DataInfo.BaseToServerDic[UIProperty.SelectedTable] = FieldToTagDic;
                    UIProperty.ShowItemList.Add(UIProperty.SelectedField + "=" + FieldToTagDic[UIProperty.SelectedField]);
                }
                else
                {
                    MyDelegate.dShowMessageBox("此字段已被关联！\n关联字段:\n" + UIProperty.SelectedField + "=" + FieldToTagDic[UIProperty.SelectedField]);
                }
            }
        }

        public virtual void TableChanged()
        {
            UIProperty.ShowItemList = new List<string>();
            UIProperty.FieldNameList = new List<string>();
            if (DataInfo.BaseToServerDic.Keys.Contains(UIProperty.SelectedTable))
            {
                List<string> ShowList = new List<string>();
                FieldToTagDic = DataInfo.BaseToServerDic[UIProperty.SelectedTable];
                foreach (string Item in FieldToTagDic.Keys)
                {
                    ShowList.Add(Item + "=" + FieldToTagDic[Item]);
                }
                UIProperty.ShowItemList = ShowList;
                //MyDelegate.dUpdateViewList();
            }
        }

        public virtual void AutoPair()
        {
            if (DataInfo.TableToDeviceDic == null || DataInfo.TableToFieldListDic == null)
            {
                MyDelegate.dShowMessageBox("请先导入Excel表！");
            }
            else
            {
                DeviceTagList = new List<string>();
                TableFieldList = new List<string>();
                NeverRelationFieldList = new List<string>();
                if (UIProperty.SelectedTable == null || FieldList == null)
                {
                    MyDelegate.dShowMessageBox("请先选择待导入数据表并读取！");
                    return;
                }
                if (UIProperty.SelectedServerNode == null || TagList == null)
                {
                    MyDelegate.dShowMessageBox("请先选择KepServer节点并读取!");
                }
                if (DataInfo.TableToDeviceDic.Keys.Contains(UIProperty.SelectedTable))
                {
                    foreach (string Item in FieldList)
                    {
                        if (DataInfo.TableToFieldListDic[UIProperty.SelectedTable].Contains(Item))
                        {
                            TableFieldList.Add(Item);
                        }
                    }
                    foreach (string Item in TableFieldList)
                    {
                        if (!FieldToTagDic.Keys.Contains(Item))
                        {
                            NeverRelationFieldList.Add(Item);
                        }
                    }
                    foreach (string Item in TagList)
                    {
                        if (Item.Contains(DataInfo.TableToDeviceDic[UIProperty.SelectedTable]))
                        {
                            DeviceTagList.Add(Item);
                        }
                    }
                    int SuccessNum = 0;
                    foreach (string Field in NeverRelationFieldList)
                    {
                        foreach (string Tag in DeviceTagList)
                        {
                            if (Tag.Contains(DataInfo.TableToFieldToTagDicDic[UIProperty.SelectedTable][Field]))
                            {
                                FieldToTagDic.Add(Field, Tag);
                                SuccessNum++;
                            }
                        }
                    }
                    DataInfo.BaseToServerDic[UIProperty.SelectedTable] = FieldToTagDic;
                    List<string> ShowList = new List<string>();
                    foreach (string Item in FieldToTagDic.Keys)
                    {
                        ShowList.Add(Item + "=" + FieldToTagDic[Item]);
                    }
                    UIProperty.ShowItemList = ShowList;
                    //MyDelegate.dUpdateViewList();
                    MyDelegate.dShowMessageBox("共检测出" + NeverRelationFieldList.Count + "条待自动匹配字段，成功匹配了" + SuccessNum + "条！");


                }
                else
                {
                    MyDelegate.dShowMessageBox("导入的Excel表中不存在已选择的数据表名称！");
                }
            }
        }

        public virtual void ChannelChanged()
        {
            if (UIProperty.SelectedChannel != null)
            {
                List<string> MyDeviceList = new List<string>();
                foreach (string Item in ChannelToDeviceDic[UIProperty.SelectedChannel].Keys)
                {
                    MyDeviceList.Add(Item);
                }
                UIProperty.DeviceList = MyDeviceList;
            }
        }

        public virtual void DeviceChanged()
        {
            if (UIProperty.SelectedDevice != null)
            {
                UIProperty.TagNameList = ChannelToDeviceDic[UIProperty.SelectedChannel][UIProperty.SelectedDevice];
            }
        }

        public virtual void ServerChanged()
        {
            UIProperty.ChannelList = new List<string>();
            UIProperty.DeviceList = new List<string>();
            UIProperty.TagNameList = new List<string>();
        }

        public virtual void DeleteItem()
        {
            if (UIProperty.SelectedField == null || UIProperty.SelectedTag == null)
            {
                MyDelegate.dShowMessageBox("请先选择关联项！");
            }
            else
            {
                UIProperty.ShowItemList.Remove(UIProperty.SelectedField + "=" + FieldToTagDic[UIProperty.SelectedField]);
                FieldToTagDic.Remove(UIProperty.SelectedField);
                DataInfo.BaseToServerDic[UIProperty.SelectedTable] = FieldToTagDic;
            }
        }

        public virtual void Save()
        {
            //Dictionary<string, decimal> TagValueDic = new Dictionary<string, decimal>();
            DataInfo.SavedBaseToServerDic = DataInfo.BaseToServerDic;
            DataInfo.GatherTagList = new List<string>();
            List<string> GatherFieldList = new List<string>();
            DataInfo.TableToFieldIndexDic = new Dictionary<string, Dictionary<string,int>>();
            DataInfo.GatherTableToFeildList = new Dictionary<string, List<string>>();
            //UIProperty.AllItemList = new List<string>();
            TagFieldList = new List<TableFieldInfo>();
            DataInfo.TableToTempDataStr = new Dictionary<string, List<string>>();
            DataInfo.TableToDataStr = new Dictionary<string, string>();
            int FieldIndex = 0;
            foreach (string Table in DataInfo.SavedBaseToServerDic.Keys)
            {
                DataInfo.GatherFieldDic = new Dictionary<string, int>();
                DataInfo.TableToTempDataStr.Add(Table, null);
                DataInfo.TableToDataStr.Add(Table, "");
                Dictionary<string, string> FieldToTagDic = new Dictionary<string, string>();
                FieldToTagDic = DataInfo.BaseToServerDic[Table];
                List<string> OneTableField = new List<string>();
                foreach (string FieldName in DataInfo.SavedBaseToServerDic[Table].Keys)
                {
                    DataInfo.GatherFieldDic.Add(FieldName, FieldIndex);
                    DataInfo.GatherTagList.Add(DataInfo.SavedBaseToServerDic[Table][FieldName]);
                    GatherFieldList.Add(FieldName);
                    OneTableField.Add(FieldName);
                    FieldIndex++;
                }
                DataInfo.TableToFieldIndexDic.Add(Table, DataInfo.GatherFieldDic);
                foreach (string Field in FieldToTagDic.Keys)
                {
                    TagFieldList.Add(new TableFieldInfo
                    {
                        TableName = Table,
                        FieldName = Field,
                        TagName = FieldToTagDic[Field]
                    });
                    //UIProperty.AllItemList.Add(Table + "-" + Field + "=" + FieldToTagDic[Field]);
                }
                DataInfo.GatherTableToFeildList.Add(Table, OneTableField);
            }
            MyDelegate.dShowMainPage();
            MyDelegate.dShowItem(TagFieldList);
        }

        public virtual void TypeSave()
        {

        }

        public virtual void TypeDescriptionChanged()
        {
            char[] Separator = { '=' };
            if (UIProperty.SelectedView != null)
            {
                string[] StrArray = UIProperty.SelectedView.Split(Separator);
                UIProperty.WillDeleteItem = StrArray[0];
            }
        }

        public virtual void TypeDelete()
        {

        }

        public virtual void SelectedResult()
        {
            char[] Separator = { '=' };
            if (UIProperty.SelectedView != null)
            {
                string[] StrArray = UIProperty.SelectedView.Split(Separator);
                UIProperty.WillDeleteItem = StrArray[0];
            }
        }

        public virtual void ReturnExample()
        {
            DataInfo.BaseToServerDic = new Dictionary<string, Dictionary<string, string>>();
            DataInfo.TableToDescriptionToFieldDicDic = new Dictionary<string, Dictionary<int, string>>();
            DataInfo.PortraitTableToTagListDic = new Dictionary<string, List<string>>();
            UIProperty.TagNameList = new List<string>();
        }

        public virtual void WriteToDataPool()
        {
            foreach (string Table in DataInfo.SavedBaseToServerDic.Keys)
            {
                List<string> TempDataStrList = new List<string>();
                if (DataInfo.TableToTempDataStr[Table] == null)
                {
                    DataInfo.TableToTempDataStr[Table] = TempDataStrList;
                }
                DataInfo.TableToTempDataStr[Table].Add(XMLHelper.GetXmlStr(DataInfo.GatherTableToFeildList[Table], DataInfo.TableToFieldIndexDic[Table], OPCClient.TagValueArray));
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

        public virtual void ReadFile()
        {
            IsReadFlag = true;
            foreach (string Table in DataInfo.SavedBaseToServerDic.Keys)
            {
                string XmlStr = "";
                string TableField = "";
                string XmlFormat = "";
                foreach (string Field in DataInfo.GatherTableToFeildList[Table])
                {
                    TableField = TableField + Field + ",";
                    XmlFormat = XmlFormat + Field + " decimal(18,2),";
                }
                //string MyTableField = TableField.Remove(TableField.Count() - 1, 1);
                //string MyXmlFormat = XmlFormat.Remove(XmlFormat.Count() - 1, 1);
                string MyTableField = TableField + "createtime";
                string MyXmlFormat = XmlFormat + "createtime datetime";
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
                        ParamValue = MyXmlFormat
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

        public virtual void WriteSavedConfig()
        {
            ConfigInfo Config = new ConfigInfo();
            Config.KepServerIP = UIProperty.OPCServerIP;
            Config.DataBaseIP = UIProperty.DataBaseIP;
            Config.DataBaseUserName = UIProperty.DataBaseUserName;
            Config.DataBasePassword = UIProperty.DataBasePassWord;
            Config.DataBaseName = UIProperty.DataBaseName;
            Config.TableType = UIProperty.SelectTableType;
            string Result = XMLHelper.GetSaveXMLStr1(DataInfo.SavedBaseToServerDic, Config);
            FileHelper.WriteToFile(AppDomain.CurrentDomain.BaseDirectory.ToString()+"OTSV/SavedConfig.ini", Result);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdfFlatUI;
using Model.OTSM;
using BLL.OTSC;
using System.Data;
using Common.Log4net;

namespace OTS.OTSV
{
    
    /// <summary>
    /// PairWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PairWindow : DazzleWindow
    {
        OTSUIProperty UIProperty;
        OTSDataInfo DataInfo;
        string CurrentDevice = "";
        string CurrentTable = "";
        string Currentfield="";
        public PairWindow(OTSUIProperty UIProperty, OTSDataInfo DataInfo)
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.DataContext = UIProperty;
            this.DataInfo = DataInfo;
            
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ClickImport(object sender, RoutedEventArgs e)
        {
            DataInfo.TableToDeviceDic = new Dictionary<string, string>();
            DataInfo.TableToFieldListDic = new Dictionary<string, List<string>>();
            DataInfo.TableToTagListDic = new Dictionary<string, List<string>>();
            DataInfo.TableToFieldToTagDicDic = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, DataTable> TablesDic;
            TablesDic = ExcelHelper.ReadExcel(UIProperty.ExcelPath);
            if (TablesDic == null)
            {
                MessageBox.Show("Excel文件已打开或文件路径错误！");
            }
            else
            {
                try
                {
                    foreach (string Device in TablesDic.Keys)
                    {
                        CurrentDevice = Device;
                        string TableName = "";
                        DataTable Table = TablesDic[Device];
                        Dictionary<string, string> FieldToTagDic=new Dictionary<string, string>();
                        for (int i = 1; i < Table.Rows.Count; i++)
                        {
                            List<string> FieldList = new List<string>();
                            List<string> TagList = new List<string>();
                            List<string> RowElement = ExcelHelper.ReadOneRow(i, Table);
                            string Element1 = RowElement[1];
                            string Element2 = RowElement[2];
                            string Element3 = RowElement[3];
                            if (Element1 != "")
                            {
                                DataInfo.TableToDeviceDic.Add(Element1, Device);
                                FieldList = new List<string>();
                                TagList = new List<string>();
                                FieldToTagDic = new Dictionary<string, string>();
                                TableName = Element1;
                                CurrentTable = TableName;
                                DataInfo.TableToFieldListDic.Add(TableName, FieldList);
                                DataInfo.TableToTagListDic.Add(TableName, TagList);
                                DataInfo.TableToFieldToTagDicDic.Add(TableName, FieldToTagDic);
                            }
                            if (Element2 != "")
                            {
                                Currentfield = Element2;
                                DataInfo.TableToFieldListDic[TableName].Add(Element2);
                                DataInfo.TableToFieldToTagDicDic[TableName].Add(Element2, Element3);
                            }
                            if (Element3 != "")
                            {
                                DataInfo.TableToTagListDic[TableName].Add(Element3);
                            }
                        }
                    }
                    MessageBox.Show("导入成功！");
                    this.Hide();
                }
                catch (Exception EX)
                {
                    LogHelp.Log.Error("导入Excel表出错！" + EX.Message);
                    MessageBox.Show("导入失败！"+"错误出现在"+CurrentDevice+"-"+CurrentTable+"-"+Currentfield);
                }

            }
        }

    }
}

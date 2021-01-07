using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using System.Data;
using Common.Log4net;

namespace BLL.OTSC
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 键：Excel表中每张工作簿的名称
        /// 值：对应的工作表对象
        /// </summary>
        private static Dictionary<string, DataTable> DataTableDic;
        /// <summary>
        /// 读一张Excel表
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>工作表字典</returns>
        public static Dictionary<string,DataTable> ReadExcel(string FilePath)
        {
            try
            {
                DataTableDic = new Dictionary<string, DataTable>();
                var StreamData = File.Open(FilePath, FileMode.Open, FileAccess.Read);
                var ReadData = ExcelReaderFactory.CreateOpenXmlReader(StreamData);
                var Result = ReadData.AsDataSet();
                DataTableCollection Sheets = Result.Tables;
                foreach (DataTable Sheet in Sheets)
                {
                    DataTableDic.Add(Sheet.TableName, Sheet);
                }
                return DataTableDic;
            }
            catch (Exception e)
            {
                LogHelp.Log.Error("读取Excel表失败！"+e.Message);
                return null;
            }
        }



        /// <summary>
        /// 读取每一行的内容
        /// </summary>
        /// <param name="RowIndex">行索引</param>
        /// <param name="Table">工作表对象</param>
        /// <returns>所读行的内容列表</returns>
        public static List<string> ReadOneRow(int RowIndex, DataTable Table)
        {
            try
            {
                List<string> OneRowList = new List<string>();
                DataRowCollection Rows = Table.Rows;
                DataRow Row = Rows[RowIndex];
                foreach (object item in Row.ItemArray)
                {
                    OneRowList.Add(item.ToString());
                }
                return OneRowList;
            }
            catch(Exception E)
            {
                LogHelp.Log.Error("Excel读取行失败！" + E.Message);
                return null;
            }
        }

      
    }
}

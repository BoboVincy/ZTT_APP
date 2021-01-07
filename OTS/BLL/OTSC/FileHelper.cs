using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Common.Log4net;

namespace BLL.OTSC
{
    public class FileHelper
    {
        /// <summary>
        /// 新建一个文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        public static void CreateFile(string FilePath, string FileName)
        {
            try
            {
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.DirectoryInfo DI = new System.IO.DirectoryInfo(@FilePath);
                    DI.Create();
                }

                string OneFile = FileName;
                string OneFilePath = FilePath + "/" + FileName;
                if (!System.IO.File.Exists(OneFilePath))
                {
                    System.IO.File.Create(OneFilePath);
                }
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("新建文件失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 将数据写入到文件中
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="Text">需要写入的数据</param>
        /// <returns></returns>
        public static bool WriteToFile(string FilePath, string Text)
        {
            try
            {
                System.IO.File.WriteAllText(FilePath, Text);
                return true;
            }
            catch(Exception Ex)
            {
                LogHelp.Log.Error("写入文件失败！" + Ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 从文件中读取数据
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="Result">true：读取成功；false：读取失败</param>
        /// <returns></returns>
        public static bool ReadFromFile(string FilePath,out string Result)
        {
            try
            {
                Result = System.IO.File.ReadAllText(FilePath);
                return true;
            }
            catch(Exception ex)
            {
                LogHelp.Log.Error("读取文件失败！" + ex.Message);
                Result = "";
                return false;
            }
        }
    }
}

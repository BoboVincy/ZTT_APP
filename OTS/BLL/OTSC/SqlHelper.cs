using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Log4net;

namespace BLL.OTSC
{
    public class SqlHelper
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private SqlConnection SqlCon;

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="ServerIP">数据库IP</param>
        /// <param name="UserName">用户名</param>
        /// <param name="Pwd">密码</param>
        /// <param name="DataBase">数据库名称</param>
        /// <returns></returns>
        public bool SqlOpen(string ServerIP, string UserName, string Pwd, string DataBase)
        {
            try
            {
                string SqlStr = string.Format("server={0};uid={1};pwd={2};Integrated Security=True;DataBase={3}", ServerIP, UserName, Pwd, DataBase);
                SqlCon = new SqlConnection(SqlStr);
                SqlCon.Open();
                return true;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("打开数据库出错！" + EX.Message);
                return false;
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns>true：关闭数据库成功 false：关闭数据库失败</returns>
        public bool SqlClose()
        {
            try
            {
                if (SqlCon == null)
                {
                    return false;

                }
                if (SqlCon.State == ConnectionState.Open || SqlCon.State == ConnectionState.Connecting)
                {
                    SqlCon.Close();
                    SqlCon.Dispose();
                }
                else
                {
                    if (SqlCon.State == ConnectionState.Closed)
                    {
                        return true;
                    }
                    if (SqlCon.State == ConnectionState.Broken)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("关闭数据库出错！" + EX.Message);
                return false;
            }
        }

        public bool ReConnect()
        {
            try
            {
                if (SqlCon.State == ConnectionState.Broken || SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("数据库重连失败！"+ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 更改数据库
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <returns></returns>
        public bool SqlModify(string SqlStr)
        {
            try
            {
                int num = 0;
                if (SqlCon == null)
                {
                    return false;
                }
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCommand sqlCommand = new SqlCommand(SqlStr, SqlCon);
                    num = sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    if (SqlCon.State == ConnectionState.Closed)
                    {
                        return false;
                    }
                    if (SqlCon.State == ConnectionState.Broken)
                    {
                        return false;
                    }
                    if (SqlCon.State == ConnectionState.Connecting)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("更改数据库出错！" + EX.Message);
                return false;
            }
        }

        /// <summary>
        /// 查询数据库并获取结果
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <returns>结果列表</returns>
        public List<string> GetSqlResult(string SqlStr)
        {
            List<string> TableList = new List<string>();
            try
            {
                if (SqlCon == null)
                {
                    return null;
                }
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCommand SqlCommand = new SqlCommand(SqlStr, SqlCon);
                    SqlDataReader SqlReader = SqlCommand.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        while (SqlReader.Read())
                        {
                            string Item = SqlReader.GetString(0);
                            TableList.Add(Item);
                        }
                    }
                }
                else
                {
                    if (SqlCon.State == ConnectionState.Closed)
                    {
                        return null;
                    }
                    if (SqlCon.State == ConnectionState.Broken)
                    {
                        return null;
                    }
                    if (SqlCon.State == ConnectionState.Connecting)
                    {
                        return null;
                    }
                }
                return TableList;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("查询数据库并获取结果出错！" + EX.Message);
                return null;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="ParamsList">参数列表</param>
        /// <returns></returns>
        public bool ExecutePro(string ProcName, List<ProcParamModel> ParamsList)
        {
            //string xml= "<root><Para EquipmentID=\"0\" MeasureID=\"100.000\" specification=\"2015\" Value=12.232/></root>";
            try
            {
                SqlParameter[] ParamArray = new SqlParameter[ParamsList.Count];
                for (int i = 0; i < ParamArray.Length; i++)
                {
                    ParamArray[i] = new SqlParameter(ParamsList[i].ParamName, ParamsList[i].ParamValue);
                }
                SqlCommand SqlCommand = SqlCon.CreateCommand();
                SqlCommand.CommandText = ProcName;
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.AddRange(ParamArray);
                int Result = SqlCommand.ExecuteNonQuery();
                if (Result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("执行存储过程出错！" + EX.Message);
                return false;
            }
        }

        
    }

    /// <summary>
    /// 存储过程参数类
    /// </summary>
    public class ProcParamModel
    {
        public string ParamName
        {
            get; set;
        }

        public string ParamValue
        {
            get; set;
        }
    }

}


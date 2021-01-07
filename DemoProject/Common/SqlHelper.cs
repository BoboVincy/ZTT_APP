using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class SqlHelper
    {
        private static SqlConnection SqlCon;

        public static bool SqlOpen(string SqlStr)
        {
            try
            {
                SqlCon = new SqlConnection(SqlStr);
                SqlCon.Open();
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }

        public static bool SqlClose()
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
                return false;
            }
        }

        public static bool SqlModify(string SqlStr)
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Common.Log4net
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public static class Logs
    {
        #region 提示
        /// <summary>
        /// 打印提示
        /// </summary>
        /// <param name="txt"></param>
        public static void Info(string txt)
        {
            ILog log = log4net.LogManager.GetLogger("loginfo");
            if (log.IsInfoEnabled) log.Info(txt);
        }

        /// <summary>
        /// 打印提示
        /// </summary>
        /// <param name="type"></param>
        /// <param name="txt"></param>
        public static void Info(Type type, string txt)
        {
            ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsInfoEnabled) log.Info(txt);
        }
        #endregion
        #region 警告
        /// <summary>
        /// 打印警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            ILog log = log4net.LogManager.GetLogger("logwarn");
            if (log.IsWarnEnabled) log.Warn(msg);
        }

        /// <summary>
        /// 打印警告
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public static void Warn(Type type, string msg)
        {
            ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsWarnEnabled) log.Warn(msg);
        }

        /// <summary>
        /// 打印警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(string msg, Exception ex)
        {
            ILog log = log4net.LogManager.GetLogger("logwarn");
            if (log.IsWarnEnabled) log.Warn(msg, ex);
        }
        #endregion
        #region 错误
        /// <summary>
        /// 打印错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            ILog log = log4net.LogManager.GetLogger("logerror");
            if (log.IsErrorEnabled) log.Error(msg);
        }

        /// <summary>
        /// 打印错误
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public static void Error(Type type, string msg)
        {
            ILog log = log4net.LogManager.GetLogger(type);
            if (log.IsErrorEnabled) log.Error(msg);
        }

        /// <summary>
        /// 打印错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex)
        {
            ILog log = log4net.LogManager.GetLogger("logerror");
            if (log.IsErrorEnabled) log.Error(msg, ex);
        }
        #endregion
    }
}

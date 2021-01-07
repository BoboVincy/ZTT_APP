using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using log4net;

namespace Common.Log4net
{
    /// <summary>
    /// 日志初始化
    /// </summary>
    public class LogFactory
    {
        /// <summary>
        /// 反射初始化Log4net(全局设置)
        /// </summary>
        static LogFactory()
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //var xml = assembly.GetManifestResourceStream("IMS.Core.Common.Log.Log4net.config");
            //log4net.Config.XmlConfigurator.Configure(xml);

            //FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("/XmlConfig/log4net.config"));
            //log4net.Config.XmlConfigurator.Configure(configFile);

            //FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("/ExtUrl/Common/Log4net.config"));
            //log4net.Config.XmlConfigurator.Configure(configFile);

            FileInfo configFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 初始化Log4net(全局设置)
        /// </summary>
        /// <param name="path"></param>
        public static void InitLog4net(string path)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }

        /// <summary>
        /// 初始化Log4net(全局设置)
        /// </summary>
        /// <param name="xml"></param>
        public static void InitLog4net(Stream xml)
        {
            log4net.Config.XmlConfigurator.Configure(xml);
        }

        /// <summary>
        /// 利用Action委托封装LOG4NET对方法的处理方法
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="function">方法名</param>
        /// <param name="errorHandle">异常处理方法</param>
        /// <param name="tryHandle">调试/运行代码</param>
        /// <param name="catchHandle">异常处理方式</param>
        /// <param name="finallyHandle">最终处理方式</param>
        public static void Logger(ILog log, string function, ErrorHandle errorHandle, Action tryHandle, Action<Exception> catchHandle = null, Action finallyHandle = null)
        {
            try
            {
                log.Debug(function);
                tryHandle();
            }
            catch (Exception ex)
            {
                log.Error(function + "失败", ex);
                if (catchHandle != null)
                {
                    catchHandle(ex);
                }
                if (errorHandle == ErrorHandle.Throw)
                {
                    //throw ex;
                }
            }
            finally
            {
                if (finallyHandle != null)
                {
                    finallyHandle();
                }
            }
        }
    }

    public class LogFactory<T>
    {
        //protected static ILog log = LogManager.GetLogger(string.Format("DAL_{0}", typeof(T).Name));
        //protected static ILog log = LogManager.GetLogger("AdoNetAppender");
        //protected static ILog log = LogManager.GetLogger("AdoNetAppender_SqlServer");
        protected static ILog log = LogManager.GetLogger("RollingLogFileAppender");

        protected static void Logger(string function, Action tryHandle, Action<Exception> catchHandle = null, Action finallyHandle = null)
        {
            LogFactory.Logger(log, function, ErrorHandle.Throw, tryHandle, catchHandle, finallyHandle);
        }
    }

    public enum ErrorHandle
    {
        Throw,
        Continue
    }
}

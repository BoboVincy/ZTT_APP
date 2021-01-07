using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: XmlConfigurator(ConfigFile = "Log4net/log4net.config", Watch = true)]
namespace Common.Log4net
{
    public class LogHelp
    {
        public readonly static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}

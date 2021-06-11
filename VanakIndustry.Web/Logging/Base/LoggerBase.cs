using NLog;
using NLog.Config;

namespace VanakIndustry.Web.Logging.Base
{
    public abstract class LoggerBase
    {
        static LoggerBase()
        {
            LogManager.ThrowConfigExceptions = true;
            LogManager.Configuration = new LoggingConfiguration();
        }
    }
}

using System;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using VanakIndustry.Web.Logging.Base;

namespace VanakIndustry.Web.Logging.Loggers
{
    public class DebugLogger : LoggerBase
    {
        private LogLevel _level = LogLevel.Debug;
        private string _name = "*";
        private Layout _layout;

        public static DebugLogger Create()
        {
            return new DebugLogger();
        }

        private DebugLogger()
        {
        }

        public DebugLogger Layout(string logLayout)
        {
            _layout = logLayout;
            return this;
        }

        public DebugLogger Level(string logLevel)
        {
            _level = LogLevel.FromString(logLevel);
            return this;
        }

        public DebugLogger Level(LogLevel logLevel)
        {
            _level = logLevel;
            return this;
        }

        public DebugLogger Level(Enums.LogLevel logLevel)
        {
            _level = LogLevel.FromString(logLevel.ToString());
            return this;
        }

        public void Initialize()
        {
            Validate();
            GenerateLayout();

            var debuggerTarget = new ConsoleTarget {Layout = _layout};

            LogManager.Configuration.AddTarget("debugger", debuggerTarget);
            LogManager.Configuration.LoggingRules.Add(new LoggingRule(_name, _level, debuggerTarget));
            LogManager.ReconfigExistingLoggers();
        }

        private void Validate()
        {
            if (_level == null) throw new Exception("The log level was not specified.");
        }

        private void GenerateLayout()
        {
            if (_layout == null)
            {
                _layout = @"***** ${level:upperCase=true}: ${message} [${date:format=dd.MM.yyyy HH\:mm\:ss}] (${callsite})";
            }
        }
    }
}
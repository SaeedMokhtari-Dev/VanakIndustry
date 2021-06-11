using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using VanakIndustry.Web.Logging.Base;
using VanakIndustry.Web.Logging.Services;

namespace VanakIndustry.Web.Logging.Loggers
{
 public class FileLogger : LoggerBase
    {
        private string _extension = ".log";
        private string _directory;
        private string _filename = $"{DateTime.UtcNow:yyyyMMddHHmmss}";
        private LogLevel _level = LogLevel.Info;
        private string _name = "*";
        private int _keepMaxLogs = 20;
        private bool _deleteObsoleteLogs = true;
        private string _filePath;
        private Layout _layout;

        public static FileLogger Create()
        {
            return new FileLogger();
        }

        private FileLogger()
        {
        }

        public FileLogger Directory(string logDirectory)
        {
            _directory = logDirectory;
            return this;
        }

        public FileLogger Filename(string logFilename)
        {
            _filename = logFilename;
            return this;
        }

        public FileLogger Level(string logLevel)
        {
            _level = LogLevel.FromString(logLevel);
            return this;
        }

        public FileLogger Level(LogLevel logLevel)
        {
            _level = logLevel;
            return this;
        }

        public FileLogger Level(Enums.LogLevel logLevel)
        {
            _level = LogLevel.FromString(logLevel.ToString());
            return this;
        }

        public FileLogger Layout(string logLayout)
        {
            _layout = logLayout;
            return this;
        }

        public FileLogger Name(string loggerName)
        {
            _name = loggerName;
            return this;
        }

        public FileLogger Extension(string filenameExtension)
        {
            _extension = filenameExtension;
            return this;
        }

        public FileLogger KeepMaxLogs(int maxlogsToKeep)
        {
            _keepMaxLogs = maxlogsToKeep;
            return this;
        }

        public FileLogger DeleteObsoleteLogs(bool deleteLogs)
        {
            _deleteObsoleteLogs = deleteLogs;
            return this;
        }

        public void Initialize()
        {
            Validate();

            SetupLogger();

            if (_deleteObsoleteLogs)
            {
                LogCleaner.CleanLogs(_directory, _keepMaxLogs, _extension);
            }
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_directory)) throw new Exception("The log directory was not specified.");

            if (string.IsNullOrWhiteSpace(_filename)) throw new Exception("The log filename was not speicified.");

            if (_keepMaxLogs < 1) throw new Exception("The minimum value for KeepMaxLogs is 1");

            if (_level == null) throw new Exception("The log level was not specified.");

            if (_extension == null) throw new Exception("The log filename extension is required.");
        }

        private void SetupLogger()
        {
            GenerateFilepath();

            GenerateLayout();

            var target = new FileTarget { Layout = _layout, FileName = _filePath };

            LogManager.Configuration.AddTarget("file", target);
            LogManager.Configuration.LoggingRules.Add(new LoggingRule(_name, _level, target));
            LogManager.ReconfigExistingLoggers();
        }

        private void GenerateFilepath()
        {
            _filePath = $"{Path.Combine(_directory, _filename)}{_extension}";
        }

        private void GenerateLayout()
        {
            if (_layout == null)
            {
                _layout = LogLayoutProvider.DefaultFileLayout();
            }
        }
    }
}
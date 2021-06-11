using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace VanakIndustry.Web.Logging.Services
{
    public static class LogCleaner
    {
        private static ILogger Logger => LogManager.GetCurrentClassLogger();

        public static void CleanLogs(string directory, int keepMaxLogs, string extension = null)
        {
            try
            {
                Logger.Debug("Checking for older log files to be deleted");

                var files = GetFiles(directory, extension);

                var skipFiles = Math.Max(1, keepMaxLogs);

                if (files.Count <= skipFiles) return;

                var deleteCount = files.Count - skipFiles;

                Logger.Debug($"Deleting {deleteCount} log file(s)");

                files.Skip(skipFiles).ToList().ForEach(TryDeleteLogFile);

                Logger.Debug("Finished deleting the older log files");
            }
            catch (Exception ex)
            {
                Logger.Debug(ex, "Failed to delete the older log files");
            }
        }

        private static List<FileSystemInfo> GetFiles(string directory, string extension)
        {
            var files = new DirectoryInfo(directory).GetFileSystemInfos().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(extension))
            {
                files = files.Where(x => x.Name.EndsWith(extension));
            }

            return files.OrderByDescending(x => x.CreationTime).ToList();
        }

        private static void TryDeleteLogFile(FileSystemInfo file)
        {
            try
            {
                File.Delete(file.FullName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to delete the log file {file}");
            }
        }
    }
}
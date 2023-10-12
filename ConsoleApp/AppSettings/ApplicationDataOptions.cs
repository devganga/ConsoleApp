using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.AppSettings
{
    public class ApplicationDataOptions
    {
        public string? FolderName { get; set; } = "iRobot";
        //public int SpecialFolder { get; set; }
        //public string? TempFolderPath { get; set; } = Path.GetTempPath();
        //public string? ApplicationDataPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string? GetTempFileName()
        {
            return Path.GetTempFileName();
        }
        public string GetApplicationTempPath()
        {
            var path = Path.Combine(Path.GetTempPath(), FolderName!);
            Directory.CreateDirectory(path);
            return path;

            //return FolderName == null
            //    ? Path.Combine(Environment.GetFolderPath(ApplicationDataFolder), FolderName!)
            //    : $"Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _options.FolderName ?? \"ConsoleApp\"";
        }
        public string GetApplicationDataPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FolderName!);
            Directory.CreateDirectory(path);
            return path;

            //return FolderName == null
            //    ? Path.GetTempPath()
            //    : $"Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _options.FolderName ?? \"ConsoleApp\"";
        }
    }
}

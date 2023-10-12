using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleApp.AppSettings
{
    public class AdrenalinOptions
    {
        public string? Url { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }

        public override string ToString()
        {

            var tempFile = Path.GetTempFileName();
            
            FileInfo fileInfo = new FileInfo(tempFile);
            fileInfo.Attributes = FileAttributes.Temporary;

            //fileInfo.AppendText().we;
            //fileInfo.
            //FileAttributes.te
            return $"Accessing url {Url} and username {Username} & password {Password} in the company is {Company}";
        }
        public AdrenalinOptions()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleApp.AppSettings
{
    public class SampleOptions
    {
        public int Id { get; set; }
        public string? Secret { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Secret}";
        }
    }
}

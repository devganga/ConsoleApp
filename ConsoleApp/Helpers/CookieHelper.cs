using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public class CookieHelper
    {
        public CookieContainer CookieContainer { get; set; }
        public CookieHelper()
        {
            CookieContainer = new CookieContainer();
        }

    }
}

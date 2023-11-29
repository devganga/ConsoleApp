using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public sealed class CookieHelper
    {
        //public CookieContainer CookieContainer { get; set; }

        private static readonly Lazy<CookieHelper> _lazy = new Lazy<CookieHelper>(() => new CookieHelper());

        public static CookieHelper Instance
        {
            get { return _lazy.Value; }
        }
        private CookieHelper()
        {
            //CookieContainer = new CookieContainer();
        }

        public bool IsValueCreated
        {
            get
            {
                return _lazy.IsValueCreated;
            }
        }

    }
}

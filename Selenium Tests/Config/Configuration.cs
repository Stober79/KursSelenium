using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.Config
{
    class Configuration
    {
        public string Browser { get; set; }
        public bool IsRemote{ get; set; }
        public Uri RemoteAddress{ get; set; }
        public string PlatformName { get; set; }
        public string BaseUrl{ get; set; }
    }
}

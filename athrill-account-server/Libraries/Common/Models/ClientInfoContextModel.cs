using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Models
{
    public class ClientInfoContextModel
    {
        public string IPAddress { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public string BroswerVersion { get; set; }
        public string RouteUrl { get; set; }
        public ClientApplication ClientApplication { get; set; }
        public Guid BatchId { get; set; }
    }
}

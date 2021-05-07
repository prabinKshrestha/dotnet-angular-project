using AT.Common.Enum;
using AT.Common.Infrastructure.Interfaces;
using AT.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Infrastructure
{
    public class ClientInfoContext : IClientInfoContext
    {
        public string IPAddress { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public string BroswerVersion { get; set; }
        public string RequestUrl { get; set; }
        public ClientApplication ClientApplication { get; set; }
        public Guid BatchId { get; set; }

        public void SetClientInfoContext(ClientInfoContextModel contextModel)
        {
            this.IPAddress = contextModel.IPAddress;
            this.Device = contextModel.Device;
            this.Browser = contextModel.Browser;
            this.BroswerVersion = contextModel.BroswerVersion;
            this.RequestUrl = contextModel.RouteUrl;
            this.ClientApplication = contextModel.ClientApplication;
            this.BatchId = contextModel.BatchId;
        }

        public string GetClientInfoName()
        {
            return $"DeviceType|{Device},BrowserType|{Browser},BroswerVersion|{BroswerVersion}";
        }
    }
}

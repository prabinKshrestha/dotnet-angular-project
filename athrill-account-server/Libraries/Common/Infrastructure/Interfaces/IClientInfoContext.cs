using AT.Common.Enum;
using AT.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Infrastructure.Interfaces
{
    public interface IClientInfoContext
    {
        string IPAddress { get; set; }
        string Device { get; set; }
        string Browser { get; set; }
        string BroswerVersion { get; set; }
        string RequestUrl { get; set; }
        ClientApplication ClientApplication { get; set; }
        Guid BatchId { get; set; }
        void SetClientInfoContext(ClientInfoContextModel contextModel);
        string GetClientInfoName();
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Configuration
{
    public static class JsonConfiguration
    {
        public static void JsonConfig(this MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // Pascal case becuase we need this in angular client side
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}

using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Configuration
{
    public static class FilterConfiguration
    {
        public static void GlobalFiltersConfiguration(this MvcOptions opt)
        {
            opt.Filters.Add(new ExceptionHandlerFilter());
        }
    }
}

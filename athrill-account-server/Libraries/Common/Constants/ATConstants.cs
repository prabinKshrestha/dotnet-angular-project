using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Constants
{
    public static class ATConstants
    {
    }
    public static class CustomClaimTypes
    {
        public const string CLAIM_USER_ID = "UserId";
    }
    public static class ATAppContext
    {
        public const string AT_CLIENT = "aa-client";
        public const string AT_SUPPORT_SITE = "aa-support-site";
        public const string AT_MOBILE = "aa-mobile";

        public static Dictionary<string , ClientApplication> ClientApplicationEnumHeaderValuePair = new Dictionary<string, ClientApplication>()
        {
            { AT_SUPPORT_SITE      ,        ClientApplication.SupportSite },
            { AT_CLIENT            ,        ClientApplication.Client  },
            { AT_MOBILE            ,        ClientApplication.Mobile  }
        };
    }

}

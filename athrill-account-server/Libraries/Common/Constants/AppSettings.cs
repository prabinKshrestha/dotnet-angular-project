using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Constants
{
    public static class AppSettings
    {
        public static ATFeatureAppSettings Features { get; set; }
    }

    public class ATFeatureAppSettings
    {
        public bool SiteSetting { get; set; }
        public bool EmailSetting { get; set; }
        public bool User { get; set; }
        public bool UserTrackRecord { get; set; }
        public bool RecordLogRecord { get; set; }
    }
}

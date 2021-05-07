using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Settings.EmailSettings
{
    public class EmailSetting : BaseEntity, ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int EmailSettingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [RecordLogIgnore]
        public string Password { get; set; }
        public string SendFromName { get; set; }
        public string ReplyToName { get; set; }
        public string ReplyToEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }

        #region overrride
        public override int Id => EmailSettingId;
        public override string ToString()
        {
            return Name;
        }
        #endregion

    }
}

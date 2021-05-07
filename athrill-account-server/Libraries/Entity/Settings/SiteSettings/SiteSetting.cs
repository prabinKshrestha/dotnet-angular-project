using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Settings.SiteSettings
{
    public class SiteSetting : BaseEntity, IImageEntity
    {
        [RecordLogIgnore]
        public int SiteSettingId { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteUrl { get; set; }   
        public string ImageName { get; set; }
        public string FeedbackEmail { get; set; }
        public string CopyrightName { get; set; }
        public string WorkHours { get; set; }
        public string AddressShortDetail { get; set; }
        public string AddressDetail { get; set; }
        public string LocationIframe { get; set; }
        public string FacebookLink { get; set; }
        public string YoutubeLink { get; set; }
        public string TwiterLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
        public string SkypeLink { get; set; }
        public string PhoneNumber { get; set; }
        public string Viber { get; set; }
        public string Whatsapp { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaImageName { get; set; }

        #region override
        public override int Id => SiteSettingId;
        #endregion

    }
}

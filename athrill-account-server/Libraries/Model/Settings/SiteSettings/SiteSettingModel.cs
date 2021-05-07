using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Settings.SiteSettings
{
    public class SiteSettingModel: BaseModel, IImageModel
    {
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
        public override int Id => SiteSettingId;
        public string FilePath => "images/sitesetting/";
        public string Image => string.Concat(FilePath, ImageName);
        public string MetaImage => string.Concat(FilePath, MetaImageName);

    }

    public class SiteSettingUpdateModel : BaseUpdateModel, IImageUpdateModel
    {
        public int SiteSettingId { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteUrl { get; set; }
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
        public IFormFile ImageFile { get; set; }
        public IFormFile MetaImageFile { get; set; }
        public string FilePath => "images/sitesetting/";
        public bool IsImageRequired => true;
        public bool IsImageChanged { get; set; }
        public bool IsMetaImageChanged { get; set; }
    }

    public class SiteSettingUpdateModelValidator : AbstractValidator<SiteSettingUpdateModel>
    {
        public SiteSettingUpdateModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.Name1).Length(1, 200);
            RuleFor(m => m.Name2).Length(1, 200);
            RuleFor(m => m.Name3).Length(1, 200);
            RuleFor(m => m.Name4).Length(1, 200);
            RuleFor(m => m.SiteSlogan).NotEmpty().NotNull().Length(1, 400);
            RuleFor(m => m.SiteUrl).NotEmpty().NotNull().Length(1, 200);
            RuleFor(s => s.FeedbackEmail).NotEmpty().NotNull().EmailAddress();
            RuleFor(m => m.CopyrightName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.WorkHours).Length(1, 200);
            RuleFor(m => m.AddressShortDetail).NotEmpty().NotNull().Length(1, 600);
            RuleFor(m => m.AddressDetail).NotEmpty().NotNull();
            RuleFor(m => m.PhoneNumber).NotEmpty().NotNull();
            RuleFor(m => m.MetaTitle).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.MetaKeywords).NotEmpty().NotNull().Length(1, 400);
            RuleFor(m => m.MetaDescription).NotEmpty().NotNull();
        }
    }
}

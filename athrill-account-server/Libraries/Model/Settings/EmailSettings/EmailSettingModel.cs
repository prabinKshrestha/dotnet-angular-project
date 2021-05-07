using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Settings.EmailSettings
{
    public class EmailSettingModel : BaseModel
    {
        public int EmailSettingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
        public override int Id => EmailSettingId;

    }

    public class EmailSettingAddModel : BaseAddModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
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
    }

    public class EmailSettingUpdateModel : BaseUpdateModel
    {
        public int EmailSettingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
    }

    public class EmailSettingAddModelValidator : AbstractValidator<EmailSettingAddModel>
    {
        public EmailSettingAddModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.Email).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(m => m.Password).NotEmpty().NotNull();
            RuleFor(m => m.SendFromName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ReplyToName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ReplyToEmail).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(m => m.Host).NotNull().NotEmpty();
            RuleFor(m => m.Port).NotNull();
            RuleFor(m => m.IsSSL).NotNull();
            RuleFor(m => m.IsDefault).NotNull();
            RuleFor(m => m.Description).NotEmpty().NotNull();
            RuleFor(m => m.IsPublished).NotNull();
        }
    }

    public class EmailSettingUpdateModelValidator : AbstractValidator<EmailSettingUpdateModel>
    {
        public EmailSettingUpdateModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.Email).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(m => m.Password).NotEmpty().NotNull();
            RuleFor(m => m.SendFromName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ReplyToName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ReplyToEmail).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(m => m.Host).NotNull().NotEmpty();
            RuleFor(m => m.Port).NotNull();
            RuleFor(m => m.IsSSL).NotNull();
            RuleFor(m => m.IsDefault).NotNull();
            RuleFor(m => m.Description).NotEmpty().NotNull();
            RuleFor(m => m.IsPublished).NotNull();
        }
    }
}

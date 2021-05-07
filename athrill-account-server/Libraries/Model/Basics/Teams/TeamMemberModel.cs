using AT.Common.Attributes;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Basics.Teams
{
    public class TeamMemberModel : BaseModel, IImageModel
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Quotation { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string Twiterlink { get; set; }
        public string Viber { get; set; }
        public bool IsPublished { get; set; }
        public string FilePath => "images/teams/";
        public string Image => $"{FilePath}{ImageName}";

        #region overrride

        public override int Id => TeamMemberId;
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Links

        public ICollection<TeamCategoryMemberLinkModel> TeamCategoryMemberLinks { get; set; }

        #endregion
    }
    public class TeamMemberAddModel : BaseAddModel, IImageAddModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Quotation { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string Twiterlink { get; set; }
        public string Viber { get; set; }
        public bool IsPublished { get; set; }

        [FromFormData]
        public List<int> TeamCategoryIds { get; set; }
        public string FilePath => "images/teams/";
        public IFormFile ImageFile { get; set; }
        public bool IsImageRequired => true;
    }
    public class TeamMemberUpdateModel : BaseUpdateModel, IImageUpdateModel
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Quotation { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string Twiterlink { get; set; }
        public string Viber { get; set; }
        public bool IsPublished { get; set; }

        [FromFormData]
        public List<int> TeamCategoryIds { get; set; }
        public string FilePath => "images/teams/";
        public IFormFile ImageFile { get; set; }
        public bool IsImageRequired => true;
        public bool IsImageChanged { get; set; }
    }
    public class TeamMemberAddModelValidator : AbstractValidator<TeamMemberAddModel>
    {
        public TeamMemberAddModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.Role).Length(1, 400);
            RuleFor(m => m.Quotation).Length(1, 400);
            RuleFor(m => m.Position).Length(1, 400);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(s => s.Email).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(s => s.IsPublished).NotNull();
        }
    }

    public class TeamMemberUpdateModelValidator : AbstractValidator<TeamMemberUpdateModel>
    {
        public TeamMemberUpdateModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.Role).Length(1, 400);
            RuleFor(m => m.Quotation).Length(1, 400);
            RuleFor(m => m.Position).Length(1, 400);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(s => s.Email).NotEmpty().NotNull().Length(1, 100).EmailAddress();
            RuleFor(s => s.IsPublished).NotNull();
        }
    }
}

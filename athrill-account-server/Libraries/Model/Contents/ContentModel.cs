using AT.Model.ATDatas;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Contents
{
    public class ContentModel: BaseModel, IImageModel
    {
        public int ContentId { get; set; }
        public int ContentTypeId { get; set; }
        public int? ParentId { get; set; }
        public int PlacementId { get; set; }
        public int Position { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string ImageName { get; set; }
        public string ImageAltName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ExternalUrl { get; set; }
        public string FontAwesomeIcon { get; set; }
        public bool IsPublished { get; set; }
        public override int Id => ContentId;
        public string FilePath => "images/content/";
        public string Image => string.Concat(FilePath, ImageName);
        public  ContentTypeModel ContentType { get; set; }
        public  ContentModel Parent { get; set; }
        public  ATDataValueModel Placement { get; set; }
        public  ICollection<ContentModel> Childrens { get; set; }
    }

    public class ContentAddModel : BaseAddModel, IImageAddModel
    {
        public int ContentTypeId { get; set; }
        public int? ParentId { get; set; }
        public int PlacementId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string ImageAltName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ExternalUrl { get; set; }
        public string FontAwesomeIcon { get; set; }
        public bool IsPublished { get; set; }
        public IFormFile ImageFile { get; set; }

        public string FilePath => "images/content/";
        public bool IsImageRequired => false;
    }

    public class ContentUpdateModel : BaseUpdateModel, IImageUpdateModel
    {
        public int ContentId { get; set; }
        public int ContentTypeId { get; set; }
        public int? ParentId { get; set; }
        public int PlacementId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string ImageAltName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ExternalUrl { get; set; }
        public string FontAwesomeIcon { get; set; }
        public bool IsPublished { get; set; }
        public IFormFile ImageFile { get; set; }
        public string FilePath => "images/content/";
        public bool IsImageRequired => false;
        public bool IsImageChanged { get; set; }
    }

    public class ContentAddModelValidator : AbstractValidator<ContentAddModel>
    {
        public ContentAddModelValidator()
        {
            RuleFor(m => m.ContentTypeId).NotNull().NotEmpty();
            RuleFor(m => m.PlacementId).NotNull().NotEmpty();
            RuleFor(m => m.Slug).Length(1, 200);
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.SubName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ImageAltName).Length(1, 200);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(m => m.IsPublished).NotNull();
        }
    }

    public class ContentUpdateModelValidator : AbstractValidator<ContentUpdateModel>
    {
        public ContentUpdateModelValidator()
        {
            RuleFor(m => m.ContentTypeId).NotNull().NotEmpty();
            RuleFor(m => m.PlacementId).NotNull().NotEmpty();
            RuleFor(m => m.Slug).Length(1, 200);
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.SubName).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ImageAltName).Length(1, 200);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(m => m.IsPublished).NotNull();
        }
    }
}

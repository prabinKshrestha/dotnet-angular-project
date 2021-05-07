using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Basics.Teams
{
    public class TeamCategoryModel : BaseModel
    {
        public int TeamCategoryId { get; set; }
        public int Orientation { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }

        #region Override

        public override int Id => TeamCategoryId;
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Links

        public ICollection<TeamCategoryMemberLinkModel> TeamCategoryMemberLinks { get; set; }

        #endregion
    }

    public class TeamCategoryAddModel : BaseAddModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }

    public class TeamCategoryUpdateModel : BaseUpdateModel
    {
        public int TeamCategoryId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }

    public class TeamCategoryAddModelValidator : AbstractValidator<TeamCategoryAddModel>
    {
        public TeamCategoryAddModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(m => m.IsPublished).NotNull();
        }
    }

    public class TeamCategoryUpdateModelValidator : AbstractValidator<TeamCategoryUpdateModel>
    {
        public TeamCategoryUpdateModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().NotNull().Length(1, 200);
            RuleFor(m => m.ShortDescription).Length(1, 600);
            RuleFor(m => m.IsPublished).NotNull();
        }
    }
}

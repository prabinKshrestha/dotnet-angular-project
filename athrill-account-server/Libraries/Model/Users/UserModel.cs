using AT.Model.ATDatas;
using System;
using System.Collections.Generic;

namespace AT.Model.Users
{
    public class UserModel : BaseModel, IImageModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string FilePath => "images/user/";
        public string Image => string.Concat(FilePath, ImageName);
        public string FullName => $"{FirstName} {MiddleName} {LastName}";


        #region Links 
        public UserLoginModel UserLogin { get; set; }
        public UserRoleLinkModel UserRoleLink { get; set; }
        public ATDataValueModel Gender { get; set; }
        #endregion


        #region override
        public override int Id => UserId;
        public override string ToString()
        {
            return string.Concat(FirstName, MiddleName, LastName);
        }
        #endregion
    }

}


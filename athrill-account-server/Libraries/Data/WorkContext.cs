using AT.Common.Models;
using AT.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data
{
    public class WorkContext : IWorkContext
    {
        public int UserLoginId { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }

        public void SetWorkContext(WorkContextModel workContextModel)
        {
            this.UserLoginId = workContextModel.UserLoginId;
            this.UserId = workContextModel.UserId;
            this.UserRoleId = workContextModel.UserRoleId;
        }
    }
}

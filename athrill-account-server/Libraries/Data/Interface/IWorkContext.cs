using AT.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Interface
{
    public interface IWorkContext
    {
        int UserLoginId { get; set; }
        int UserId { get; set; }
        int UserRoleId { get; set; }
        void SetWorkContext(WorkContextModel workContextModel);
    }
}

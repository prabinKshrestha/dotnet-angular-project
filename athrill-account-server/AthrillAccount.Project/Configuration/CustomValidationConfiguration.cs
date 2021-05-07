using AT.Common.Enum;
using AT.Model;
using AT.Model.Exceptions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Configuration
{
    public static class CustomValidationConfiguration
    {
        public static void RegisterValidators(this FluentValidationMvcConfiguration opt)
        {
            opt.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
            opt.RegisterValidatorsFromAssembly(typeof(BaseModel).GetTypeInfo().Assembly);
        }
        public static void CustomValidationConfig(this ApiBehaviorOptions opt)
        {
            opt.InvalidModelStateResponseFactory = c =>
            {
                List<ATBusinessExceptionMessageModel> violations = new List<ATBusinessExceptionMessageModel>();
                c.ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList()
                                                                .ForEach(y =>
                                                                {
                                                                    violations.Add(new ATBusinessExceptionMessageModel()
                                                                    {
                                                                        ErrorLevel = ATErrorLevel.Error,
                                                                        Message = y,
                                                                        TargetType = null,
                                                                    });
                                                                });
                return new BadRequestObjectResult(new ATBusinessExceptionModel
                {
                    Message = "Business Rule Violation Occurred.",
                    Validations = violations
                });
            };
        }
    }
}

using AT.Common.Enum;
using AT.Service.System.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            IATLogger logger = context.HttpContext.RequestServices.GetService(typeof(IATLogger)) as ATLogger;
            Exception exception = context.Exception.GetBaseException();
            Type exceptionType = exception.GetType();

            List<Type> selectedExceptionType = new List<Type>()
            {
                typeof(OperationCanceledException),
                typeof(TaskCanceledException)
            };

            if (selectedExceptionType.Contains(exceptionType))
            {
                logger.LogExceptionToFile(ATExceptionTypes.Unkown, "Global Exception Handler: OperationCanceledException or TaskCanceledException", exception);
            }
            else if(exceptionType == typeof(SqlException))
            {
                logger.LogExceptionToFile(ATExceptionTypes.SqlException, "Global Exception Handler : Sql Exception", exception, sendMessage: true);
            }
            else
            {
                logger.LogExceptionToFile(ATExceptionTypes.Unkown, "Global Exception Handler : Unkown Exception", exception, sendMessage: true);
            }
            context.Result = new ContentResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Content = "An error has occurred. Please try again or contact administatrator."
            };
        }
    }
}

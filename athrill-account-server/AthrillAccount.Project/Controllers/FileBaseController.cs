using AT.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Controllers
{
    public class FileBaseController : ApiBaseController
    {
        protected ActionResult Download(string fileName, string filePath)
        {
            return ResponseWrapper(() =>
            {
                string filepath = Path.Combine($"{PUBLIC_PATH}{filePath}{fileName}");
                if (!System.IO.File.Exists(filepath))
                {
                    throw new ATBusinessException($"File {fileName} not found.");
                }
                MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("application/octet-stream");
                FileContentResult retVal = new FileContentResult(System.IO.File.ReadAllBytes(filepath), mediaTypeHeaderValue)
                {
                    FileDownloadName = fileName
                };
                return retVal;
            });
        }
    }
}

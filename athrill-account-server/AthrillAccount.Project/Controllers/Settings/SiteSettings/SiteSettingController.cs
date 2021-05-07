using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Entity.Settings.SiteSettings;
using AT.Model;
using AT.Model.Settings.SiteSettings;
using AT.Service.Settings.SiteSettings;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AthrillAccount.Project.Controllers.Settings.SiteSettings
{
    [Route("sitesetting")]
    [ApiController]
    public class SiteSettingController : ApiBaseController
    {
        private readonly ISiteSettingService _siteSettingService;

        public SiteSettingController(ISiteSettingService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }

        /// <summary>
        ///  Get Site Setting
        /// </summary>
        /// <returns>Single Site Setting</returns>
        [HttpGet]
        public ActionResult<SiteSettingModel> GetSiteSetting()
        {
            return SimpleGetResponse<SiteSetting, SiteSettingModel>(_siteSettingService, (int)ATSettings.SiteSettingId);
        }

        /// <summary>
        /// Update Site Setting
        /// </summary>
        /// <returns>Returns Site Setting</returns>
        [HttpPut]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult<SiteSettingModel> UpdateSiteSetting([FromForm] SiteSettingUpdateModel model)
        {
            return ResponseWrapper<SiteSettingModel>(() =>
            {
                if (model.IsMetaImageChanged)
                {
                    if (model.MetaImageFile == null && model.MetaImageFile.Length == 0)
                    {
                        throw new ATBusinessException("Meta image is required.");
                    }
                    string imageName = GenerateImageName(model.MetaImageFile);
                    ImageModel imageModel = new ImageModel(model.MetaImageFile, model.FilePath, imageName);
                    StoreImage(imageModel);
                    if (!string.IsNullOrWhiteSpace(model.MetaImageName))
                    {
                        string oldImagePath = Path.Combine(PUBLIC_PATH + model.FilePath, model.MetaImageName);
                        DeleteFile(oldImagePath);
                    }
                    model.MetaImageName = imageName;
                }
                return SimpleUpdateResponse<SiteSetting, SiteSettingModel, SiteSettingUpdateModel>((int)ATSettings.SiteSettingId, model, _siteSettingService);
            });
        }
    }
}
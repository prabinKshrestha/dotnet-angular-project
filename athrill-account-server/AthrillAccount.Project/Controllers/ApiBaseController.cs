using AthrillAccount.Project.AutoMapperProfile;
using AutoMapper;
using AT.Common.Exceptions;
using AT.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AT.Model;
using AT.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AT.Model.Exceptions;
using AT.Common.Api.Infrastructure;
using static AT.Common.Api.Constants.ApiConstants;
using AT.Common.Api.Request;
using AT.Common.Api.Helpers;
using AT.Common.Api.Constants;
using System.Linq.Dynamic.Core.Exceptions;
using Microsoft.Data.SqlClient;
using AT.Common.Enum;
using AT.Service.System.Loggers;
using Microsoft.EntityFrameworkCore;

namespace AthrillAccount.Project.Controllers
{
    public class ApiBaseController : ControllerBase, IApiBaseController
    {
        protected readonly IMapper _mapper;
        protected readonly string PUBLIC_PATH;
        private IATLogger _logger => Request.HttpContext.RequestServices.GetService(typeof(IATLogger)) as ATLogger;
        public ApiBaseController()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                PUBLIC_PATH = "wwwroot/";
            }
            else
            {
                PUBLIC_PATH = "wwwroot/";
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityToModelProfile>();
                cfg.AddProfile<ModelToEntityProfile>();
            });
            _mapper = config.CreateMapper();
        }
        public ActionResult<TModel> SimpleGetResponse<TEntity, TModel>(IBaseService<TEntity> baseService, int id)
            where TEntity : class
            where TModel : class
        {
            return ResponseWrapper<TModel>(() =>
            {
                TEntity entity = baseService.Get(id);
                return Ok(_mapper.Map<TEntity, TModel>(entity));
            });
        }
        public ActionResult<List<TModel>> SimpleGetResponse<TEntity, TModel>(IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
        {
            return ResponseWrapper<List<TModel>>(() =>
            {
                IEnumerable<TEntity> entity = baseService.GetAll();
                return Ok(_mapper.Map<List<TEntity>, List<TModel>>(entity.ToList()));
            });
        }

        public ActionResult<TModel> SimpleAddResponse<TEntity, TModel, TAddModel>(TAddModel model, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
            where TAddModel : class
        {
            return ResponseWrapper<TModel>(() => {
                TEntity entity = _mapper.Map<TAddModel, TEntity>(model);
                ImageModel imageModel = null;
                if (entity is IImageEntity)
                {
                    var convertedModel = model as IImageAddModel;
                    if (convertedModel.ImageFile != null && convertedModel.ImageFile.Length > 0)
                    {
                        var imageEntity = entity as IImageEntity;
                        imageEntity.ImageName = GenerateImageName(convertedModel.ImageFile);
                        imageModel = new ImageModel(convertedModel.ImageFile, convertedModel.FilePath, imageEntity.ImageName);
                    }
                    else if (convertedModel.IsImageRequired)
                    {
                        throw new ATBusinessException("Image is Required.");
                    }
                }
                baseService.Add(entity);
                if (imageModel != null)
                {
                    StoreImage(imageModel);
                }
                if (baseService is IQueryService<TEntity> && entity is BaseEntity)
                {
                    return Ok(_mapper.Map<TEntity, TModel>(GetUpdateEntityForRequestBase(entity as BaseEntity, baseService as IQueryService<TEntity>)));
                }
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<TEntity, TModel>(entity));
            });
        }
        public ActionResult<TModel> SimpleUpdateResponse<TEntity, TModel, TUpdateModel>(int id, TUpdateModel model, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
            where TUpdateModel : class
        {
            return ResponseWrapper<TModel>(() => {
                TEntity entity = baseService.Get(id);
                ImageModel imageModel = null;
                string oldImagePath = null;
                if (entity is IImageEntity)
                {
                    var convertedModel = model as IImageUpdateModel;
                    if (convertedModel.IsImageChanged)
                    {
                        var imageEntity = entity as IImageEntity;
                        if (convertedModel.ImageFile != null && convertedModel.ImageFile.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(imageEntity.ImageName))
                            {
                                oldImagePath = Path.Combine(PUBLIC_PATH + convertedModel.FilePath, imageEntity.ImageName);
                            }
                            imageEntity.ImageName = GenerateImageName(convertedModel.ImageFile);
                            imageModel = new ImageModel(convertedModel.ImageFile, convertedModel.FilePath, imageEntity.ImageName);
                        }
                        else if (convertedModel.IsImageRequired)
                        {
                            throw new ATBusinessException("Image is Required.");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(imageEntity.ImageName))
                            {
                                oldImagePath = Path.Combine(PUBLIC_PATH + convertedModel.FilePath, imageEntity.ImageName);
                            }
                            imageEntity.ImageName = null;
                        }
                    }
                }
                baseService.Update(_mapper.Map(model, entity));
                if (imageModel != null)
                {
                    StoreImage(imageModel);
                }
                if (oldImagePath != null)
                {
                    DeleteFile(oldImagePath);
                }
                if (baseService is IQueryService<TEntity> && entity is BaseEntity)
                {
                    return Ok(_mapper.Map<TEntity, TModel>(GetUpdateEntityForRequestBase(entity as BaseEntity, baseService as IQueryService<TEntity>)));
                }
                return StatusCode(StatusCodes.Status202Accepted, _mapper.Map<TEntity, TModel>(entity));
            });
        }

        public ActionResult<TModel> SimpleDeleteResponse<TEntity, TModel>(int id, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
        {
            return ResponseWrapper<TModel>(() => {
                TEntity entity = baseService.Get(id);
                string oldImagePath = null;
                if (entity is IImageEntity)
                {
                    var imageModel = _mapper.Map<TEntity, TModel>(entity) as IImageModel;
                    var imageEntity = entity as IImageEntity;
                    if (!string.IsNullOrEmpty(imageEntity.ImageName))
                    {
                        oldImagePath = Path.Combine(PUBLIC_PATH + imageModel.FilePath, imageEntity.ImageName);
                    }
                }
                baseService.Delete(entity);
                if (oldImagePath != null)
                {
                    DeleteFile(oldImagePath);
                }
                return Ok();
            });
        }

        public ActionResult<List<TModel>> RequestGetResponse<TEntity, TModel>(IQueryService<TEntity> queryService)
            where TEntity : class
            where TModel : class
        {
            return ResponseWrapper<List<TModel>>(() =>
            {
                RequestBase requestBase = RequestHelper.GetRequestBase((ODataQueryOptions)HttpContext.Items[RequestProperties.ODATA_OPTIONS]);
                if (HttpMethods.IsHead(Request.Method))
                {
                    // only show return count on response header
                    Response.Headers.Add(ResponseHeaderProperties.COUNT, queryService.GetCount(requestBase).ToString());
                    return Ok();
                }
                IEnumerable<TEntity> entity = queryService.GetAll(requestBase);
                return Ok(_mapper.Map<List<TEntity>, List<TModel>>(entity.ToList()));
            });
        }
        public ActionResult<TModel> RequestGetResponse<TEntity, TModel>(IQueryService<TEntity> queryService, int id)
            where TEntity : class
            where TModel : class
        {
            return ResponseWrapper<TModel>(() =>
            {
                GetByIdRequestBase getByIdRequestBase = RequestHelper.GetByIdRequestBase((ODataQueryOptions)HttpContext.Items[RequestProperties.ODATA_OPTIONS], id);
                TEntity entity = queryService.GetById(id, getByIdRequestBase);
                return Ok(_mapper.Map<TEntity, TModel>(entity));
            });
        }

        protected TEntity GetUpdateEntityForRequestBase<TEntity>(BaseEntity baseEntity, IQueryService<TEntity> queryService) where TEntity : class
        {
            GetByIdRequestBase getByIdRequestBase = RequestHelper.GetByIdRequestBase((ODataQueryOptions)HttpContext.Items[RequestProperties.ODATA_OPTIONS], baseEntity.Id);
            return queryService.GetById(baseEntity.Id, getByIdRequestBase);
        }
        protected ActionResult<TModel> ResponseWrapper<TModel>(Func<ActionResult<TModel>> completeAction) where TModel : class
        {
            ActionResult<TModel> response;
            try
            {
                response = completeAction();
            }
            catch (Exception ex)
            {
                response = HandleError(ex);
            }
            return response;

        }
        protected ActionResult ResponseWrapper(Func<ActionResult> completeAction)
        {
            ActionResult response;
            try
            {
                response = completeAction();
            }
            catch (Exception ex)
            {
                response = HandleError(ex);
            }
            return response;

        }

        private ActionResult HandleError(Exception ex)
        {
            ActionResult retVal = null;
            string exception = ex.GetType().Name;
            switch (exception)
            {
                case nameof(ATBusinessException):
                    retVal = StatusCode(StatusCodes.Status400BadRequest, _mapper.Map<ATBusinessException, ATBusinessExceptionModel>((ATBusinessException)ex));
                    break;
                case nameof(ATReferenceException):
                    _logger.LogExceptionToFile(ATExceptionTypes.ATReferenceException, "Reference Exception.", ex, ATErrorLevel.Information);
                    retVal = NotFound(GetSimpleExceptionModel(ex.Message));
                    break;
                case nameof(ATAuthenticationException):
                    _logger.LogExceptionToFile(ATExceptionTypes.ATAuthenticationException, "Authentication Exception.", ex, ATErrorLevel.Information);
                    retVal = StatusCode(StatusCodes.Status400BadRequest, GetSimpleExceptionModel("Authentication Exception.", ex));
                    break;
                case nameof(ParseException):
                    _logger.LogExceptionToFile(ATExceptionTypes.ODataParseException, "OData Query Exception.", ex, sendMessage: true);
                    retVal = StatusCode(StatusCodes.Status400BadRequest, GetSimpleExceptionModel("OData Query Exception.", ex));
                    break;
                case nameof(ApplicationException):
                    _logger.LogExceptionToFile(ATExceptionTypes.ApplicationException, "Application Exception.", ex, sendMessage: true);
                    retVal = StatusCode(StatusCodes.Status500InternalServerError, GetSimpleExceptionModel("Application Exception.", ex));
                    break;
                case nameof(DbUpdateException):
                    _logger.LogExceptionToFile(ATExceptionTypes.DbUpdateException, "Database Exception.", ex, sendMessage: true);
                    retVal = StatusCode(StatusCodes.Status500InternalServerError, GetSimpleExceptionModel("Internal Server Error.", ex));
                    break;
                case nameof(SqlException):
                    _logger.LogExceptionToFile(ATExceptionTypes.SqlException, "SQL Server Exception.", ex, sendMessage: true);
                    retVal = StatusCode(StatusCodes.Status500InternalServerError, GetSimpleExceptionModel("SQL Server Exception.", ex));
                    break;
                case nameof(NullReferenceException):
                    _logger.LogExceptionToFile(ATExceptionTypes.NullReferenceExecption, "Null Reference Error.", ex, sendMessage: true);
                    retVal = StatusCode(StatusCodes.Status500InternalServerError, GetSimpleExceptionModel("Internal Server Error.", ex));
                    break;
                default:
                    _logger.LogExceptionToFile(ATExceptionTypes.Unkown, "Exception is unkown.", ex, sendMessage:true);
                    retVal = StatusCode(StatusCodes.Status500InternalServerError, GetSimpleExceptionModel("Internal Server Error.", ex));
                    break;
            }
            return retVal;
        }

        private ATBusinessExceptionModel GetSimpleExceptionModel(string mainMessage , Exception exception = null)
        {
            ATBusinessExceptionModel aTBusinessExceptionModel = new ATBusinessExceptionModel
            {
                Message = mainMessage
            };
            if (exception != null)
            {
                aTBusinessExceptionModel.Validations = new List<ATBusinessExceptionMessageModel>()
                {
                    new ATBusinessExceptionMessageModel()
                    {
                        ErrorLevel = ATErrorLevel.Error,
                        Message = exception.Message
                    }
                };
            };
            return aTBusinessExceptionModel;
        }

        #region image  methods

        protected string GenerateImageName(IFormFile file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string FileNameToDB = fileName + "-D" + DateTime.UtcNow.ToString("yyMMddmmssfff") + extension;
            return FileNameToDB;
        }
        protected void StoreImage(ImageModel imageModel)
        {
            string imageFolder = string.Format("{0}\\{1}{2}", Directory.GetCurrentDirectory(), PUBLIC_PATH.Replace('/','\\'), imageModel.FilePath.Replace('/', '\\'));
            Directory.CreateDirectory(imageFolder);
            string FileNameToStore = Path.Combine(PUBLIC_PATH + imageModel.FilePath, imageModel.ImageName);
            using (var fileStream = new FileStream(FileNameToStore, FileMode.Create))
            {
                imageModel.ImageFile.CopyTo(fileStream);
            }
        }
        protected void DeleteFile(string totalFilePath)
        {
            if (System.IO.File.Exists(totalFilePath))
            {
                System.IO.File.Delete(totalFilePath);
            }
        }

        #endregion


    }
}

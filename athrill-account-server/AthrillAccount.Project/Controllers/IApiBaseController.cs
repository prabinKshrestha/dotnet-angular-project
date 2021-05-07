using Microsoft.AspNetCore.Mvc;
using AT.Model;
using AT.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Controllers
{
    public interface IApiBaseController
    {
        ActionResult<TModel> SimpleGetResponse<TEntity, TModel>(IBaseService<TEntity> baseService, int id)
            where TEntity : class
            where TModel : class
        ;

        public ActionResult<List<TModel>> SimpleGetResponse<TEntity, TModel>(IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
        ;
        public ActionResult<TModel> SimpleAddResponse<TEntity, TModel, TAddModel>(TAddModel model, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
            where TAddModel : class
        ;
        public ActionResult<TModel> SimpleUpdateResponse<TEntity, TModel, TUpdateModel>(int id, TUpdateModel model, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
            where TUpdateModel : class
        ;
        public ActionResult<TModel> SimpleDeleteResponse<TEntity, TModel>(int id, IBaseService<TEntity> baseService)
            where TEntity : class
            where TModel : class
        ;
        public ActionResult<TModel> RequestGetResponse<TEntity, TModel>(IQueryService<TEntity> queryService, int id)
            where TEntity : class
            where TModel : class
        ;
        public ActionResult<List<TModel>> RequestGetResponse<TEntity, TModel>(IQueryService<TEntity> queryService)
            where TEntity : class
            where TModel : class
        ;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Entity.Contents;
using AT.Entity.Contents.ContentTrees;
using AT.Model.Contents;
using AT.Service.Contents;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthrillAccount.Project.Controllers.Contents
{
    [Route("content")]
    [ApiController]
    public class ContentController : ApiBaseController
    {
        private readonly IContentService _contentService;
        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }
        /// <summary>
        ///  Get Content
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Content</returns>
        [HttpGet("{id}", Name = "GetContentRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<ContentModel> GetContent(int id)
        {
            return SimpleGetResponse<Content, ContentModel>(_contentService, id);
        }

        /// <summary>
        ///  Get All Contents
        /// </summary>
        /// <returns>All Contents</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<List<ContentModel>> GetContents()
        {
            return RequestGetResponse<Content, ContentModel>(_contentService);
        }

        /// <summary>
        ///  Add Content
        /// </summary>
        /// <returns>Added Content</returns>
        [HttpPost]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<ContentModel> AddContent([FromForm] ContentAddModel model)
        {
            return SimpleAddResponse<Content, ContentModel, ContentAddModel>(model, _contentService);
        }

        /// <summary>
        ///  Update Content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Updated Content</returns>
        [HttpPut("{id}", Name = "UpdateContentRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<ContentModel> UpdateContent(int id, [FromForm] ContentUpdateModel model)
        {
            return SimpleUpdateResponse<Content, ContentModel, ContentUpdateModel>(id, model, _contentService);
        }

        /// <summary>
        ///  Delete Content
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Updated Content</returns>
        [HttpDelete("{id}", Name = "DeleteContentRoute")]
        [BasicAuthorization]
        public ActionResult<ContentModel> DeleteContent(int id)
        {
            return SimpleDeleteResponse<Content, ContentModel>(id, _contentService);
        }

        /// <summary>
        ///  Get Content Tree Structure
        /// </summary>
        /// <returns>Content Tree Structure</returns>
        [HttpGet]
        [Route("tree",Name = "GetContentTreesRoute")]
        [BasicAuthorization]
        public ActionResult<List<ContentTree>> GetContentTrees()
        {
            return ResponseWrapper<List<ContentTree>>(() => {
                return _contentService.GetContentTrees();
            });
        }

        /// <summary>
        ///  Save Content Tree Structure
        /// </summary>
        /// <returns>Content Tree Structure</returns>
        [HttpPut]
        [Route("tree", Name = "UpdateContentTreesRoute")]
        [BasicAuthorization]
        public ActionResult UpdateContentTrees(List<ContentTree> contentTrees)
        {
            return ResponseWrapper(() => {
                _contentService.UpdateContentTrees(contentTrees);
                return StatusCode(StatusCodes.Status202Accepted);
            });
        }

        /// <summary>
        ///  Content Types
        /// </summary>
        /// <returns>Content Types</returns>
        [HttpGet]
        [Route("contenttypes", Name = "GetAllContentTypesRoute")]
        [BasicAuthorization]
        public ActionResult<List<ContentTypeModel>> GetContentAllTypes()
        {
            return ResponseWrapper(() => {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<List<ContentType>, List<ContentTypeModel>>(_contentService.GetAllContentTypes()));
            });
        }
    }
}
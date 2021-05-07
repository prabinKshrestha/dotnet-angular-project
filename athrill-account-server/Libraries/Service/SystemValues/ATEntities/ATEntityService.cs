using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Data.Interface;
using AT.Data.Request;
using AT.Entity.SystemValues.ATEntities;
using System;
using System.Collections.Generic;

namespace AT.Service.SystemValues.ATEntities
{
    public class ATEntityService : QueryService<ATEntity>, IATEntityService
    {
        private readonly IRepository<ATEntity> _aTEntityRepository;

        public ATEntityService(IRepository<ATEntity> aTEntityRepository)
        {
            _aTEntityRepository = aTEntityRepository;
        }
             

        public ATEntity Get(int id)
        {
            return _aTEntityRepository.Get(id);
        }

        public IEnumerable<ATEntity> GetAll()
        {
            return _aTEntityRepository.GetAll();
        }

        public override ATEntity GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.EntityId == id, id);
        }

        #region Modify Methods

        public void Add(ATEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ATEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ATEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("EntityId", "ASC");
        }

        public override void SetRepository()
        {
            QueryRepository = _aTEntityRepository;
        }
        #endregion
    }
}

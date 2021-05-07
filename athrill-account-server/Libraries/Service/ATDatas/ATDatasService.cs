using AT.Data.Interface;
using AT.Entity.System.ATDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.ATDatas
{
    public class ATDatasService : IATDatasService
    {
        private readonly IRepository<ATDataValue> _atDataRepository;

        public ATDatasService(IRepository<ATDataValue> atDataRepository)
        {
            _atDataRepository = atDataRepository;
        }

        public List<ATDataValue> GetATDataValuesByType(int atDataTypeId)
        {
            return _atDataRepository.TableNotTracked.Where(x => x.ATDataTypeId == atDataTypeId && x.IsActive).ToList();
        }

        #region NotImplemented
        public void Add(ATDataValue entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ATDataValue entity)
        {
            throw new NotImplementedException();
        }

        public ATDataValue Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ATDataValue> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ATDataValue entity)
        {
            throw new NotImplementedException();
        }

        
        #endregion
    }
}

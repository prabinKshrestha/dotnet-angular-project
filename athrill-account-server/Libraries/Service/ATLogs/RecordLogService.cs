using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Data.Interface;
using AT.Data.Request;
using AT.Entity.ATLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.ATLogs
{
    public class RecordLogService : QueryService<RecordLog>, IRecordLogService
    {
        private readonly IRepository<RecordLog> _recordLogRepository;

        public RecordLogService(IRepository<RecordLog> recordLogRepository)
        {
            _recordLogRepository = recordLogRepository;
        }

        public RecordLog Get(int id)
        {
            return _recordLogRepository.Get(id);
        }

        public IEnumerable<RecordLog> GetAll()
        {
            return _recordLogRepository.GetAll();
        }

        public override RecordLog GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.RecordLogId == id, id);
        }

        public RecordLog GetLastRecordLog(int id)
        {
            RecordLog recordLog = _recordLogRepository.TableNotTracked.FirstOrDefault(x => x.RecordLogId == id);
            return  _recordLogRepository.TableNotTracked.OrderByDescending(x => x.RecordLogId).FirstOrDefault(x => x.InsertId == recordLog.InsertId && x.RecordLogId < recordLog.RecordLogId);
        }

        #region Private Methods

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }

        public override void SetRepository()
        {
            QueryRepository = _recordLogRepository;
        }

        #endregion

        #region Modify Methods

        public void Add(RecordLog entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(RecordLog entity)
        {
            throw new NotImplementedException();
        }

        public void Update(RecordLog entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Data.Interface;
using AT.Data.Request;
using AT.Entity.Basics.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Basics.Teams
{
    public class TeamCategoryService : QueryService<TeamCategory>, ITeamCategoryService
    {
        private readonly IRepository<TeamCategory> _teamCategoryRepository;
        private readonly ITeamCategoryServiceRule _teamCategoryServiceRule;
        private readonly IUnitOfWork _unitOfWork;

        public TeamCategoryService(IRepository<TeamCategory> teamCategoryRepository
            , ITeamCategoryServiceRule teamCategoryServiceRule
            , IUnitOfWork unitOfWork)
        {
            _teamCategoryRepository = teamCategoryRepository;
            _teamCategoryServiceRule = teamCategoryServiceRule;
            _unitOfWork = unitOfWork;
        }

        public TeamCategory Get(int id)
        {
            return _teamCategoryRepository.Get(id);
        }
        public IEnumerable<TeamCategory> GetAll()
        {
            return _teamCategoryRepository.GetAll();
        }
        public override TeamCategory GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.TeamCategoryId == id, id);
        }

        #region Modify Methods

        public void Add(TeamCategory entity)
        {
            entity.Orientation = (_teamCategoryRepository.TableNotTracked.Select(p => p.Orientation).Cast<int?>().Max() ?? 0) + 1;
            _teamCategoryServiceRule.CheckAddRule(entity);
            _teamCategoryRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(TeamCategory entity)
        {
            _teamCategoryServiceRule.CheckUpdateRule(entity);
            _teamCategoryRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(TeamCategory entity)
        {
            _teamCategoryServiceRule.CheckDeleteRule(entity);
            entity.Orientation = 0;
            _teamCategoryRepository.Delete(entity);
            _unitOfWork.Commit();
        }
        public void UpdateTeamCategoryPostion(List<int> ids)
        {
            List<TeamCategory> teams = _teamCategoryRepository.GetAll().ToList();
            int orientation = 1;
            ids.ForEach(id =>
            {
                TeamCategory team = teams.FirstOrDefault(x => x.Id == id);
                team.Orientation = orientation;
                _teamCategoryRepository.Update(team);
                orientation++;
            });
            _unitOfWork.Commit();
        }

        #endregion

        #region Private Methods

        public override void SetRepository()
        {
            QueryRepository = _teamCategoryRepository;
        }

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }
        #endregion
    }
}

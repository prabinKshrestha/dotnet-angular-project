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
    public class TeamMemberService : QueryService<TeamMember>, ITeamMemberService
    {
        private readonly IRepository<TeamMember> _teamMemberRepository;
        private readonly IRepository<TeamCategoryMemberLink> _teamCategoryMemberLinkRepository;
        private readonly ITeamMemberServiceRule _teamMemberServiceRule;
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberService(IRepository<TeamMember> teamMemberRepository
            , IRepository<TeamCategoryMemberLink> teamCategoryMemberLinkRepository
            , ITeamMemberServiceRule teamMemberServiceRule
            , IUnitOfWork unitOfWork)
        {
            _teamMemberRepository = teamMemberRepository;
            _teamCategoryMemberLinkRepository = teamCategoryMemberLinkRepository;
            _teamMemberServiceRule = teamMemberServiceRule;
            _unitOfWork = unitOfWork;
        }

        public TeamMember Get(int id)
        {
            return _teamMemberRepository.Get(id);
        }
        public IEnumerable<TeamMember> GetAll()
        {
            return _teamMemberRepository.GetAll();
        }
        public override TeamMember GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.TeamMemberId == id, id);
        }

        #region Modify Methods

        public void Add(TeamMember entity)
        {
            _teamMemberServiceRule.CheckAddRule(entity);
            List<TeamCategoryMemberLink> links = _teamCategoryMemberLinkRepository.TableNotTracked.Where(x => entity.TeamCategoryIds.Contains(x.TeamCategoryId)).ToList();
            Dictionary<int, int> catIdsAndOrientationIds = links.GroupBy(x => x.TeamCategoryId).ToDictionary(x => x.Key, x => (x.Select(p => p.TeamMemberOrientation).Cast<int?>().Max() ?? 0) + 1);
            entity.TeamCategoryMemberLinks = entity.TeamCategoryIds.Select(x => new TeamCategoryMemberLink()
            {
                TeamCategoryId = x,
                TeamMemberOrientation = catIdsAndOrientationIds.ContainsKey(x) ? catIdsAndOrientationIds[x] : 1
            }).ToList();
            _teamMemberRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(TeamMember entity)
        {
            _teamMemberServiceRule.CheckUpdateRule(entity);

            //team category ids form client which are fresh one.
            List<int> categoryIdsFromClients = entity.TeamCategoryIds;
            //Links for this particular team members to process new categories, delete links
            List<TeamCategoryMemberLink> teamCategoryMemberLinks = _teamCategoryMemberLinkRepository.TableNotTracked.Where(x => x.TeamMemberId == entity.TeamMemberId).ToList();
            // These are those links which need to be deleted
            List<TeamCategoryMemberLink> teamCategoryMemberLinksToDelete = teamCategoryMemberLinks.Where(x => !categoryIdsFromClients.Contains(x.TeamCategoryId)).ToList();
            //these are new cateogory to which this member will be linked
            List<int> categoryIdsToInsert = categoryIdsFromClients.Except(teamCategoryMemberLinks.Select(x => x.TeamCategoryId)).ToList();

            //insert new categories
            List<TeamCategoryMemberLink> links = _teamCategoryMemberLinkRepository.TableNotTracked.Where(x => categoryIdsToInsert.Contains(x.TeamCategoryId)).ToList();
            Dictionary<int, int> catIdsAndOrientationIds = links.GroupBy(x => x.TeamCategoryId).ToDictionary(x => x.Key, x => (x.Select(p => p.TeamMemberOrientation).Cast<int?>().Max() ?? 0) + 1);
            entity.TeamCategoryMemberLinks = categoryIdsToInsert.Select(x => new TeamCategoryMemberLink()
            {
                TeamCategoryId = x,
                TeamMemberOrientation = catIdsAndOrientationIds.ContainsKey(x) ? catIdsAndOrientationIds[x] : 1
            }).ToList();

            //delete existing
            // TODO : this should be hard deleted
            teamCategoryMemberLinksToDelete.ForEach(x => {
                x.TeamMemberOrientation = 0;
                _teamCategoryMemberLinkRepository.Delete(x);
            });

            _teamMemberRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(TeamMember entity)
        {
            _teamMemberServiceRule.CheckDeleteRule(entity);

            //delete existing
            // NOTE : this should be soft deleted
            List<TeamCategoryMemberLink> teamCategoryMemberLinksToDelete = _teamCategoryMemberLinkRepository.Table.Where(x => x.TeamMemberId == entity.TeamMemberId).ToList();
            teamCategoryMemberLinksToDelete.ForEach(x => {
                x.TeamMemberOrientation = 0;
                _teamCategoryMemberLinkRepository.Delete(x);
            });

            _teamMemberRepository.Delete(entity);
            _unitOfWork.Commit();
        }
        public void UpdateTeamMemberPostion(List<TeamMemberOrientationUpdateEntityModel> model)
        {
            List<TeamCategoryMemberLink> links = _teamCategoryMemberLinkRepository.GetAll().ToList();
            Dictionary<int, List<int>> collection = model.GroupBy(x => x.TeamCategoryId).ToDictionary(x => x.Key, x => x.Select(p => p.TeamMemberId).ToList());
            foreach (KeyValuePair<int, List<int>> pairs in collection)
            {
                int orientation = 1;
                pairs.Value.ForEach(teamMemberId =>
                {
                    TeamCategoryMemberLink linkToUpdate = links.FirstOrDefault(x => x.TeamMemberId == teamMemberId && x.TeamCategoryId == pairs.Key);
                    linkToUpdate.TeamMemberOrientation = orientation;
                    _teamCategoryMemberLinkRepository.Update(linkToUpdate);
                    orientation++;
                });
            }
            _unitOfWork.Commit();
        }

        #endregion

        #region Private Methods

        public override void SetRepository()
        {
            QueryRepository = _teamMemberRepository;
        }

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }
        #endregion
    }
}

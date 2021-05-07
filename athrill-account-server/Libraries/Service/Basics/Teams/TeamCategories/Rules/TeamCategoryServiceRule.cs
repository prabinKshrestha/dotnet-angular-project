using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Data.Interface;
using AT.Entity.Basics.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Basics.Teams
{
    public class TeamCategoryServiceRule : ITeamCategoryServiceRule
    {
        private readonly IRepository<TeamCategory> _teamCategoryRepository;
        private readonly IRepository<TeamCategoryMemberLink> _teamCategoryMemberLinkRepository;

        public TeamCategoryServiceRule(IRepository<TeamCategory> teamCategoryRepository
            , IRepository<TeamCategoryMemberLink> teamCategoryMemberLinkRepository)
        {
            _teamCategoryRepository = teamCategoryRepository;
            _teamCategoryMemberLinkRepository = teamCategoryMemberLinkRepository;
        }
        public void CheckAddRule(TeamCategory entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Team Category Add Business Rule Violations", validations);
            }
        }

        public void CheckUpdateRule(TeamCategory entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Team Category Update Business Rule Violations", validations);
            }
        }

        public void CheckDeleteRule(TeamCategory entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (_teamCategoryMemberLinkRepository.TableNotTracked.Any(x => x.TeamCategoryId == entity.TeamCategoryId))
            {
                validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Since, team members are assigned to this category, the category cannot be deleted.", "Team Category Name"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("Team Category Update Business Rule Violations", validations);
            }
        }

        private IEnumerable<ATBusinessExceptionMessage> PerformBasicCheck(TeamCategory entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();

            if (_teamCategoryRepository.TableNotTracked.Any(x => x.Name == entity.Name && entity.TeamCategoryId != x.TeamCategoryId))
            {
                validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Name already exist.", "Team Category Name"));
            }
            return validations;
        }

    }
}

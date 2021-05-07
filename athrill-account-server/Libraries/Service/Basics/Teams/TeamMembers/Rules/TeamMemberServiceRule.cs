using AT.Common.Exceptions;
using AT.Data.Interface;
using AT.Entity.Basics.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Basics.Teams
{
    public class TeamMemberServiceRule : ITeamMemberServiceRule
    {
        private readonly IRepository<TeamMember> _teamMemberRepository;
        private readonly IRepository<TeamCategory> _teamCategoryRepository;
        private readonly IRepository<TeamCategoryMemberLink> _teamCategoryMemberLinkRepository;

        public TeamMemberServiceRule(IRepository<TeamMember> teamMemberRepository
            , IRepository<TeamCategory> teamCategoryRepository
            , IRepository<TeamCategoryMemberLink> teamCategoryMemberLinkRepository)
        {
            _teamMemberRepository = teamMemberRepository;
            _teamCategoryRepository = teamCategoryRepository;
            _teamCategoryMemberLinkRepository = teamCategoryMemberLinkRepository;
        }
        public void CheckAddRule(TeamMember entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Team Member Add Business Rule Violations", validations);
            }
        }

        public void CheckUpdateRule(TeamMember entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Team Member Update Business Rule Violations", validations);
            }
        }

        public void CheckDeleteRule(TeamMember entity)
        {
        }

        private IEnumerable<ATBusinessExceptionMessage> PerformBasicCheck(TeamMember entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            List<TeamMember> existingData = _teamMemberRepository.TableNotTracked.ToList();

            if (existingData.Any(x => string.Equals(x.Name, entity.Name, StringComparison.OrdinalIgnoreCase) && x.TeamMemberId != entity.TeamMemberId))
            {
                validations.Add(new ATBusinessExceptionMessage("Team name cannot be duplicated.", "Name"));
            }

            if (existingData.Any(x => x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase) && x.TeamMemberId != entity.TeamMemberId))
            {
                validations.Add(new ATBusinessExceptionMessage("Email cannot be duplicated.", "Email"));
            }

            if (entity.TeamCategoryIds == null || entity.TeamCategoryIds.Count < 1)
            {
                validations.Add(new ATBusinessExceptionMessage("At least one category should be selected.", "Team Category"));
            }
            else
            {
                List<int> ids = _teamCategoryRepository.TableNotTracked.Where(x => entity.TeamCategoryIds.Contains(x.TeamCategoryId)).Select(x => x.TeamCategoryId).ToList();
                if (entity.TeamCategoryIds.Any(x => !ids.Contains(x)))
                {
                    validations.Add(new ATBusinessExceptionMessage("One of the cateogory does not exist.", "Team Category"));
                }
            }

            return validations;
        }
    }
}

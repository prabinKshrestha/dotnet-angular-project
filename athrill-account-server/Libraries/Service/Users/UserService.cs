using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Common.Constants;
using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Infrastructure.Interfaces;
using AT.Data.Interface;
using AT.Data.Request;
using AT.Entity.Users;
using AT.Service.Users.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Users
{
    public class UserService : QueryService<User>, IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<UserLogin> _userLoginRepository;
        private readonly IRepository<UserRoleLink> _userRoleLinkRepository;
        private readonly IRepository<UserTrackRecord> _userTrackRecordRepository;
        private readonly IUserServiceRule _userServiceRule;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IConfiguration _configuration;
        private readonly IClientInfoContext _clientInfoContext;
        public UserService(IRepository<User> userRepository
            , IRepository<UserRole> userRoleRepository
            , IRepository<UserLogin> userLoginRepository
            , IRepository<UserRoleLink> userRoleLinkRepository
            , IRepository<UserTrackRecord> userTrackRecordRepository
            , IUserServiceRule userServiceRule
            , IUnitOfWork unitOfWork
            , IWorkContext workContext
            , IClientInfoContext clientInfoContext
            , IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleLinkRepository = userRoleLinkRepository;
            _userRoleRepository = userRoleRepository;
            _userTrackRecordRepository = userTrackRecordRepository;
            _userServiceRule = userServiceRule;
            _unitOfWork = unitOfWork;
            _workContext = workContext;
            _configuration = configuration;
            _clientInfoContext = clientInfoContext;
        }
        public User Get(int id)
        {
           return _userRepository.Get(id);
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public override User GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.UserId == id, id);
        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _userRoleRepository.GetAll().Where(x => x.UserRoleId != (int)UserRoles.SuperAdmin);
        }

        public IList<UserTrackRecord> GetUserTrackRecords(RequestBase request)
        {
            return GetQueryForUserTrackRcords(request).FinalizeQuery(request);
        }
        public int GetUserTrackRecordsCount(RequestBase request)
        {
            return GetQueryForUserTrackRcords(request).GetCount();
        }

        #region modify

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }
        public void Update(User entity)
        {
            _userServiceRule.CheckUpdateRule(entity);
            _userRepository.Update(entity);
            _unitOfWork.Commit();
        }
        public void Delete(User entity)
        {
            _userServiceRule.CheckDeleteRule(entity);
            UserLogin userLogin = _userLoginRepository.Table.FirstOrDefault(x => x.UserId == entity.UserId);
            UserRoleLink userRoleLink = _userRoleLinkRepository.Table.FirstOrDefault(x => x.UserId == entity.UserId);
            _userRepository.Delete(entity);
            _userLoginRepository.Delete(userLogin);
            _userRoleLinkRepository.Delete(userRoleLink);
            _unitOfWork.Commit();
        }
        public void ChangeActiveStatus(int userId, bool activeStatus)
        {
            User user = _userRepository.TableNotTracked.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                _userServiceRule.CheckChangeActiveStatusRule(user, activeStatus);
                if (user.IsActive != activeStatus)
                {
                    user.IsActive = activeStatus;
                    _userRepository.Update(user);
                    _unitOfWork.Commit();

                    #region set user data track

                    UserTrackRecord userTrackRecord = new UserTrackRecord()
                    {
                        UserId = userId,
                        UserTrackTypeId = activeStatus ? (int)UserTrackTypes.Activate : (int)UserTrackTypes.Deactivate,
                        IpAddress = _clientInfoContext.IPAddress,
                        ClientName = _clientInfoContext.GetClientInfoName(),
                        CreatedById = _workContext.UserId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _userTrackRecordRepository.Add(userTrackRecord);
                    _unitOfWork.Commit(false); // do not insert logs to record log table

                    #endregion
                }
            }
            else
            {
                throw new ATReferenceException(userId, typeof(User).Name);
            }
        }

        public void UpdateRegisteredUser(int userId, UserRegisteredUpdateEntityModel model)
        {
            _userServiceRule.CheckUserRegisteredUpdateRule(userId, model);
            User user = _userRepository.TableNotTracked.FirstOrDefault(x => x.UserId == userId);
            
            if (user.IsActive != model.IsActive)
            {
                user.IsActive = model.IsActive;
                _userRepository.Update(user);
                _unitOfWork.Commit();

                #region set user data track

                UserTrackRecord userTrackRecord = new UserTrackRecord()
                {
                    UserId = userId,
                    UserTrackTypeId = model.IsActive ? (int)UserTrackTypes.Activate : (int)UserTrackTypes.Deactivate,
                    IpAddress = _clientInfoContext.IPAddress,
                    ClientName = _clientInfoContext.GetClientInfoName(),
                    CreatedById = _workContext.UserId,
                    CreatedOn = DateTime.UtcNow
                };
                _userTrackRecordRepository.Add(userTrackRecord);
                _unitOfWork.Commit(false); // do not insert logs to record log table

                #endregion
            }
            UserRoleLink userRoleLink = _userRoleLinkRepository.TableNotTracked.FirstOrDefault(x => x.UserId == userId);
            if (userRoleLink.UserRoleId != model.UserRoleId)
            {
                userRoleLink.UserRoleId = model.UserRoleId;
                _userRoleLinkRepository.Update(userRoleLink);
                _unitOfWork.Commit();
            }
        }

        #endregion

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }

        public override void SetRepository()
        {
            QueryRepository = _userRepository;
        }
        public virtual IQueryable<UserTrackRecord> GetQueryForUserTrackRcords(RequestBase request)
        {
            IQueryable<UserTrackRecord> query = _userTrackRecordRepository.TableFromRequest(request);
            OrderByItem orderByItem = new OrderByItem("CreatedOn", "desc");
            IOrderedQueryable<UserTrackRecord> orderedQuery = query.OrderFromRequest(request, orderByItem);
            return orderedQuery;
        }
    }
}

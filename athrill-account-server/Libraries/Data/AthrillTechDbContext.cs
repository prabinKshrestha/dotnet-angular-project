using AT.Common.Enum;
using AT.Data.Interface;
using AT.Entity;
using AT.Entity.Basics;
using AT.Entity.Settings.EmailSettings;
using AT.Entity.Settings.SiteSettings;
using AT.Entity.System.ATDatas;
using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AT.Entity.Contents;
using AT.Entity.ATLogs;
using AT.Common.Infrastructure.Interfaces;
using AT.Entity.SystemValues.ATEntities;
using AT.Common.Attributes;
using AT.Entity.Basics.Teams;

namespace AT.Data
{
    public class AthrillTechDbContext : DbContext, IAthrillTechDbContext
    {
        private readonly IWorkContext _workContext;
        private readonly IClientInfoContext _clientInfoContext;
        public AthrillTechDbContext(DbContextOptions dbContextOption
            , IWorkContext workContext
            , IClientInfoContext clientInfoContext) : base(dbContextOption)
        {
            _workContext = workContext;
            _clientInfoContext = clientInfoContext;
        }

        #region DbSets

        #region System and System Values

        public DbSet<ATEntity> ATEntities { get; set; }
        public DbSet<RecordLog> DbRecordLogs { get; set; }
        public DbSet<ATDataType> ATDataTypes { get; set; }
        public DbSet<ATDataValue> ATDataValues { get; set; }

        #endregion

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRoleLink> UserRoleLinks { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserTrackRecord> UserTrackRecords { get; set; }
        #endregion

        #region Settings
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        #endregion

        #region Basic

        #region Team
        public DbSet<TeamCategory> TeamCategories { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamCategoryMemberLink> TeamCategoryMemberLinks { get; set; }
        #endregion

        #endregion

        #region Contents

        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<Content> Contents { get; set; }

        #endregion

        #endregion

        // mapping to physical table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            FilterDeletedRecords(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public void FilterDeletedRecords(ModelBuilder modelBuilder)
        {
            #region System And System Values

            modelBuilder.Entity<ATEntity>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<ATDataType>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<ATDataValue>().HasQueryFilter(x => x.DeactivatedOn == null);

            #endregion

            #region Users

            modelBuilder.Entity<User>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<UserLogin>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<UserRoleLink>().HasQueryFilter(x => x.DeactivatedOn == null);

            #endregion

            #region Setting

            modelBuilder.Entity<SiteSetting>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<EmailSetting>().HasQueryFilter(x => x.DeactivatedOn == null);

            #endregion

            #region Contents

            modelBuilder.Entity<Content>().HasQueryFilter(x => x.DeactivatedOn == null);

            #endregion

            #region Basic

            #region Team

            modelBuilder.Entity<TeamCategory>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<TeamMember>().HasQueryFilter(x => x.DeactivatedOn == null);
            modelBuilder.Entity<TeamCategoryMemberLink>().HasQueryFilter(x => x.DeactivatedOn == null);

            #endregion

            #endregion
        }

        // commmit manual
        public void Commit(bool LogRecords)
        {
            List<RecordLog> bulkRecordLogs = new List<RecordLog>();

            IEnumerable<EntityEntry> addEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            IEnumerable<EntityEntry> updateEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            IEnumerable<EntityEntry> deleteEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();
            
            if (addEntries.Count() > 0) // for added entries
            {
                Guid insertGuid = Guid.NewGuid();
                foreach (EntityEntry entry in addEntries)
                {
                    if (entry.Entity is BaseEntity)
                    {
                        var entity = entry.Entity as BaseEntity;
                        if (entity.CreatedOn == null || entity.CreatedOn == DateTime.MinValue)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                        }
                        entity.DeactivatedOn = null;
                        entity.CreatedById = (_workContext.UserId > 0) ? _workContext.UserId : 0;
                        entity.InsertId = insertGuid;
                        entity.BatchId = _clientInfoContext.BatchId;
                    }
                    if (LogRecords)
                    {
                        bulkRecordLogs.Add(RecordLog(entry, RecordLogTypes.Added, insertGuid));
                    }
                }
            }

            if (updateEntries.Count() > 0) // for updated entries
            {
                foreach (var entry in updateEntries)
                {
                    Guid? insertGuid = null;
                    if (entry.Entity is BaseEntity)
                    {
                        var entity = entry.Entity as BaseEntity;
                        entity.UpdatedOn = DateTime.UtcNow;
                        entity.UpdatedById = (_workContext.UserId > 0) ? _workContext.UserId : 0;
                        entity.BatchId = _clientInfoContext.BatchId;
                        insertGuid = entity.InsertId;
                    }
                    if (LogRecords)
                    {
                        bulkRecordLogs.Add(RecordLog(entry, RecordLogTypes.Modified, insertGuid));
                    }
                }
            }

            if (deleteEntries.Count() > 0) // for deleted entries
            {
                foreach (var entry in deleteEntries)
                {
                    Guid? insertGuid = null;
                    if (entry.Entity is BaseEntity && entry.Entity is ISoftDeleteEntity)
                    {
                        entry.State = EntityState.Modified;
                        var entity = entry.Entity as BaseEntity;
                        entity.UpdatedOn = DateTime.UtcNow;
                        entity.DeactivatedOn = DateTime.UtcNow;
                        entity.UpdatedById = (_workContext.UserId > 0) ? _workContext.UserId : 0;
                        entity.BatchId = _clientInfoContext.BatchId;
                        insertGuid = entity.InsertId;
                    }
                    if (LogRecords)
                    {
                        bulkRecordLogs.Add(RecordLog(entry, RecordLogTypes.Deleted, insertGuid));
                    }
                }
            }

            if (LogRecords && bulkRecordLogs.Count() > 0)
            {
                // it is not saved here because, it will be saved in unit of work save changes
                this.DbRecordLogs.AddRange(bulkRecordLogs); 
            }
        }

        //record log if set
        public RecordLog RecordLog(EntityEntry entry, RecordLogTypes recordLogType, Guid? insertId)
        {
            return new RecordLog
            {
                RecordType = (int)recordLogType,
                EntityId = (int)EntityStore.EntityNameEnumPair[entry.Entity.GetType().Name],
                Record = JsonConvert.SerializeObject(entry.Entity, 
                                                    new JsonSerializerSettings{
                                                            Formatting = Formatting.None,
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                            ContractResolver = new RecordLogContractResolver()
                                                    }),
                CreatedBy = _workContext.UserId,
                CreatedOn = DateTime.UtcNow,
                InsertId = insertId ?? Guid.NewGuid(),
                BatchId = _clientInfoContext.BatchId,
                IPAddress = _clientInfoContext.IPAddress,
                ClientName = _clientInfoContext.GetClientInfoName()
            };
        }
    }
}

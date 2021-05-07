using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AT.Data.Migrations
{
    public partial class initialsetupmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATDataTypes",
                columns: table => new
                {
                    ATDataTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATDataTypes", x => x.ATDataTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ATEntities",
                columns: table => new
                {
                    EntityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsShownInRecordLogForSupportSite = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATEntities", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    ContentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.ContentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    EmailSettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    SendFromName = table.Column<string>(maxLength: 200, nullable: false),
                    ReplyToName = table.Column<string>(maxLength: 200, nullable: false),
                    ReplyToEmail = table.Column<string>(maxLength: 100, nullable: false),
                    Host = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false),
                    IsSSL = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.EmailSettingId);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    SiteSettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(maxLength: 200, nullable: false),
                    SiteName1 = table.Column<string>(maxLength: 200, nullable: true),
                    SiteName2 = table.Column<string>(maxLength: 200, nullable: true),
                    SiteName3 = table.Column<string>(maxLength: 200, nullable: true),
                    SiteName4 = table.Column<string>(maxLength: 200, nullable: true),
                    SiteSlogan = table.Column<string>(maxLength: 400, nullable: false),
                    SiteUrl = table.Column<string>(maxLength: 200, nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    FeedbackEmail = table.Column<string>(nullable: false),
                    CopyrightName = table.Column<string>(maxLength: 200, nullable: false),
                    WorkHours = table.Column<string>(maxLength: 200, nullable: true),
                    AddressShortDetail = table.Column<string>(maxLength: 600, nullable: false),
                    AddressDetail = table.Column<string>(nullable: false),
                    LocationIframe = table.Column<string>(nullable: true),
                    FacebookLink = table.Column<string>(nullable: true),
                    YoutubeLink = table.Column<string>(nullable: true),
                    TwiterLink = table.Column<string>(nullable: true),
                    InstagramLink = table.Column<string>(nullable: true),
                    LinkedInLink = table.Column<string>(nullable: true),
                    SkypeLink = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Viber = table.Column<string>(nullable: true),
                    Whatsapp = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 200, nullable: false),
                    MetaKeywords = table.Column<string>(maxLength: 400, nullable: false),
                    MetaDescription = table.Column<string>(nullable: false),
                    MetaImageName = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.SiteSettingId);
                });

            migrationBuilder.CreateTable(
                name: "TeamCategories",
                columns: table => new
                {
                    TeamCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orientation = table.Column<int>(nullable: false),
                    TeamCategoryName = table.Column<string>(maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 600, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCategories", x => x.TeamCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    TeamMemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamMemberName = table.Column<string>(maxLength: 200, nullable: false),
                    Role = table.Column<string>(maxLength: 400, nullable: true),
                    Position = table.Column<string>(maxLength: 400, nullable: true),
                    Quotation = table.Column<string>(maxLength: 400, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 600, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FacebookLink = table.Column<string>(nullable: true),
                    InstagramLink = table.Column<string>(nullable: true),
                    SkypeLink = table.Column<string>(nullable: true),
                    LinkedInLink = table.Column<string>(nullable: true),
                    Twiterlink = table.Column<string>(nullable: true),
                    Viber = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.TeamMemberId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsShown = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "ATDataValues",
                columns: table => new
                {
                    ATDataValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ATDataTypeId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATDataValues", x => x.ATDataValueId);
                    table.ForeignKey(
                        name: "FK_ATDataValues_ATDataTypes_ATDataTypeId",
                        column: x => x.ATDataTypeId,
                        principalTable: "ATDataTypes",
                        principalColumn: "ATDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordLogs",
                columns: table => new
                {
                    RecordLogId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    RecordType = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Record = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordLogs", x => x.RecordLogId);
                    table.ForeignKey(
                        name: "FK_RecordLogs_ATEntities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "ATEntities",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamCategoryMemberLinks",
                columns: table => new
                {
                    TeamCategoryMemberLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamCategoryId = table.Column<int>(nullable: false),
                    TeamMemberId = table.Column<int>(nullable: false),
                    TeamMemberOrientation = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCategoryMemberLinks", x => x.TeamCategoryMemberLinkId);
                    table.ForeignKey(
                        name: "FK_TeamCategoryMemberLinks_TeamCategories_TeamCategoryId",
                        column: x => x.TeamCategoryId,
                        principalTable: "TeamCategories",
                        principalColumn: "TeamCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamCategoryMemberLinks_TeamMembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "TeamMembers",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentTypeId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    PlacementId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 200, nullable: false),
                    ContentName = table.Column<string>(maxLength: 200, nullable: false),
                    ContentSubName = table.Column<string>(maxLength: 200, nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    ImageAltName = table.Column<string>(maxLength: 200, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 600, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExternalUrl = table.Column<string>(nullable: true),
                    FontAwesomeIcon = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Contents_ContentTypes_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentTypes",
                        principalColumn: "ContentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contents_Contents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Contents",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contents_ATDataValues_PlacementId",
                        column: x => x.PlacementId,
                        principalTable: "ATDataValues",
                        principalColumn: "ATDataValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_ATDataValues_GenderId",
                        column: x => x.GenderId,
                        principalTable: "ATDataValues",
                        principalColumn: "ATDataValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    SaltKey = table.Column<string>(nullable: false),
                    OldPassword = table.Column<string>(nullable: true),
                    IsResetPassword = table.Column<bool>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserLoginId);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleLinks",
                columns: table => new
                {
                    UserRoleLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserRoleId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: false),
                    DeactivatedOn = table.Column<DateTime>(nullable: true),
                    InsertId = table.Column<Guid>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleLinks", x => x.UserRoleLinkId);
                    table.ForeignKey(
                        name: "FK_UserRoleLinks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleLinks_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackRecords",
                columns: table => new
                {
                    UserTrackRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserTrackTypeId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: false),
                    ClientName = table.Column<string>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackRecords", x => x.UserTrackRecordId);
                    table.ForeignKey(
                        name: "FK_UserTrackRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATDataValues_ATDataTypeId",
                table: "ATDataValues",
                column: "ATDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ParentId",
                table: "Contents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_PlacementId",
                table: "Contents",
                column: "PlacementId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordLogs_EntityId",
                table: "RecordLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCategoryMemberLinks_TeamCategoryId",
                table: "TeamCategoryMemberLinks",
                column: "TeamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCategoryMemberLinks_TeamMemberId",
                table: "TeamCategoryMemberLinks",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLinks_UserId",
                table: "UserRoleLinks",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLinks_UserRoleId",
                table: "UserRoleLinks",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackRecords_UserId",
                table: "UserTrackRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "RecordLogs");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "TeamCategoryMemberLinks");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoleLinks");

            migrationBuilder.DropTable(
                name: "UserTrackRecords");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropTable(
                name: "ATEntities");

            migrationBuilder.DropTable(
                name: "TeamCategories");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ATDataValues");

            migrationBuilder.DropTable(
                name: "ATDataTypes");
        }
    }
}

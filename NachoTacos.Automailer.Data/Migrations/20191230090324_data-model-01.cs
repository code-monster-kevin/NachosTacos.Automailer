using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NachoTacos.Automailer.Data.Migrations
{
    public partial class datamodel01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignActivities",
                columns: table => new
                {
                    CampaignActivityId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CampaignTrackingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignActivities", x => x.CampaignActivityId);
                });

            migrationBuilder.CreateTable(
                name: "CampaignContacts",
                columns: table => new
                {
                    CampaignId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignContacts", x => new { x.CampaignId, x.ContactId });
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CampaignId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CampaignSettings",
                columns: table => new
                {
                    CampaignSettingId = table.Column<Guid>(nullable: false),
                    CampaignId = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    SendDayAfterAdded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignSettings", x => x.CampaignSettingId);
                });

            migrationBuilder.CreateTable(
                name: "CampaignTrackings",
                columns: table => new
                {
                    CampaignTrackingId = table.Column<Guid>(nullable: false),
                    CampaignId = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    EmailModelId = table.Column<Guid>(nullable: false),
                    SentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignTrackings", x => x.CampaignTrackingId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    Unsubscribe = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    IdentityNo = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "EmailModels",
                columns: table => new
                {
                    EmailModelId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Text1 = table.Column<string>(nullable: true),
                    Text2 = table.Column<string>(nullable: true),
                    Text3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailModels", x => x.EmailModelId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    EmailFrom = table.Column<string>(nullable: false),
                    EmailSubject = table.Column<string>(nullable: false),
                    EmailContent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.EmailTemplateId);
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Code", "CampaignId", "CreatedDate", "Description", "UpdatedDate" },
                values: new object[] { "DEF", new Guid("905c3cbe-e2af-4323-add6-6b2350501da7"), new DateTime(2019, 12, 30, 9, 3, 24, 431, DateTimeKind.Utc).AddTicks(7368), "Default Campaign", new DateTime(2019, 12, 30, 9, 3, 24, 431, DateTimeKind.Utc).AddTicks(7899) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignActivities");

            migrationBuilder.DropTable(
                name: "CampaignContacts");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "CampaignSettings");

            migrationBuilder.DropTable(
                name: "CampaignTrackings");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "EmailModels");

            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}

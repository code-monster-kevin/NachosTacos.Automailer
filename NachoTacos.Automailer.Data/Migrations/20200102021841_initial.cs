using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NachoTacos.Automailer.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutomailerTasks",
                columns: table => new
                {
                    AutomailerTaskId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomailerTasks", x => x.AutomailerTaskId);
                });

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
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    JoinedDate = table.Column<DateTime>(nullable: false)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CampaignId = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    SendAfterJoined = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
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
                    CampaignSettingId = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AutomailerModels",
                columns: table => new
                {
                    AutomailerModelId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    TrackingLink = table.Column<string>(nullable: true),
                    UnsubscribeLink = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Text1 = table.Column<string>(nullable: true),
                    Text2 = table.Column<string>(nullable: true),
                    Text3 = table.Column<string>(nullable: true),
                    AutomailerTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomailerModels", x => x.AutomailerModelId);
                    table.ForeignKey(
                        name: "FK_AutomailerModels_AutomailerTasks_AutomailerTaskId",
                        column: x => x.AutomailerTaskId,
                        principalTable: "AutomailerTasks",
                        principalColumn: "AutomailerTaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CampaignContacts",
                columns: new[] { "CampaignId", "ContactId", "CreatedDate", "JoinedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new Guid("a0b46943-45b2-4b6c-bf07-8134339b667e"), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4374), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(3865), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4377) },
                    { new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new Guid("f9a46200-f300-402a-a91b-e26773560c76"), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4462), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4449), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4463) },
                    { new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new Guid("01134e10-a5a5-419c-8a33-5aa3404eb6c9"), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4467), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4466), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4467) },
                    { new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new Guid("794eb60f-d56f-433e-9f65-fc82987faefa"), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4470), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4469), new DateTime(2020, 1, 2, 2, 18, 40, 849, DateTimeKind.Utc).AddTicks(4471) }
                });

            migrationBuilder.InsertData(
                table: "CampaignSettings",
                columns: new[] { "CampaignSettingId", "Active", "CampaignId", "CreatedDate", "EmailTemplateId", "SendAfterJoined", "UpdatedDate" },
                values: new object[] { new Guid("be9d40a0-987e-46d4-8343-4f306aa4d0e5"), false, new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new DateTime(2020, 1, 2, 2, 18, 40, 850, DateTimeKind.Utc).AddTicks(3802), new Guid("e0b3fd72-8177-4e93-9627-a31603166643"), 1, new DateTime(2020, 1, 2, 2, 18, 40, 850, DateTimeKind.Utc).AddTicks(3809) });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Code", "CampaignId", "CreatedDate", "Description", "UpdatedDate" },
                values: new object[,]
                {
                    { "DEF", new Guid("905c3cbe-e2af-4323-add6-6b2350501da7"), new DateTime(2020, 1, 2, 2, 18, 40, 846, DateTimeKind.Utc).AddTicks(1118), "Default Campaign", new DateTime(2020, 1, 2, 2, 18, 40, 846, DateTimeKind.Utc).AddTicks(1638) },
                    { "TEST", new Guid("0ff7ff45-5250-44ca-9997-c4825b8092cb"), new DateTime(2020, 1, 2, 2, 18, 40, 847, DateTimeKind.Utc).AddTicks(8493), "A test campaign", new DateTime(2020, 1, 2, 2, 18, 40, 847, DateTimeKind.Utc).AddTicks(8510) }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Email", "ContactId", "CreatedDate", "IdentityNo", "Mobile", "Name", "Nationality", "Source", "Unsubscribe", "UpdatedDate" },
                values: new object[,]
                {
                    { "joe.test@mail.com", new Guid("a0b46943-45b2-4b6c-bf07-8134339b667e"), new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7522), null, null, "Joe Test", null, "seed data", false, new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7528) },
                    { "jane.test@mail.com", new Guid("f9a46200-f300-402a-a91b-e26773560c76"), new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7594), null, null, "Jane Test", null, "seed data", false, new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7596) },
                    { "ken.test@mail.com", new Guid("01134e10-a5a5-419c-8a33-5aa3404eb6c9"), new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7611), null, null, "Ken Test", null, "seed data", false, new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7612) },
                    { "kelly.test@mail.com", new Guid("794eb60f-d56f-433e-9f65-fc82987faefa"), new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7615), null, null, "Kelly Test", null, "seed data", false, new DateTime(2020, 1, 2, 2, 18, 40, 848, DateTimeKind.Utc).AddTicks(7616) }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "EmailTemplateId", "CreatedDate", "EmailContent", "EmailFrom", "EmailSubject", "UpdatedDate" },
                values: new object[] { new Guid("e0b3fd72-8177-4e93-9627-a31603166643"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "<b>Hi @Model.Name,<br />We are having a<font color= 'red' > Great </ font > offer today!<br />Don't miss it!<br /><a href='@Model.UnsubscribeLink'>Unsubscribe</a> <img src='@Model.TrackingLink' />", "tester1@nachotacos.com", "This is a test email", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_AutomailerModels_AutomailerTaskId",
                table: "AutomailerModels",
                column: "AutomailerTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutomailerModels");

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
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "AutomailerTasks");
        }
    }
}

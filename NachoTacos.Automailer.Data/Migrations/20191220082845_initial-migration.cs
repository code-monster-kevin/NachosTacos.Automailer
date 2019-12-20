using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NachoTacos.Automailer.Data.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailSubject = table.Column<string>(nullable: true),
                    EmailContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailerTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailerTasks_EmailTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplateModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AutomailerTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplateModels_MailerTasks_AutomailerTaskId",
                        column: x => x.AutomailerTaskId,
                        principalTable: "MailerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplateModels_AutomailerTaskId",
                table: "EmailTemplateModels",
                column: "AutomailerTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MailerTasks_EmailTemplateId",
                table: "MailerTasks",
                column: "EmailTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplateModels");

            migrationBuilder.DropTable(
                name: "MailerTasks");

            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}

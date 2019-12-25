using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NachoTacos.Automailer.Data.Migrations
{
    public partial class datamodel001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailModels",
                columns: table => new
                {
                    EmailModelId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
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
                    EmailSubject = table.Column<string>(nullable: false),
                    EmailContent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.EmailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTasks",
                columns: table => new
                {
                    EmailTaskId = table.Column<Guid>(nullable: false),
                    EmailTemplateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTasks", x => x.EmailTaskId);
                    table.ForeignKey(
                        name: "FK_EmailTasks_EmailTemplates_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplates",
                        principalColumn: "EmailTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTaskModels",
                columns: table => new
                {
                    EmailTaskId = table.Column<Guid>(nullable: false),
                    EmailModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTaskModels", x => new { x.EmailTaskId, x.EmailModelId });
                    table.ForeignKey(
                        name: "FK_EmailTaskModels_EmailModels_EmailModelId",
                        column: x => x.EmailModelId,
                        principalTable: "EmailModels",
                        principalColumn: "EmailModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailTaskModels_EmailTasks_EmailTaskId",
                        column: x => x.EmailTaskId,
                        principalTable: "EmailTasks",
                        principalColumn: "EmailTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTaskModels_EmailModelId",
                table: "EmailTaskModels",
                column: "EmailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTasks_EmailTemplateId",
                table: "EmailTasks",
                column: "EmailTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTaskModels");

            migrationBuilder.DropTable(
                name: "EmailModels");

            migrationBuilder.DropTable(
                name: "EmailTasks");

            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}

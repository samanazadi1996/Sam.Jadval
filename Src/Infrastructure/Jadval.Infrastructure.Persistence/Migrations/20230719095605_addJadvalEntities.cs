using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jadval.Infrastructure.Persistence.Migrations
{
    public partial class addJadvalEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jadvals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jadvals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JadvalQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JadvalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JadvalQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JadvalQuestions_Jadvals_JadvalId",
                        column: x => x.JadvalId,
                        principalTable: "Jadvals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JadvalQuestionValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JadvalQuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JadvalQuestionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JadvalQuestionValues_JadvalQuestions_JadvalQuestionId",
                        column: x => x.JadvalQuestionId,
                        principalTable: "JadvalQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JadvalQuestions_JadvalId",
                table: "JadvalQuestions",
                column: "JadvalId");

            migrationBuilder.CreateIndex(
                name: "IX_JadvalQuestionValues_JadvalQuestionId",
                table: "JadvalQuestionValues",
                column: "JadvalQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JadvalQuestionValues");

            migrationBuilder.DropTable(
                name: "JadvalQuestions");

            migrationBuilder.DropTable(
                name: "Jadvals");
        }
    }
}

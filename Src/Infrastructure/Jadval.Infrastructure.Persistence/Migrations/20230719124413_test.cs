using Microsoft.EntityFrameworkCore.Migrations;

namespace Jadval.Infrastructure.Persistence.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrosswordQuestions_Crosswords_CrosswordId",
                table: "CrosswordQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_CrosswordQuestionValues_CrosswordQuestions_CrosswordQuestionId",
                table: "CrosswordQuestionValues");

            migrationBuilder.AlterColumn<long>(
                name: "CrosswordQuestionId",
                table: "CrosswordQuestionValues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CrosswordId",
                table: "CrosswordQuestions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "CrosswordQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CrosswordQuestions_Crosswords_CrosswordId",
                table: "CrosswordQuestions",
                column: "CrosswordId",
                principalTable: "Crosswords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CrosswordQuestionValues_CrosswordQuestions_CrosswordQuestionId",
                table: "CrosswordQuestionValues",
                column: "CrosswordQuestionId",
                principalTable: "CrosswordQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrosswordQuestions_Crosswords_CrosswordId",
                table: "CrosswordQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_CrosswordQuestionValues_CrosswordQuestions_CrosswordQuestionId",
                table: "CrosswordQuestionValues");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "CrosswordQuestions");

            migrationBuilder.AlterColumn<long>(
                name: "CrosswordQuestionId",
                table: "CrosswordQuestionValues",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CrosswordId",
                table: "CrosswordQuestions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_CrosswordQuestions_Crosswords_CrosswordId",
                table: "CrosswordQuestions",
                column: "CrosswordId",
                principalTable: "Crosswords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrosswordQuestionValues_CrosswordQuestions_CrosswordQuestionId",
                table: "CrosswordQuestionValues",
                column: "CrosswordQuestionId",
                principalTable: "CrosswordQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

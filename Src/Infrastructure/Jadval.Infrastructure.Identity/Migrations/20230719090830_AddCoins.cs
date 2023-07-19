using Microsoft.EntityFrameworkCore.Migrations;

namespace Jadval.Infrastructure.Identity.Migrations
{
    public partial class AddCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Coins",
                schema: "Identity",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Level",
                schema: "Identity",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coins",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Level",
                schema: "Identity",
                table: "User");
        }
    }
}

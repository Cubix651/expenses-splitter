using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class AddEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "SettlementUsers");
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "SettlementUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "SettlementUsers");
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "SettlementUsers",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class FkIdOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdOwner",
                table: "Settlements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOwner",
                table: "Settlements");
        }
    }
}

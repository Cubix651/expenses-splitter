using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class AddFirendsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Friends",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Friends");
        }
    }
}

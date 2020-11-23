using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class UserColumnInSettlements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ownerId",
                table: "Settlements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_ownerId",
                table: "Settlements",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_Users_ownerId",
                table: "Settlements",
                column: "ownerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_Users_ownerId",
                table: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Settlements_ownerId",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "Settlements");
        }
    }
}

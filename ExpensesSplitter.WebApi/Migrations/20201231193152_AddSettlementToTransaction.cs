using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class AddSettlementToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SettlementId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SettlementId",
                table: "Transactions",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Settlements_SettlementId",
                table: "Transactions",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Settlements_SettlementId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SettlementId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Transactions");
        }
    }
}

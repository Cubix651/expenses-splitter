using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class AddExpenseParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseParticipations",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(nullable: false),
                    SettlementUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseParticipations", x => new { x.ExpenseId, x.SettlementUserId });
                    table.ForeignKey(
                        name: "FK_ExpenseParticipations_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipations_SettlementUsers_SettlementUserId",
                        column: x => x.SettlementUserId,
                        principalTable: "SettlementUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipations_SettlementUserId",
                table: "ExpenseParticipations",
                column: "SettlementUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseParticipations");
        }
    }
}

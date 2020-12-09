using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class AddWhoPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SettlementUsers",
                column: "Id",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
            
            migrationBuilder.AddColumn<Guid>(
                name: "WhoPaidId",
                table: "Expenses",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_WhoPaidId",
                table: "Expenses",
                column: "WhoPaidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SettlementUsers_WhoPaidId",
                table: "Expenses",
                column: "WhoPaidId",
                principalTable: "SettlementUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SettlementUsers_WhoPaidId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_WhoPaidId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "WhoPaidId",
                table: "Expenses");
            
            migrationBuilder.DeleteData(
                table: "SettlementUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

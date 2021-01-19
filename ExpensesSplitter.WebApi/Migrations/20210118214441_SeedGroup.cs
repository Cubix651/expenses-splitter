using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class SeedGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Groups",
            columns: new[] { "Id", "Name", "SettlementId" },
            values: new object[] { Guid.Parse("00000000-0000-0000-0000-000000000000"), "Indywidualna", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesSplitter.WebApi.Migrations
{
    public partial class CreateTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_Users_ownerId",
                table: "Settlements");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Settlements",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Settlements_ownerId",
                table: "Settlements",
                newName: "IX_Settlements_OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SettlementUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromId = table.Column<Guid>(nullable: false),
                    ToId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_SettlementUsers_FromId",
                        column: x => x.FromId,
                        principalTable: "SettlementUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transactions_SettlementUsers_ToId",
                        column: x => x.ToId,
                        principalTable: "SettlementUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettlementUsers_UserId",
                table: "SettlementUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromId",
                table: "Transactions",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToId",
                table: "Transactions",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_Users_OwnerId",
                table: "Settlements",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementUsers_Users_UserId",
                table: "SettlementUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_Users_OwnerId",
                table: "Settlements");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementUsers_Users_UserId",
                table: "SettlementUsers");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_SettlementUsers_UserId",
                table: "SettlementUsers");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Settlements",
                newName: "ownerId");

            migrationBuilder.RenameIndex(
                name: "IX_Settlements_OwnerId",
                table: "Settlements",
                newName: "IX_Settlements_ownerId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SettlementUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_Users_ownerId",
                table: "Settlements",
                column: "ownerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

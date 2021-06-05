using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator1.Migrations
{
    public partial class Autentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserExpensesList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpensesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExpensesList_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesUserExpensesList",
                columns: table => new
                {
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    MonthlyUserExpensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesUserExpensesList", x => new { x.ExpensesId, x.MonthlyUserExpensesId });
                    table.ForeignKey(
                        name: "FK_ExpensesUserExpensesList_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpensesUserExpensesList_UserExpensesList_MonthlyUserExpensesId",
                        column: x => x.MonthlyUserExpensesId,
                        principalTable: "UserExpensesList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesUserExpensesList_MonthlyUserExpensesId",
                table: "ExpensesUserExpensesList",
                column: "MonthlyUserExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpensesList_ApplicationUserId",
                table: "UserExpensesList",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesUserExpensesList");

            migrationBuilder.DropTable(
                name: "UserExpensesList");
        }
    }
}

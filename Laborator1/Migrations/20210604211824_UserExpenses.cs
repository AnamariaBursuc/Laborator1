using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator1.Migrations
{
    public partial class UserExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesUserExpensesList_UserExpensesList_MonthlyUserExpensesId",
                table: "ExpensesUserExpensesList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExpensesList_AspNetUsers_ApplicationUserId",
                table: "UserExpensesList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExpensesList",
                table: "UserExpensesList");

            migrationBuilder.RenameTable(
                name: "UserExpensesList",
                newName: "UserExpensesLists");

            migrationBuilder.RenameColumn(
                name: "MonthlyUserExpensesId",
                table: "ExpensesUserExpensesList",
                newName: "UserExpensesListsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesUserExpensesList_MonthlyUserExpensesId",
                table: "ExpensesUserExpensesList",
                newName: "IX_ExpensesUserExpensesList_UserExpensesListsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExpensesList_ApplicationUserId",
                table: "UserExpensesLists",
                newName: "IX_UserExpensesLists_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExpensesLists",
                table: "UserExpensesLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesUserExpensesList_UserExpensesLists_UserExpensesListsId",
                table: "ExpensesUserExpensesList",
                column: "UserExpensesListsId",
                principalTable: "UserExpensesLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExpensesLists_AspNetUsers_ApplicationUserId",
                table: "UserExpensesLists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesUserExpensesList_UserExpensesLists_UserExpensesListsId",
                table: "ExpensesUserExpensesList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExpensesLists_AspNetUsers_ApplicationUserId",
                table: "UserExpensesLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExpensesLists",
                table: "UserExpensesLists");

            migrationBuilder.RenameTable(
                name: "UserExpensesLists",
                newName: "UserExpensesList");

            migrationBuilder.RenameColumn(
                name: "UserExpensesListsId",
                table: "ExpensesUserExpensesList",
                newName: "MonthlyUserExpensesId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesUserExpensesList_UserExpensesListsId",
                table: "ExpensesUserExpensesList",
                newName: "IX_ExpensesUserExpensesList_MonthlyUserExpensesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExpensesLists_ApplicationUserId",
                table: "UserExpensesList",
                newName: "IX_UserExpensesList_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExpensesList",
                table: "UserExpensesList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesUserExpensesList_UserExpensesList_MonthlyUserExpensesId",
                table: "ExpensesUserExpensesList",
                column: "MonthlyUserExpensesId",
                principalTable: "UserExpensesList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExpensesList_AspNetUsers_ApplicationUserId",
                table: "UserExpensesList",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator1.Migrations
{
    public partial class addComentsToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Expenses_ExpensesId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ExpensesId",
                table: "Comment",
                newName: "ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ExpensesId",
                table: "Comment",
                newName: "IX_Comment_ExpenseId");

            migrationBuilder.AddColumn<long>(
                name: "MovieId",
                table: "Comment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Comment",
                newName: "ExpensesId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ExpenseId",
                table: "Comment",
                newName: "IX_Comment_ExpensesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Expenses_ExpensesId",
                table: "Comment",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

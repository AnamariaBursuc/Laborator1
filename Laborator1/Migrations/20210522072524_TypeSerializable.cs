using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator1.Migrations
{
    public partial class TypeSerializable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Expenses_ExpenseId",
                table: "Comment",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

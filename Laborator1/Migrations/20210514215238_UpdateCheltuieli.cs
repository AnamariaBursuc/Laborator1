using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator1.Migrations
{
    public partial class UpdateCheltuieli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cheltuieli",
                table: "cheltuieli");

            migrationBuilder.RenameTable(
                name: "cheltuieli",
                newName: "Cheltuieli");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cheltuieli",
                table: "Cheltuieli",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cheltuieli",
                table: "Cheltuieli");

            migrationBuilder.RenameTable(
                name: "Cheltuieli",
                newName: "cheltuieli");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cheltuieli",
                table: "cheltuieli",
                column: "Id");
        }
    }
}

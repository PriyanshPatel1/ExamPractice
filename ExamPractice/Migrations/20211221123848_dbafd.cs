using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPractice.Migrations
{
    public partial class dbafd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_emp",
                table: "emp");

            migrationBuilder.RenameTable(
                name: "emp",
                newName: "Temps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temps",
                table: "Temps",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temps",
                table: "Temps");

            migrationBuilder.RenameTable(
                name: "Temps",
                newName: "emp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_emp",
                table: "emp",
                column: "Id");
        }
    }
}

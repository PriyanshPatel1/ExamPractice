using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPractice.Migrations
{
    public partial class dba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "emp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emp", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emp");
        }
    }
}

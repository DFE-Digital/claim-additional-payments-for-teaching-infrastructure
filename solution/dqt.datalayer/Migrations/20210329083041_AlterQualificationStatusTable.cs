using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class AlterQualificationStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ITTStartDate",
                table: "QualifiedTeachersBackup",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "QualificationName",
                table: "QualifiedTeachersBackup",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ITTStartDate",
                table: "QualifiedTeachers",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "QualificationName",
                table: "QualifiedTeachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITTStartDate",
                table: "QualifiedTeachersBackup");

            migrationBuilder.DropColumn(
                name: "QualificationName",
                table: "QualifiedTeachersBackup");

            migrationBuilder.DropColumn(
                name: "ITTStartDate",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "QualificationName",
                table: "QualifiedTeachers");
        }
    }
}

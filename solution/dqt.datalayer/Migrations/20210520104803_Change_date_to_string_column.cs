using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class Change_date_to_string_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QTSAwardDate",
                table: "QualifiedTeachersBackup",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "ITTStartDate",
                table: "QualifiedTeachersBackup",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "DoB",
                table: "QualifiedTeachersBackup",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "QTSAwardDate",
                table: "QualifiedTeachers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "ITTStartDate",
                table: "QualifiedTeachers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "DoB",
                table: "QualifiedTeachers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "QTSAwardDate",
                table: "QualifiedTeachersBackup",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ITTStartDate",
                table: "QualifiedTeachersBackup",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "QualifiedTeachersBackup",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "QTSAwardDate",
                table: "QualifiedTeachers",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ITTStartDate",
                table: "QualifiedTeachers",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "QualifiedTeachers",
                type: "Date",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

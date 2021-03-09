using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class RemoveUGSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientReference",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "HigherEducationSubjectCode",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "InitialTeacherTrainingSubjectCode",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "NationalInsuranceNumber",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "QualifiedTeachingStatusAwardDate",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "TeacherReferenceNumber",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "UndergraduateSubject",
                table: "QualifiedTeachers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoB",
                table: "QualifiedTeachers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ITTSubject1Code",
                table: "QualifiedTeachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITTSubject2Code",
                table: "QualifiedTeachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITTSubject3Code",
                table: "QualifiedTeachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NINumber",
                table: "QualifiedTeachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QualifiedTeachers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QTSAwardDate",
                table: "QualifiedTeachers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Trn",
                table: "QualifiedTeachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoB",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "ITTSubject1Code",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "ITTSubject2Code",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "ITTSubject3Code",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "NINumber",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "QTSAwardDate",
                table: "QualifiedTeachers");

            migrationBuilder.DropColumn(
                name: "Trn",
                table: "QualifiedTeachers");

            migrationBuilder.AddColumn<string>(
                name: "ClientReference",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "QualifiedTeachers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HigherEducationSubjectCode",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitialTeacherTrainingSubjectCode",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalInsuranceNumber",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QualifiedTeachingStatusAwardDate",
                table: "QualifiedTeachers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TeacherReferenceNumber",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UndergraduateSubject",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);
        }
    }
}

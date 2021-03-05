using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dqt.datalayer.Migrations
{
    public partial class IntialTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QualifiedTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TeacherReferenceNumber = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    NationalInsuranceNumber = table.Column<string>(nullable: true),
                    QualifiedTeachingStatusAwardDate = table.Column<DateTime>(nullable: false),
                    UndergraduateSubject = table.Column<string>(nullable: true),
                    HigherEducationSubjectCode = table.Column<string>(nullable: true),
                    InitialTeacherTrainingSubjectCode = table.Column<string>(nullable: true),
                    ClientReference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifiedTeachers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualifiedTeachers");
        }
    }
}

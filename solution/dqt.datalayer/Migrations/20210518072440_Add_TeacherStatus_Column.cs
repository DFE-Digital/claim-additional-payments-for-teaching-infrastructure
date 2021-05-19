using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class Add_TeacherStatus_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherStatus",
                table: "QualifiedTeachersBackup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherStatus",
                table: "QualifiedTeachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherStatus",
                table: "QualifiedTeachersBackup");

            migrationBuilder.DropColumn(
                name: "TeacherStatus",
                table: "QualifiedTeachers");
        }
    }
}

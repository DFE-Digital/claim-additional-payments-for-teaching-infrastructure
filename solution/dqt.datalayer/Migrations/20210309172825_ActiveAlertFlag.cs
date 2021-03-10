using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class ActiveAlertFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveAlert",
                table: "QualifiedTeachers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveAlert",
                table: "QualifiedTeachers");
        }
    }
}

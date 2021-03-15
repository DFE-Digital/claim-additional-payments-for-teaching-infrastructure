using Microsoft.EntityFrameworkCore.Migrations;

namespace dqt.datalayer.Migrations
{
    public partial class RemoveEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "QualifiedTeachers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "QualifiedTeachers",
                type: "text",
                nullable: true);
        }
    }
}

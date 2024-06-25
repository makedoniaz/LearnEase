using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnEase.Migrations
{
    /// <inheritdoc />
    public partial class CourseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseLogoPath",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLogoPath",
                table: "Courses");
        }
    }
}

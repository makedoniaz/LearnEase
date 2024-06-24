using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration.UserSecrets;

#nullable disable

namespace LearnEase.Migrations
{
    /// <inheritdoc />
    public partial class FixUserIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses"
            );

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Feedbacks"
            );

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Courses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Feedbacks",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Courses",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Feedbacks",
                newName: "UserId1");
        }
    }
}

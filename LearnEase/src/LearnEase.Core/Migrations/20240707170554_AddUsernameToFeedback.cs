using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnEase.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Feedbacks");
        }
    }
}

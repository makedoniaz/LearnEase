using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnEase.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFKToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");
        }
    }
}

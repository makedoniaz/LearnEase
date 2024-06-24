using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnEase.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Feedbacks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Courses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId1",
                table: "Feedbacks",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId1",
                table: "Courses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId1",
                table: "Courses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_UserId1",
                table: "Feedbacks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_UserId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_UserId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Courses");
        }
    }
}

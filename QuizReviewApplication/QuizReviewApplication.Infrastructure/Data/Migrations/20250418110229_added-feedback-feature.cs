using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizReviewApplication.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedfeedbackfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Answers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Answers");
        }
    }
}

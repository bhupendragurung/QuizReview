using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizReviewApplication.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixanswerentity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Quizzes_QuizId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuizId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuizId",
                table: "Answers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizId",
                table: "Answers",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Quizzes_QuizId",
                table: "Answers",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizReviewApplication.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixanswerentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatgegoryId",
                table: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CatgegoryId",
                table: "Answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

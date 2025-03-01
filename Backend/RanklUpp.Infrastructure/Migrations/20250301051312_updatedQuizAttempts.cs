using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RanklUpp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedQuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "quiz_attempts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_quiz_attempts_QuizId",
                table: "quiz_attempts",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_attempts_quizzes_QuizId",
                table: "quiz_attempts",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_attempts_quizzes_QuizId",
                table: "quiz_attempts");

            migrationBuilder.DropIndex(
                name: "IX_quiz_attempts_QuizId",
                table: "quiz_attempts");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "quiz_attempts");
        }
    }
}

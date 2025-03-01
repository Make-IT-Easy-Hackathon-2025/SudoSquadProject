using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RanklUpp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedScoreToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "quiz_attempts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "quiz_attempts");
        }
    }
}

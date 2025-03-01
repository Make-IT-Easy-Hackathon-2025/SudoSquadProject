using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RanklUpp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "memories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "memories");
        }
    }
}

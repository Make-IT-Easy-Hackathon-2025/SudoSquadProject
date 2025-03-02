using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RanklUpp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedRoadMapToMemory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoadMapId",
                table: "memories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_memories_RoadMapId",
                table: "memories",
                column: "RoadMapId");

            migrationBuilder.AddForeignKey(
                name: "FK_memories_roadmaps_RoadMapId",
                table: "memories",
                column: "RoadMapId",
                principalTable: "roadmaps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memories_roadmaps_RoadMapId",
                table: "memories");

            migrationBuilder.DropIndex(
                name: "IX_memories_RoadMapId",
                table: "memories");

            migrationBuilder.DropColumn(
                name: "RoadMapId",
                table: "memories");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RanklUpp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedRoadMaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roadmaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadmaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roadmap_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    RoadMapId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadmap_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roadmap_items_roadmaps_RoadMapId",
                        column: x => x.RoadMapId,
                        principalTable: "roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roadmap_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoadMapItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roadmap_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_roadmap_items_roadmap_items_RoadMapItemId",
                        column: x => x.RoadMapItemId,
                        principalTable: "roadmap_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roadmap_items_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roadmap_items_RoadMapId",
                table: "roadmap_items",
                column: "RoadMapId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roadmap_items_RoadMapItemId",
                table: "user_roadmap_items",
                column: "RoadMapItemId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roadmap_items_UserId",
                table: "user_roadmap_items",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roadmap_items");

            migrationBuilder.DropTable(
                name: "roadmap_items");

            migrationBuilder.DropTable(
                name: "roadmaps");
        }
    }
}

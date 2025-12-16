using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wayplot_Backend.Migrations
{
    /// <inheritdoc />
    public partial class MapUserLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Maps",
                newName: "JsonUrl");

            migrationBuilder.AddColumn<string>(
                name: "GpxUrl",
                table: "Maps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MapSharedUsers",
                columns: table => new
                {
                    MapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapSharedUsers", x => new { x.MapId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MapSharedUsers_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapSharedUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapSharedUsers_UserId",
                table: "MapSharedUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapSharedUsers");

            migrationBuilder.DropColumn(
                name: "GpxUrl",
                table: "Maps");

            migrationBuilder.RenameColumn(
                name: "JsonUrl",
                table: "Maps",
                newName: "Url");
        }
    }
}

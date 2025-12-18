using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wayplot_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AnalyticRecordAddStatusfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AnalyticRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AnalyticRecords");
        }
    }
}

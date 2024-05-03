using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackendLearning.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeviceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "devices",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "devices");
        }
    }
}

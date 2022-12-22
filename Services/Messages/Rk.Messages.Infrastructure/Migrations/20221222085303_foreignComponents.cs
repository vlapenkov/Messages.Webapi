using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class foreignComponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "areforeigncomponentsused",
                table: "baseproduct",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "shareofforeigncomponents",
                table: "baseproduct",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "areforeigncomponentsused",
                table: "baseproduct");

            migrationBuilder.DropColumn(
                name: "shareofforeigncomponents",
                table: "baseproduct");
        }
    }
}

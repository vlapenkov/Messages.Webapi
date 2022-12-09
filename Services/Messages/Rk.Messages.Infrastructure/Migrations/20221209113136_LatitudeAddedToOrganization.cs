using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LatitudeAddedToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "organizations",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "organizations",
                type: "double precision",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "latitude", "longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "latitude", "longitude" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "organizations");
        }
    }
}

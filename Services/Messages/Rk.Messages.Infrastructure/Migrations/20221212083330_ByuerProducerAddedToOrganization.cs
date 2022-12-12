using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ByuerProducerAddedToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isbuyer",
                table: "organizations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isproducer",
                table: "organizations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "isbuyer", "isproducer" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "isbuyer", "isproducer" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isbuyer",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "isproducer",
                table: "organizations");
        }
    }
}

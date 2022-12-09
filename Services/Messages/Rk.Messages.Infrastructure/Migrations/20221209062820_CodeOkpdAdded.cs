using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodeOkpdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "baseproduct",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "codeokpd2",
                table: "baseproduct",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "baseproduct");

            migrationBuilder.DropColumn(
                name: "codeokpd2",
                table: "baseproduct");
        }
    }
}

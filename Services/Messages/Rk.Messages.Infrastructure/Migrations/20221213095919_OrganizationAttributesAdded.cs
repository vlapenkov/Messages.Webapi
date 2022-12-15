using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationAttributesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "account",
                table: "organizations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bankname",
                table: "organizations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bik",
                table: "organizations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "corraccount",
                table: "organizations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "documentid",
                table: "organizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "factaddress",
                table: "organizations",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "account", "bankname", "bik", "corraccount", "documentid", "factaddress" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "account", "bankname", "bik", "corraccount", "documentid", "factaddress" },
                values: new object[] { null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "account",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "bankname",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "bik",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "corraccount",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "documentid",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "factaddress",
                table: "organizations");
        }
    }
}

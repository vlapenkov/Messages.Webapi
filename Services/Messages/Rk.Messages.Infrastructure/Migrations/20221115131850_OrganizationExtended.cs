using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    public partial class OrganizationExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastmodifiedby",
                table: "organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "okved",
                table: "organizations",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "okved2",
                table: "organizations",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L,
                column: "status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L,
                column: "status",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "ix_organizations_ogrn",
                table: "organizations",
                column: "ogrn",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_organizations_ogrn",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "created",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "okved",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "okved2",
                table: "organizations");

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L,
                column: "status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L,
                column: "status",
                value: 0);
        }
    }
}

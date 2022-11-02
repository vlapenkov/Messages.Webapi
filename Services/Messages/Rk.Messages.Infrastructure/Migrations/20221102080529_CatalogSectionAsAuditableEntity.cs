using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class CatalogSectionAsAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "catalogsections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "catalogsections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "catalogsections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastmodifiedby",
                table: "catalogsections",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "catalogsections");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "catalogsections");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "catalogsections");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "catalogsections");
        }
    }
}

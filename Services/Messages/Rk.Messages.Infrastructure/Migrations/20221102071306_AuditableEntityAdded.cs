using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class AuditableEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "baseproduct",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "baseproduct",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "baseproduct",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastmodifiedby",
                table: "baseproduct",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "baseproduct");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "baseproduct");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "baseproduct");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "baseproduct");
        }
    }
}

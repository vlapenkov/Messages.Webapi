using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class ShoppingCartAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "shoppingcartitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "shoppingcartitems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "shoppingcartitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastmodifiedby",
                table: "shoppingcartitems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastmodifiedby",
                table: "orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "shoppingcartitems");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "shoppingcartitems");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "shoppingcartitems");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "shoppingcartitems");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "orders");
        }
    }
}

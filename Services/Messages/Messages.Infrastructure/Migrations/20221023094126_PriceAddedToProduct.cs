using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class PriceAddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "baseproduct",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "baseproduct");
        }
    }
}

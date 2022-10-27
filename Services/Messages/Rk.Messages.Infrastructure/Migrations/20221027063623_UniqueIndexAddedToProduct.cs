using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class UniqueIndexAddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_baseproduct_name",
                table: "baseproduct",
                column: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_baseproduct_name",
                table: "baseproduct");
        }
    }
}

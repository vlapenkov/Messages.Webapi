using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProducerAddedToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "producerid",
                table: "orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_orders_producerid",
                table: "orders",
                column: "producerid");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_organizations_producerid",
                table: "orders",
                column: "producerid",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_organizations_producerid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_producerid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "producerid",
                table: "orders");
        }
    }
}

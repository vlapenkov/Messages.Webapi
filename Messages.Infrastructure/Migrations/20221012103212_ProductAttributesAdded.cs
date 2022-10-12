using Microsoft.EntityFrameworkCore.Migrations;

namespace Messages.Infrastructure.Migrations
{
    public partial class ProductAttributesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Вес" },
                    { 2L, "Длина" },
                    { 3L, "Ширина" },
                    { 4L, "Цвет" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 4L);
        }
    }
}

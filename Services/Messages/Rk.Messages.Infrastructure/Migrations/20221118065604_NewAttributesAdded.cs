using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    public partial class NewAttributesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "Масса, кг");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Длина, м");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 3L,
                column: "name",
                value: "Ширина, м ");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 5L,
                column: "name",
                value: "Объем, куб. м");

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 6L, "Диапазон измерений,  Дб" },
                    { 7L, "Частотный диапазон,  Гц" },
                    { 8L, "Срок службы,  лет" },
                    { 9L, "Погрешность, не более,  Дб" },
                    { 10L, "Напряжение питания постоянного тока,  В" },
                    { 11L, "Ток потребления, не более,  мА" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "Вес");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Длина");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 3L,
                column: "name",
                value: "Ширина");

            migrationBuilder.UpdateData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 5L,
                column: "name",
                value: "Объем");
        }
    }
}

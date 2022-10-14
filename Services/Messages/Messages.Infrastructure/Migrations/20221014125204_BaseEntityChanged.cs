using Microsoft.EntityFrameworkCore.Migrations;

namespace Messages.Infrastructure.Migrations
{
    public partial class BaseEntityChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "fullname", "inn", "kpp", "name", "ogrn" },
                values: new object[] { 2L, "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "7404052938", "631201001", "Златоустовский машиностроительный завод", "1146312005344" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}

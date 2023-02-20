using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "account", "address", "bankname", "bik", "city", "corraccount", "created", "createdby", "documentid", "email", "factaddress", "fullname", "inn", "isbuyer", "isproducer", "kpp", "lastmodified", "lastmodifiedby", "latitude", "longitude", "name", "ogrn", "okved", "okved2", "phone", "region", "site", "status" },
                values: new object[,]
                {
                    { 1L, null, "Самарская обл., г. Самара, ул. Земеца, д. 18", null, null, "Самара", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Ракетно-космический центр «Прогресс», Самара", "6312139922", false, false, "631201001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, "Прогресс", "1146312005344", null, null, null, "Самарская область", null, 1 },
                    { 2L, null, "456227, Челябинская область, город Златоуст, Парковый проезд, 1", null, null, "Златоуст", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "7404052938", false, false, "631201001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, "Златоустовский машиностроительный завод", "1146312005355", null, null, null, "Челябинская область", "http://www.zlatmash.ru/", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "catalogsections",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parentcatalogsectionid = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_catalogsections", x => x.id);
                    table.ForeignKey(
                        name: "fk_catalogsections_catalogsections_catalogsectionid",
                        column: x => x.parentcatalogsectionid,
                        principalTable: "catalogsections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    fileid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    fullname = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    ogrn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    inn = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    kpp = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    region = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    city = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    address = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    site = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "baseproduct",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    catalogsectionid = table.Column<long>(type: "bigint", nullable: false),
                    organizationid = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    itemtype = table.Column<int>(type: "integer", nullable: false),
                    codetnved = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    fullname = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    measuringunit = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_baseproduct", x => x.id);
                    table.ForeignKey(
                        name: "fk_baseproduct_catalogsections_catalogsectionid",
                        column: x => x.catalogsectionid,
                        principalTable: "catalogsections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_baseproduct_organizations_organizationid",
                        column: x => x.organizationid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organizationid = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    comments = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_organizations_organizationid",
                        column: x => x.organizationid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attributevalues",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    baseproductid = table.Column<long>(type: "bigint", nullable: false),
                    attributeid = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attributevalues", x => x.id);
                    table.ForeignKey(
                        name: "fk_attributevalues_attributes_attributeid",
                        column: x => x.attributeid,
                        principalTable: "attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_attributevalues_baseproduct_baseproductid",
                        column: x => x.baseproductid,
                        principalTable: "baseproduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productdocuments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    baseproductid = table.Column<long>(type: "bigint", nullable: false),
                    documentid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productdocuments", x => x.id);
                    table.ForeignKey(
                        name: "fk_productdocuments_baseproduct_baseproductid",
                        column: x => x.baseproductid,
                        principalTable: "baseproduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productdocuments_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shoppingcartitems",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    productid = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shoppingcartitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_shoppingcartitems_baseproduct_productid",
                        column: x => x.productid,
                        principalTable: "baseproduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<long>(type: "bigint", nullable: false),
                    productid = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_orderitems_baseproduct_productid",
                        column: x => x.productid,
                        principalTable: "baseproduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orderitems_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Вес" },
                    { 2L, "Длина" },
                    { 3L, "Ширина" },
                    { 4L, "Цвет" },
                    { 5L, "Объем" }
                });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "address", "city", "fullname", "inn", "kpp", "name", "ogrn", "region", "site", "status" },
                values: new object[,]
                {
                    { 1L, "Самарская обл., г. Самара, ул. Земеца, д. 18", "Самара", "Ракетно-космический центр «Прогресс», Самара", "6312139922", "631201001", "Прогресс", "1146312005344", "Самарская область", null, 0 },
                    { 2L, "456227, Челябинская область, город Златоуст, Парковый проезд, 1", "Златоуст", "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "7404052938", "631201001", "Златоустовский машиностроительный завод", "1146312005344", "Челябинская область", "http://www.zlatmash.ru/", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_attributevalues_attributeid",
                table: "attributevalues",
                column: "attributeid");

            migrationBuilder.CreateIndex(
                name: "ix_attributevalues_baseproductid",
                table: "attributevalues",
                column: "baseproductid");

            migrationBuilder.CreateIndex(
                name: "ix_baseproduct_catalogsectionid",
                table: "baseproduct",
                column: "catalogsectionid");

            migrationBuilder.CreateIndex(
                name: "ix_baseproduct_organizationid",
                table: "baseproduct",
                column: "organizationid");

            migrationBuilder.CreateIndex(
                name: "ix_catalogsections_parentcatalogsectionid",
                table: "catalogsections",
                column: "parentcatalogsectionid");

            migrationBuilder.CreateIndex(
                name: "ix_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "ix_orderitems_productid",
                table: "orderitems",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_organizationid",
                table: "orders",
                column: "organizationid");

            migrationBuilder.CreateIndex(
                name: "ix_productdocuments_baseproductid",
                table: "productdocuments",
                column: "baseproductid");

            migrationBuilder.CreateIndex(
                name: "ix_productdocuments_documentid",
                table: "productdocuments",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_shoppingcartitems_productid",
                table: "shoppingcartitems",
                column: "productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attributevalues");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "productdocuments");

            migrationBuilder.DropTable(
                name: "shoppingcartitems");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "baseproduct");

            migrationBuilder.DropTable(
                name: "catalogsections");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}

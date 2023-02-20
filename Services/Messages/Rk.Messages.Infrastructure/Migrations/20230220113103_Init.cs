using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
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
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
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
                name: "news",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    documentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_news", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    fullname = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    ogrn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    inn = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    kpp = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    region = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    city = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    address = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    site = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    okved = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    okved2 = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    phone = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    isproducer = table.Column<bool>(type: "boolean", nullable: false),
                    isbuyer = table.Column<bool>(type: "boolean", nullable: false),
                    factaddress = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    bankname = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    account = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    corraccount = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    bik = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    documentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productsexchanges",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exchangetype = table.Column<int>(type: "integer", nullable: false),
                    productsloaded = table.Column<long>(type: "bigint", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productsexchanges", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sectiondocuments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    catalogsectionid = table.Column<long>(type: "bigint", nullable: false),
                    documentid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sectiondocuments", x => x.id);
                    table.ForeignKey(
                        name: "fk_sectiondocuments_catalogsections_catalogsectionid",
                        column: x => x.catalogsectionid,
                        principalTable: "catalogsections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sectiondocuments_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "baseproduct",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    catalogsectionid = table.Column<long>(type: "bigint", nullable: false),
                    organizationid = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    fullname = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    article = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<float>(type: "real", nullable: true),
                    areforeigncomponentsused = table.Column<bool>(type: "boolean", nullable: true),
                    itemtype = table.Column<int>(type: "integer", nullable: false),
                    codetnved = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    codeokpd2 = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    address = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    measuringunit = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    country = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    availablestatus = table.Column<int>(type: "integer", nullable: true),
                    shareofforeigncomponents = table.Column<float>(type: "real", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
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
                    producerid = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    comments = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_orders_organizations_producerid",
                        column: x => x.producerid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationdocuments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organizationid = table.Column<long>(type: "bigint", nullable: false),
                    documentid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationdocuments", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizationdocuments_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationdocuments_organizations_organizationid",
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
                    value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
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
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    baseproductid = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_baseproduct_baseproductid",
                        column: x => x.baseproductid,
                        principalTable: "baseproduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shoppingcartitems",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    productid = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: false),
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
                    { 1L, "Масса, кг" },
                    { 2L, "Длина, м" },
                    { 3L, "Ширина, м " },
                    { 4L, "Цвет" },
                    { 5L, "Объем, куб. м" },
                    { 6L, "Диапазон измерений,  Дб" },
                    { 7L, "Частотный диапазон,  Гц" },
                    { 8L, "Срок службы,  лет" },
                    { 9L, "Погрешность, не более,  Дб" },
                    { 10L, "Напряжение питания постоянного тока,  В" },
                    { 11L, "Ток потребления, не более,  мА" }
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
                name: "ix_orders_producerid",
                table: "orders",
                column: "producerid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationdocuments_documentid",
                table: "organizationdocuments",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationdocuments_organizationid",
                table: "organizationdocuments",
                column: "organizationid");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_ogrn",
                table: "organizations",
                column: "ogrn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_productdocuments_baseproductid",
                table: "productdocuments",
                column: "baseproductid");

            migrationBuilder.CreateIndex(
                name: "ix_productdocuments_documentid",
                table: "productdocuments",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_baseproductid",
                table: "reviews",
                column: "baseproductid");

            migrationBuilder.CreateIndex(
                name: "ix_sectiondocuments_catalogsectionid",
                table: "sectiondocuments",
                column: "catalogsectionid");

            migrationBuilder.CreateIndex(
                name: "ix_sectiondocuments_documentid",
                table: "sectiondocuments",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_shoppingcartitems_productid",
                table: "shoppingcartitems",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attributevalues");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "organizationdocuments");

            migrationBuilder.DropTable(
                name: "productdocuments");

            migrationBuilder.DropTable(
                name: "productsexchanges");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "sectiondocuments");

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

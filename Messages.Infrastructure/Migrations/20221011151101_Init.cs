using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Messages.Infrastructure.Migrations
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
                name: "catalogsection",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parentcatalogsectionid = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_catalogsection", x => x.id);
                    table.ForeignKey(
                        name: "fk_catalogsection_catalogsection_catalogsectionid",
                        column: x => x.parentcatalogsectionid,
                        principalTable: "catalogsection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    kpp = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true)
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
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    itemtype = table.Column<int>(type: "integer", nullable: false),
                    codetnved = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_baseproduct", x => x.id);
                    table.ForeignKey(
                        name: "fk_baseproduct_catalogsection_catalogsectionid",
                        column: x => x.catalogsectionid,
                        principalTable: "catalogsection",
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

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "fullname", "inn", "kpp", "name", "ogrn" },
                values: new object[] { 1L, "Ракетно-космический центр «Прогресс», Самара", "6312139922", "631201001", "Прогресс", "1146312005344" });

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
                name: "ix_catalogsection_parentcatalogsectionid",
                table: "catalogsection",
                column: "parentcatalogsectionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attributevalues");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "baseproduct");

            migrationBuilder.DropTable(
                name: "catalogsection");
        }
    }
}

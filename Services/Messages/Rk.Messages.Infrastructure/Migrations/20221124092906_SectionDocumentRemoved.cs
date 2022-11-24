using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    public partial class SectionDocumentRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sectiondocuments");

            migrationBuilder.DropColumn(
                name: "sectiondocumentid",
                table: "catalogsections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sectiondocumentid",
                table: "catalogsections",
                type: "bigint",
                nullable: true);

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
                        name: "fk_sectiondocuments_catalogsections_sectionid",
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

            migrationBuilder.CreateIndex(
                name: "ix_sectiondocuments_catalogsectionid",
                table: "sectiondocuments",
                column: "catalogsectionid");

            migrationBuilder.CreateIndex(
                name: "ix_sectiondocuments_documentid",
                table: "sectiondocuments",
                column: "documentid");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rk.Messages.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    documentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    lastmodifiedby = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_news", x => x.id);
                });

            //migrationBuilder.CreateIndex(
            //    name: "ix_organizations_ogrn",
            //    table: "organizations",
            //    column: "ogrn",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "news");

            //migrationBuilder.DropIndex(
            //    name: "ix_organizations_ogrn",
            //    table: "organizations");
        }
    }
}

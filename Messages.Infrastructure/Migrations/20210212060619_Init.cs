using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Messages.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Description", "IsRead", "Name", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { 1, "Description", false, "Name", 2, 1 },
                    { 2, "Description", false, "Name", 1, 2 },
                    { 3, "Description", false, "Name", 3, 1 },
                    { 4, "Description", false, "Name", 1, 3 },
                    { 5, "Description", false, "Name", 3, 2 },
                    { 6, "Description", false, "Name", 2, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}

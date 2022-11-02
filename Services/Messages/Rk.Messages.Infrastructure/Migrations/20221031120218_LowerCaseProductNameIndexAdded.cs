using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    public partial class LowerCaseProductNameIndexAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("CREATE INDEX \"IX_Products_NameColumn_lower\" ON \"Products\" ((lower(\"Name\")));");
            migrationBuilder.Sql("CREATE INDEX ix_baseproduct_name_lower ON baseproduct (lower(name));");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX ix_baseproduct_name_lower;");
        }
    }
}

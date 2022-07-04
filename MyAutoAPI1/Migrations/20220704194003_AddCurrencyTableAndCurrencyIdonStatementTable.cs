using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAutoAPI1.Migrations
{
    public partial class AddCurrencyTableAndCurrencyIdonStatementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Statement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Statement");
        }
    }
}

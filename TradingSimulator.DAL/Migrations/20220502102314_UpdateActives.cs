using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingSimulator.DAL.Migrations
{
    public partial class UpdateActives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastAsk",
                table: "Actives",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LastBid",
                table: "Actives",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAsk",
                table: "Actives");

            migrationBuilder.DropColumn(
                name: "LastBid",
                table: "Actives");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingSimulator.DAL.Migrations
{
    public partial class updateDeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StopLoss",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TakeProfit",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StopLoss",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "TakeProfit",
                table: "Deals");
        }
    }
}

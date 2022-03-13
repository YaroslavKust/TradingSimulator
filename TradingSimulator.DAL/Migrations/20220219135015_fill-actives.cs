using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingSimulator.DAL.Migrations
{
    public partial class fillactives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actives",
                columns: new[] { "Id", "Name", "Ticket", "Type" },
                values: new object[,]
                {
                    { 1, "Bitcoin", "BTC", 0 },
                    { 2, "Etherium", "ETH", 0 },
                    { 3, "Litecoin", "LTC", 0 },
                    { 4, "Tesla", "TSL", 3 },
                    { 5, "Meta Platform Inc.", "FB", 3 },
                    { 6, "Apple", "AAPL", 3 },
                    { 7, "Microsoft", "MSFT", 3 },
                    { 8, "S&P 500", "SPX", 2 },
                    { 9, "Индекс ММВБ", "MICEXINDEXCF", 2 },
                    { 10, "Index Dow Jones", "DJI", 2 },
                    { 11, "Gold", "XAU", 1 },
                    { 12, "Platinum", "XPT", 1 },
                    { 13, "Palladium", "XPD", 1 },
                    { 14, "Silver", "XAG", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}

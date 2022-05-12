using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingSimulator.DAL.Migrations
{
    public partial class appednDealTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "Deals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenTime",
                table: "Deals",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Deals");
        }
    }
}

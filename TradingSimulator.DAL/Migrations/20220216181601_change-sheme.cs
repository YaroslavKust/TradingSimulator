using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingSimulator.DAL.Migrations
{
    public partial class changesheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Operations_CloseOperationId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Operations_OpenOperationId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Actives_ActiveId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Users_UserId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_ActiveId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_UserId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Deals_CloseOperationId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ActiveId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CloseOperationId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Operations",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Deals",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OpenOperationId",
                table: "Deals",
                newName: "ActiveId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_OpenOperationId",
                table: "Deals",
                newName: "IX_Deals_ActiveId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Debt",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sum",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DealId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosePrice",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Count",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpenPrice",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_DealId",
                table: "Operations",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UserId",
                table: "Deals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Actives_ActiveId",
                table: "Deals",
                column: "ActiveId",
                principalTable: "Actives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Deals_DealId",
                table: "Operations",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Actives_ActiveId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Deals_DealId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_DealId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UserId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Debt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ClosePrice",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "OpenPrice",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Operations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Deals",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ActiveId",
                table: "Deals",
                newName: "OpenOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_ActiveId",
                table: "Deals",
                newName: "IX_Deals_OpenOperationId");

            migrationBuilder.AddColumn<int>(
                name: "ActiveId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Count",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CloseOperationId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Count] * [Price]",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ActiveId",
                table: "Operations",
                column: "ActiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_UserId",
                table: "Operations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CloseOperationId",
                table: "Deals",
                column: "CloseOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Operations_CloseOperationId",
                table: "Deals",
                column: "CloseOperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Operations_OpenOperationId",
                table: "Deals",
                column: "OpenOperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Actives_ActiveId",
                table: "Operations",
                column: "ActiveId",
                principalTable: "Actives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Users_UserId",
                table: "Operations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

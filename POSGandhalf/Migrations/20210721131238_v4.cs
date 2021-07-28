using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POSGandhalf.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "LastUpdate", "ProductId", "Quantity" },
                values: new object[] { 1, new DateTime(2021, 7, 21, 13, 12, 38, 115, DateTimeKind.Local).AddTicks(3090), 1, 10f });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "LastUpdate", "ProductId", "Quantity" },
                values: new object[] { 2, new DateTime(2021, 7, 21, 13, 12, 38, 138, DateTimeKind.Local).AddTicks(4750), 2, 20f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

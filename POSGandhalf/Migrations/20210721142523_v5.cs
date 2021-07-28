using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POSGandhalf.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2021, 7, 21, 14, 25, 23, 354, DateTimeKind.Local).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2021, 7, 21, 14, 25, 23, 376, DateTimeKind.Local).AddTicks(7860));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email", "FirstName", "LastName", "Phone", "UserLoginName", "UserPassword" },
                values: new object[] { "NoRoad", "admin@admin.com", "Admin", "Admin", "912345678", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "City", "Email", "FirstName", "LastName", "Phone", "PostalCode", "UserActive", "UserLastLogin", "UserLoginName", "UserPassword", "UserRole" },
                values: new object[] { 2, "Alameda de Belém", "Ponta Delgada", "clsoft.silvestre@gmail.com", "Celso", "Silvestre", "912152324", "9500-461", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "csilvestre", "1234", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2021, 7, 21, 13, 12, 38, 115, DateTimeKind.Local).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2021, 7, 21, 13, 12, 38, 138, DateTimeKind.Local).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email", "FirstName", "LastName", "Phone", "UserLoginName", "UserPassword" },
                values: new object[] { "Alameda de Belém", "clsoft.silvestre@gmail.com", "Celso", "Silvestre", "912152324", "csilvestre", "1234" });
        }
    }
}

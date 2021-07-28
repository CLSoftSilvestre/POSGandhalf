using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POSGandhalf.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "City", "Email", "FirstName", "LastName", "Phone", "PostalCode", "UserActive", "UserLastLogin", "UserLoginName", "UserPassword", "UserRole" },
                values: new object[] { 1, "Alameda de Belém", "Ponta Delgada", "clsoft.silvestre@gmail.com", "Celso", "Silvestre", "912152324", "9500-461", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "csilvestre", "1234", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

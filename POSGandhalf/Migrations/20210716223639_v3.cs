using Microsoft.EntityFrameworkCore.Migrations;

namespace POSGandhalf.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "DefaultTax", "Description", "SellingUnit" },
                values: new object[] { 1, 0f, "Mercearia", 4 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price", "ProductCategoryId" },
                values: new object[] { 1, "Laranja do algarve", "Laranjas", 1.35f, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price", "ProductCategoryId" },
                values: new object[] { 2, "Maça Golden", "Maça", 1.05f, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

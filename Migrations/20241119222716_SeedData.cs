using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Abbaye de Leffe" });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "GeneDrinks" });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Name", "Price" },
                values: new object[] { 1, 6.5999999999999996, 1, "Leffe Blonde", 2.20m });

            migrationBuilder.InsertData(
                table: "WholesalerStocks",
                columns: new[] { "Id", "BeerId", "Quantity", "WholesalerId" },
                values: new object[] { 1, 1, 10, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WholesalerStocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

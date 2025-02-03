using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class AddedmoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchMore",
                table: "stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWhenBought",
                table: "shopingCart",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyBuyInPrice",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchMore",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "DateWhenBought",
                table: "shopingCart");

            migrationBuilder.DropColumn(
                name: "CompanyBuyInPrice",
                table: "products");
        }
    }
}

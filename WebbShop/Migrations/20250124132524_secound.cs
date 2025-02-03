using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class secound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_shops",
                table: "shops");

            migrationBuilder.RenameTable(
                name: "shops",
                newName: "shopingCart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shopingCart",
                table: "shopingCart",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_shopingCart",
                table: "shopingCart");

            migrationBuilder.RenameTable(
                name: "shopingCart",
                newName: "shops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shops",
                table: "shops",
                column: "Id");
        }
    }
}

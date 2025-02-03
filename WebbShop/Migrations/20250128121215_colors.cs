using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class colors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "colors",
                table: "shopingCart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "colors",
                table: "shopingCart");
        }
    }
}

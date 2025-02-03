using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class canBebought : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanBeBought",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBeBought",
                table: "products");
        }
    }
}

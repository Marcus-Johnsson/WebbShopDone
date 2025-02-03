using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class frakt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Frakt",
                table: "shopingCart",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frakt",
                table: "shopingCart");
        }
    }
}

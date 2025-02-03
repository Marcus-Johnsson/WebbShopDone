using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShop.Migrations
{
    /// <inheritdoc />
    public partial class colorfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "colors",
                table: "shopingCart",
                newName: "color");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "shopingCart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "color",
                table: "shopingCart",
                newName: "colors");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "shopingCart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

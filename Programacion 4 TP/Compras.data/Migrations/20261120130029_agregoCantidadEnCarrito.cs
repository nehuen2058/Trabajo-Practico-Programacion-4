using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compras.data.Migrations
{
    /// <inheritdoc />
    public partial class agregoCantidadEnCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Carritos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Carritos");
        }
    }
}

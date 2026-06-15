using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISO_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayOrderToProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ProductDetailSubItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ProductDetailSubItems");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ProductDetails");
        }
    }
}

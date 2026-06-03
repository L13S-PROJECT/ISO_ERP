using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISO_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddProductRelationToProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            
            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Products_ProductId",
                table: "Productions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Products_ProductId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_ProductId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Productions");
        }
    }
}

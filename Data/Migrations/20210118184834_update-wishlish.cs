using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class updatewishlish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WishLists_WishListID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WishListID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WishListID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "WishLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductID",
                table: "WishLists",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductID",
                table: "WishLists",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductID",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_ProductID",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "WishLists");

            migrationBuilder.AddColumn<int>(
                name: "WishListID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WishListID",
                table: "Products",
                column: "WishListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WishLists_WishListID",
                table: "Products",
                column: "WishListID",
                principalTable: "WishLists",
                principalColumn: "WishListID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

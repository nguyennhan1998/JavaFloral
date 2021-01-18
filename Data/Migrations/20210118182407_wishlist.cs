using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishListID",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    WishListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.WishListID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WishLists_WishListID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Products_WishListID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WishListID",
                table: "Products");
        }
    }
}

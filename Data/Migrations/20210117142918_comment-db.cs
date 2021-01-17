using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class commentdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "CommentProduct",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentProduct", x => new { x.CommentID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_CommentProduct_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentProduct_ProductID",
                table: "CommentProduct",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentProduct");

            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}

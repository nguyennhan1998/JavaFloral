using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaFloral.Data.Migrations
{
    public partial class productupdatecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentProduct_Comment_CommentID",
                table: "CommentProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentProduct_Products_ProductID",
                table: "CommentProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentProduct",
                table: "CommentProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "CommentProduct",
                newName: "CommentProducts");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_CommentProduct_ProductID",
                table: "CommentProducts",
                newName: "IX_CommentProducts_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentProducts",
                table: "CommentProducts",
                columns: new[] { "CommentID", "ProductID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProducts_Comments_CommentID",
                table: "CommentProducts",
                column: "CommentID",
                principalTable: "Comments",
                principalColumn: "CommentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProducts_Products_ProductID",
                table: "CommentProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentProducts_Comments_CommentID",
                table: "CommentProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentProducts_Products_ProductID",
                table: "CommentProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentProducts",
                table: "CommentProducts");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "CommentProducts",
                newName: "CommentProduct");

            migrationBuilder.RenameIndex(
                name: "IX_CommentProducts_ProductID",
                table: "CommentProduct",
                newName: "IX_CommentProduct_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "CommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentProduct",
                table: "CommentProduct",
                columns: new[] { "CommentID", "ProductID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProduct_Comment_CommentID",
                table: "CommentProduct",
                column: "CommentID",
                principalTable: "Comment",
                principalColumn: "CommentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentProduct_Products_ProductID",
                table: "CommentProduct",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

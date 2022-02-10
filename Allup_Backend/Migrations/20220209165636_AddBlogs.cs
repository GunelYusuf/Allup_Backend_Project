using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup_Backend.Migrations
{
    public partial class AddBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ProductId",
                table: "Blogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Products_ProductId",
                table: "Blogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Products_ProductId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ProductId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Blogs");
        }
    }
}

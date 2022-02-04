using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup_Backend.Migrations
{
    public partial class ServicesAddNewProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServicesSliders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ServicesSliders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServicesSliders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ServicesSliders");
        }
    }
}

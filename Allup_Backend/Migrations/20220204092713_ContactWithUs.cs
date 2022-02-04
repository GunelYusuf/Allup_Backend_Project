using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup_Backend.Migrations
{
    public partial class ContactWithUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapUrl",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenCloseClocks",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapUrl",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OpenCloseClocks",
                table: "Contacts");
        }
    }
}

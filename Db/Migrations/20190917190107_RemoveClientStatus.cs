using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class RemoveClientStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Clients",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Clients",
                newName: "MiddleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Clients",
                newName: "Patronymic");
        }
    }
}

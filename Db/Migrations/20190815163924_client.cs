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

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "Clients",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Clients",
                newName: "Patronymic");
        }
    }
}

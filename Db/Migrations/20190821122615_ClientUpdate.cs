using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class ClientUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "PassportData");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Clients",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Clients",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "PassportData",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Clients",
                newName: "MiddleName");
        }
    }
}

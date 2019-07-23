using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class Clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Name");
        }
    }
}

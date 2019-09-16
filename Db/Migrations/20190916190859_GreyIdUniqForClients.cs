using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class GreyIdUniqForClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients",
                column: "GreyAddressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients",
                column: "GreyAddressId");
        }
    }
}

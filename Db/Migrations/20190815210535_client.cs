using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_GreyAddresses_ClientId",
                table: "GreyAddresses");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Clients",
                newName: "MiddleName");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "Clients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "GreyAddressId",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreyAddresses_ClientId",
                table: "GreyAddresses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients",
                column: "GreyAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_GreyAddresses_GreyAddressId",
                table: "Clients",
                column: "GreyAddressId",
                principalTable: "GreyAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_GreyAddresses_GreyAddressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_GreyAddresses_ClientId",
                table: "GreyAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Clients_GreyAddressId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "GreyAddressId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Clients",
                newName: "Patronymic");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreyAddresses_ClientId",
                table: "GreyAddresses",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

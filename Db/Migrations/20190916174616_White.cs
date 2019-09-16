using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class White : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "GrayAddressId",
                table: "WhiteAddresses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses",
                column: "GrayAddressId",
                principalTable: "GreyAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "GrayAddressId",
                table: "WhiteAddresses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses",
                column: "GrayAddressId",
                principalTable: "GreyAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

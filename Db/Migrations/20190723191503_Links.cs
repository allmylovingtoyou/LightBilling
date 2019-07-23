using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class Links : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

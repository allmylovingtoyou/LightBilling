using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class client1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Subnets_SubnetId",
                table: "Houses");

            migrationBuilder.AlterColumn<int>(
                name: "SubnetId",
                table: "Houses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Subnets_SubnetId",
                table: "Houses",
                column: "SubnetId",
                principalTable: "Subnets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Subnets_SubnetId",
                table: "Houses");

            migrationBuilder.AlterColumn<int>(
                name: "SubnetId",
                table: "Houses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Subnets_SubnetId",
                table: "Houses",
                column: "SubnetId",
                principalTable: "Subnets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

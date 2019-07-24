using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class Houses3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalNumber",
                table: "Houses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalNumber",
                table: "Houses");
        }
    }
}

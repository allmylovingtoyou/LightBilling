using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Db.Migrations
{
    public partial class Houses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Houses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Houses",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Clients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HwIpAddress",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HwPort",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HwIpAddress",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HwPort",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}

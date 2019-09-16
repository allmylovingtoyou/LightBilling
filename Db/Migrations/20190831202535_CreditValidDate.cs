using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Migrations
{
    public partial class CreditValidDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreditValidFrom",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreditValidTo",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreditValidFrom",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreditValidTo",
                table: "Clients");
        }
    }
}

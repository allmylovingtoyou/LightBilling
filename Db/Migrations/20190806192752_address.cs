using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Db.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_FullAddress_FullAddressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "FullAddress");

            migrationBuilder.DropIndex(
                name: "IX_Clients_FullAddressId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteAddress",
                table: "WhiteAddress");

            migrationBuilder.DropColumn(
                name: "FullAddressId",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "WhiteAddress",
                newName: "WhiteAddresses");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Subnets",
                newName: "Net");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GrayAddressId",
                table: "WhiteAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteAddresses",
                table: "WhiteAddresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GreyAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true),
                    SubnetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreyAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreyAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GreyAddresses_Subnets_SubnetId",
                        column: x => x.SubnetId,
                        principalTable: "Subnets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JoinClientsTariffs",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    TariffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinClientsTariffs", x => new { x.ClientId, x.TariffId });
                    table.ForeignKey(
                        name: "FK_JoinClientsTariffs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinClientsTariffs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhiteAddresses_GrayAddressId",
                table: "WhiteAddresses",
                column: "GrayAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreyAddresses_ClientId",
                table: "GreyAddresses",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreyAddresses_SubnetId",
                table: "GreyAddresses",
                column: "SubnetId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinClientsTariffs_TariffId",
                table: "JoinClientsTariffs",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses",
                column: "GrayAddressId",
                principalTable: "GreyAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Houses_HouseId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_WhiteAddresses_GreyAddresses_GrayAddressId",
                table: "WhiteAddresses");

            migrationBuilder.DropTable(
                name: "GreyAddresses");

            migrationBuilder.DropTable(
                name: "JoinClientsTariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteAddresses",
                table: "WhiteAddresses");

            migrationBuilder.DropIndex(
                name: "IX_WhiteAddresses_GrayAddressId",
                table: "WhiteAddresses");

            migrationBuilder.DropColumn(
                name: "GrayAddressId",
                table: "WhiteAddresses");

            migrationBuilder.RenameTable(
                name: "WhiteAddresses",
                newName: "WhiteAddress");

            migrationBuilder.RenameColumn(
                name: "Net",
                table: "Subnets",
                newName: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FullAddressId",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteAddress",
                table: "WhiteAddress",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FullAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Grey = table.Column<string>(nullable: true),
                    SubnetId = table.Column<int>(nullable: true),
                    WhiteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullAddress_Subnets_SubnetId",
                        column: x => x.SubnetId,
                        principalTable: "Subnets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FullAddress_WhiteAddress_WhiteId",
                        column: x => x.WhiteId,
                        principalTable: "WhiteAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FullAddressId",
                table: "Clients",
                column: "FullAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_FullAddress_SubnetId",
                table: "FullAddress",
                column: "SubnetId");

            migrationBuilder.CreateIndex(
                name: "IX_FullAddress_WhiteId",
                table: "FullAddress",
                column: "WhiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_FullAddress_FullAddressId",
                table: "Clients",
                column: "FullAddressId",
                principalTable: "FullAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

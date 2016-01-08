using System;
using Microsoft.Data.Entity.Migrations;

namespace RiotGames.Payments.Api.PaymentMethodApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Active = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    GatewayName = table.Column<string>(nullable: true),
                    InactivatedOn = table.Column<DateTime>(nullable: false),
                    PaymentInstrumentName = table.Column<string>(nullable: false),
                    PaymentMethodID = table.Column<string>(nullable: true),
                    PaymentMethodName = table.Column<string>(nullable: false),
                    PspName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("PaymentMethod");
        }
    }
}

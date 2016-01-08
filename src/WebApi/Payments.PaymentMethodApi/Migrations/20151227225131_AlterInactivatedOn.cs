using System;
using Microsoft.Data.Entity.Migrations;

namespace RiotGames.Payments.Api.PaymentMethodApi.Migrations
{
    public partial class AlterInactivatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InactivatedOn",
                table: "PaymentMethod",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InactivatedOn",
                table: "PaymentMethod",
                nullable: false);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Movements",
                newName: "EntryTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "Movements",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BikeHour",
                table: "Companys",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CarHour",
                table: "Companys",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BikeHour",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "CarHour",
                table: "Companys");

            migrationBuilder.RenameColumn(
                name: "EntryTime",
                table: "Movements",
                newName: "Date");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Vehicles_VehiclesId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_VehiclesId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "VehiclesId",
                table: "Movements");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_VehicleId",
                table: "Movements",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Vehicles_VehicleId",
                table: "Movements",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Vehicles_VehicleId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_VehicleId",
                table: "Movements");

            migrationBuilder.AddColumn<int>(
                name: "VehiclesId",
                table: "Movements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movements_VehiclesId",
                table: "Movements",
                column: "VehiclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Vehicles_VehiclesId",
                table: "Movements",
                column: "VehiclesId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}

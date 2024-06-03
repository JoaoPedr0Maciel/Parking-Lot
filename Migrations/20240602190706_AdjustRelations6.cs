using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelations6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Vehicles_VehicleId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_VehicleId",
                table: "Movements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

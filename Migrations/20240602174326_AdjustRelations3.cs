using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Companys_CompanyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Vehicle_Id",
                table: "Movements",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "Company_Id",
                table: "Movements",
                newName: "CompanyId");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Companys_CompanyId",
                table: "Vehicles",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Companys_CompanyId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Movements",
                newName: "Vehicle_Id");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Movements",
                newName: "Company_Id");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Companys_CompanyId",
                table: "Vehicles",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id");
        }
    }
}

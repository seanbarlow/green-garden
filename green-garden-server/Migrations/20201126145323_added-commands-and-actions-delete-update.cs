using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class addedcommandsandactionsdeleteupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Devices_DeviceId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Sensors_SensorId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_SensorId",
                table: "Commands");

            migrationBuilder.AlterColumn<int>(
                name: "Minutes",
                table: "Commands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SensorId",
                table: "Commands",
                column: "SensorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Devices_DeviceId",
                table: "Commands",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                principalTable: "Lookups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Sensors_SensorId",
                table: "Commands",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Devices_DeviceId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Sensors_SensorId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_SensorId",
                table: "Commands");

            migrationBuilder.AlterColumn<int>(
                name: "Minutes",
                table: "Commands",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SensorId",
                table: "Commands",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Devices_DeviceId",
                table: "Commands",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Sensors_SensorId",
                table: "Commands",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class addedcommandsandactionsdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEvent_Devices_DeviceId",
                table: "DeviceEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEvent_Devices_DeviceId",
                table: "DeviceEvent",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEvent_Devices_DeviceId",
                table: "DeviceEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEvent_Devices_DeviceId",
                table: "DeviceEvent",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

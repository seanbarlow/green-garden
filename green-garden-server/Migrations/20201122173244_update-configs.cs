using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class updateconfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_Devices_DeviceId",
                table: "Sensor");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_Lookups_SensorTypeId",
                table: "Sensor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensor",
                table: "Sensor");

            migrationBuilder.RenameTable(
                name: "Sensor",
                newName: "Sensors");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_SensorTypeId",
                table: "Sensors",
                newName: "IX_Sensors_SensorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_DeviceId",
                table: "Sensors",
                newName: "IX_Sensors_DeviceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Lookups",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Lookups",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Logs",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Logs",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Devices_DeviceId",
                table: "Sensors",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Lookups_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Devices_DeviceId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Lookups_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors");

            migrationBuilder.RenameTable(
                name: "Sensors",
                newName: "Sensor");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensor",
                newName: "IX_Sensor_SensorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_DeviceId",
                table: "Sensor",
                newName: "IX_Sensor_DeviceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Lookups",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Lookups",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensor",
                table: "Sensor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_Devices_DeviceId",
                table: "Sensor",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_Lookups_SensorTypeId",
                table: "Sensor",
                column: "SensorTypeId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

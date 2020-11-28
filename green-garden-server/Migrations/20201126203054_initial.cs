using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LookupTypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lookups_LookupTypes_LookupTypeId",
                        column: x => x.LookupTypeId,
                        principalTable: "LookupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    SensorTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Sent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commands_Lookups_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commands_Lookups_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    SensorTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_Lookups_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceId" },
                values: new object[] { 1, "green-garden-controller" });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "Id", "Description", "Name", "UniqueId" },
                values: new object[,]
                {
                    { 1, "The type of sensors we are monitoring and controlling", "Sensor Types", "sensortypes" },
                    { 2, "Event Type from devices", "Event Types", "eventtypes" },
                    { 3, "Action Type from devices", "Action Types", "actiontypes" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name", "UniqueId" },
                values: new object[,]
                {
                    { 100, "Pump", 1, "Pump", "pump" },
                    { 101, "Light", 1, "Light", "light" },
                    { 102, "pH Meter", 1, "pH Meter", "phmeter" },
                    { 103, "Fan", 1, "Fan", "fan" },
                    { 104, "Water Level", 1, "Water Level", "waterlevel" },
                    { 105, "Humidity", 1, "Humidity", "humidity" },
                    { 106, "Temperature", 1, "Temperature", "temperature" },
                    { 20, "The sensor sent an update", 2, "Update Event", "update" },
                    { 21, "A sensor setting has changed.", 2, "Change Event", "change" },
                    { 300, "On", 3, "On", "on" },
                    { 301, "Off", 3, "Off", "off" },
                    { 302, "Seconds the light is on for", 3, "Light on Seconds", "lightonseconds" },
                    { 303, "Seconds the light is off", 3, "Light of Seconds", "lightoffseconds" },
                    { 304, "Seconds the Pump is on", 3, "Pump on Seconds", "pumponseconds" },
                    { 305, "Seconds the pump is off", 3, "Pump off Seconds", "pumpoffseconds" }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "DeviceId", "SensorTypeId", "Status" },
                values: new object[] { 1, 1, 100, null });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "DeviceId", "SensorTypeId", "Status" },
                values: new object[] { 2, 1, 101, null });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_DeviceId",
                table: "Commands",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SensorTypeId",
                table: "Commands",
                column: "SensorTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_DeviceId",
                table: "Events",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_LookupTypeId",
                table: "Lookups",
                column: "LookupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupTypes_UniqueId",
                table: "LookupTypes",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_DeviceId",
                table: "Sensors",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropTable(
                name: "LookupTypes");
        }
    }
}

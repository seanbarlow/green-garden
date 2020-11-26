using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class addedcommandsandactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Sensors",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Sensors",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Sensors",
                type: "Date",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Sensors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LookupTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Lookups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Lookups_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    SensorTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Lookups_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Lookups_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Lookups_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "Id", "Description", "Name", "UniqueId" },
                values: new object[] { 5, "Event Type from devices", "Event Types", "eventtypes" });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "Id", "Description", "Name", "UniqueId" },
                values: new object[] { 6, "Action Type from devices", "Action Types", "actiontypes" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name" },
                values: new object[,]
                {
                    { 23, "The sensor is updating us with its status", 5, "update" },
                    { 24, "The sensor has changed its status", 5, "change" },
                    { 25, "On", 6, "on" },
                    { 26, "Off", 6, "off" },
                    { 27, "lightonseconds", 6, "lightonseconds" },
                    { 28, "lightoffseconds", 6, "lightoffseconds" },
                    { 29, "pumponseconds", 6, "pumponseconds" },
                    { 30, "pumpoffseconds", 6, "pumpoffseconds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_DeviceId",
                table: "Commands",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SensorId",
                table: "Commands",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_ActionTypeId",
                table: "DeviceEvent",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_DeviceId",
                table: "DeviceEvent",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_EventTypeId",
                table: "DeviceEvent",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_SensorTypeId",
                table: "DeviceEvent",
                column: "SensorTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "DeviceEvent");

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LookupTypes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Lookups");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Devices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Sensors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Sensors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Sensors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTypeId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Lookups_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logs_Lookups_MessageTypeId",
                        column: x => x.MessageTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "Id", "Description", "Name", "UniqueId" },
                values: new object[] { 1, "Amount of time used to ccontrol a device", "Frequencies", "frequencies" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name" },
                values: new object[,]
                {
                    { 18, "int", 4, "int" },
                    { 19, "bool", 4, "bool" },
                    { 20, "json", 4, "json" },
                    { 21, "float", 4, "float" },
                    { 22, "string", 4, "string" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Seconds", 1, "Seconds" },
                    { 2, "Minutes", 1, "Minutes" },
                    { 3, "Hours", 1, "Hours" },
                    { 4, "Days", 1, "Days" },
                    { 5, "Weeks", 1, "Weeks" },
                    { 6, "Months", 1, "Months" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_DataTypeId",
                table: "Logs",
                column: "DataTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_DeviceId",
                table: "Logs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_MessageTypeId",
                table: "Logs",
                column: "MessageTypeId",
                unique: true);
        }
    }
}

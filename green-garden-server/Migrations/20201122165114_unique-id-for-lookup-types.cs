using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class uniqueidforlookuptypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "LookupTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "UniqueId" },
                values: new object[] { "Frequencies", "frequencies" });

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "UniqueId" },
                values: new object[] { "Device Types", "devicetypes" });

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "UniqueId" },
                values: new object[] { "Message Types", "messagetypes" });

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "UniqueId" },
                values: new object[] { "Data Types", "datatypes" });

            migrationBuilder.CreateIndex(
                name: "IX_LookupTypes_UniqueId",
                table: "LookupTypes",
                column: "UniqueId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LookupTypes_UniqueId",
                table: "LookupTypes");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "LookupTypes");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Frequency");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Device Type");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Message Type");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Data Type");
        }
    }
}

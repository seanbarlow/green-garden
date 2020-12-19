using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class db_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name", "UniqueId" },
                values: new object[] { 306, "Humidity", 3, "Humidity", "humidity" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name", "UniqueId" },
                values: new object[] { 307, "Temperature", 3, "Temperature", "temperature" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Description", "LookupTypeId", "Name", "UniqueId" },
                values: new object[] { 308, "Heat Index", 3, "Heat Index", "heatindex" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 308);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace green_garden_server.Migrations
{
    public partial class removerelationshipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Lookups_SensorTypeId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_SensorTypeId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "ActionTypeId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "SensorTypeId",
                table: "Commands");

            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensorType",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "SensorType",
                table: "Commands");

            migrationBuilder.AddColumn<int>(
                name: "ActionTypeId",
                table: "Commands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SensorTypeId",
                table: "Commands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SensorTypeId",
                table: "Commands",
                column: "SensorTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Lookups_ActionTypeId",
                table: "Commands",
                column: "ActionTypeId",
                principalTable: "Lookups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Lookups_SensorTypeId",
                table: "Commands",
                column: "SensorTypeId",
                principalTable: "Lookups",
                principalColumn: "Id");
        }
    }
}

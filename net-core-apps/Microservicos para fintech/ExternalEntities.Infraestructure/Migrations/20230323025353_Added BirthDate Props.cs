using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedBirthDateProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExternalQuodData_BirthDate",
                table: "QuodDataRecords",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "ApplicationUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(1575), new DateTime(2023, 3, 22, 23, 53, 52, 128, DateTimeKind.Unspecified).AddTicks(7248) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5155), new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5064) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5179), new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5168) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5201), new DateTime(2023, 3, 22, 23, 53, 52, 137, DateTimeKind.Unspecified).AddTicks(5190) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalQuodData_BirthDate",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "ApplicationUser");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(3140), new DateTime(2023, 3, 19, 8, 39, 12, 485, DateTimeKind.Unspecified).AddTicks(2684) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4783), new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4732) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4824), new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4813) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4844), new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4834) });
        }
    }
}

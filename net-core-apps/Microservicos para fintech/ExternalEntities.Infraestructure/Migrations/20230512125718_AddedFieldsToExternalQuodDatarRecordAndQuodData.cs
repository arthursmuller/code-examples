using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedFieldsToExternalQuodDatarRecordAndQuodData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "QuodDataRecords",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExternalQuodData_Gender",
                table: "QuodDataRecords",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ExternalQuodData_Name",
                table: "QuodDataRecords",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "ExternalQuodData_PublicExposed",
                table: "QuodDataRecords",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(6458), new DateTime(2023, 5, 12, 9, 57, 18, 263, DateTimeKind.Unspecified).AddTicks(4515) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7680), new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7693), new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7687) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7705), new DateTime(2023, 5, 12, 9, 57, 18, 272, DateTimeKind.Unspecified).AddTicks(7699) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_Gender",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_Name",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_PublicExposed",
                table: "QuodDataRecords");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(4947), new DateTime(2023, 4, 8, 17, 35, 14, 630, DateTimeKind.Unspecified).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6555), new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6505) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6579), new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6568) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6599), new DateTime(2023, 4, 8, 17, 35, 14, 639, DateTimeKind.Unspecified).AddTicks(6590) });
        }
    }
}

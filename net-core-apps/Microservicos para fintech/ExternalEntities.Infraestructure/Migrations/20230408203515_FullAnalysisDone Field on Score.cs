using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class FullAnalysisDoneFieldonScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FullAnalysisDone",
                table: "UserScore",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAnalysisDone",
                table: "UserScore");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(5699), new DateTime(2023, 4, 7, 20, 11, 31, 263, DateTimeKind.Unspecified).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8571), new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8595), new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8586) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8614), new DateTime(2023, 4, 7, 20, 11, 31, 270, DateTimeKind.Unspecified).AddTicks(8604) });
        }
    }
}

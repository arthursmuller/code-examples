using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedCratedDateToScoreHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdDate",
                table: "ScoreFilterHistory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(177), new DateTime(2023, 4, 5, 1, 3, 52, 361, DateTimeKind.Unspecified).AddTicks(2175) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1279), new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1251) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1292), new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1287) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1304), new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1299) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdDate",
                table: "ScoreFilterHistory");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(1568), new DateTime(2023, 4, 4, 19, 17, 14, 353, DateTimeKind.Unspecified).AddTicks(6089) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3062), new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3025) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3082), new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3073) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3102), new DateTime(2023, 4, 4, 19, 17, 14, 363, DateTimeKind.Unspecified).AddTicks(3092) });
        }
    }
}

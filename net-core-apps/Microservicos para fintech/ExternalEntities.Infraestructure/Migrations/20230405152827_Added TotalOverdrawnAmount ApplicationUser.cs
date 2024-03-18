using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedTotalOverdrawnAmountApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalOverdrawnAmount",
                table: "ApplicationUser",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 12, 28, 26, 761, DateTimeKind.Unspecified).AddTicks(8495), new DateTime(2023, 4, 5, 12, 28, 26, 752, DateTimeKind.Unspecified).AddTicks(1015) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(623), new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(568) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(648), new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(636) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(672), new DateTime(2023, 4, 5, 12, 28, 26, 762, DateTimeKind.Unspecified).AddTicks(660) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOverdrawnAmount",
                table: "ApplicationUser");

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
    }
}

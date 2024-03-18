using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedExternalCreditFieldOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalExternalCreditAvailable",
                table: "ApplicationUser",
                type: "decimal(65,30)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExternalCreditAvailable",
                table: "ApplicationUser");

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
    }
}

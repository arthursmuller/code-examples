using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedFinancialAnalysisFieldsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCurrentDebitAmount",
                table: "ApplicationUser",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCurrentDebitDelinquentAmount",
                table: "ApplicationUser",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDelinquencyAmount",
                table: "ApplicationUser",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalFinancialOperations",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

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
                columns: new[] { "Cpf", "CreatedDate", "UpdateDate" },
                values: new object[] { "84509848072", new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4824), new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4813) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4844), new DateTime(2023, 3, 19, 8, 39, 12, 490, DateTimeKind.Unspecified).AddTicks(4834) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCurrentDebitAmount",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "TotalCurrentDebitDelinquentAmount",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "TotalDelinquencyAmount",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "TotalFinancialOperations",
                table: "ApplicationUser");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(1320), new DateTime(2023, 2, 20, 16, 6, 36, 153, DateTimeKind.Unspecified).AddTicks(75) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3026), new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(2984) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cpf", "CreatedDate", "UpdateDate" },
                values: new object[] { "40755899008", new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3045), new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3036) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3062), new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3053) });
        }
    }
}

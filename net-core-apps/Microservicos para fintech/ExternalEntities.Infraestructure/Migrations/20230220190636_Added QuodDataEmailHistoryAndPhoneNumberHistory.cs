using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedQuodDataEmailHistoryAndPhoneNumberHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalQuodData_Email",
                table: "QuodDataRecords",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ExternalQuodData_MobilePhoneNumber",
                table: "QuodDataRecords",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ExternalQuodData_PhoneNumber",
                table: "QuodDataRecords",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataEmails",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastSeen = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataEmails", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataEmails_QuodDataRecords_ExternalQuodDataRecordApplica~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataMobilePhoneNumbers",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastSeen = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataMobilePhoneNumbers", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataMobilePhoneNumbers_QuodDataRecords_ExternalQuodDataR~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataPhoneNumbers",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastSeen = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataPhoneNumbers", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataPhoneNumbers_QuodDataRecords_ExternalQuodDataRecordA~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3045), new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3036) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3062), new DateTime(2023, 2, 20, 16, 6, 36, 156, DateTimeKind.Unspecified).AddTicks(3053) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuodDataEmails");

            migrationBuilder.DropTable(
                name: "QuodDataMobilePhoneNumbers");

            migrationBuilder.DropTable(
                name: "QuodDataPhoneNumbers");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_Email",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_MobilePhoneNumber",
                table: "QuodDataRecords");

            migrationBuilder.DropColumn(
                name: "ExternalQuodData_PhoneNumber",
                table: "QuodDataRecords");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(941), new DateTime(2023, 2, 19, 12, 34, 7, 75, DateTimeKind.Unspecified).AddTicks(8727) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2879), new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2825) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2905), new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2893) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2928), new DateTime(2023, 2, 19, 12, 34, 7, 88, DateTimeKind.Unspecified).AddTicks(2917) });
        }
    }
}

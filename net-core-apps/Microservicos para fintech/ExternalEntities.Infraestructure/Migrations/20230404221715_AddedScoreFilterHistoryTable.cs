using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedScoreFilterHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreFilterHistory",
                columns: table => new
                {
                    UserScoreId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreFilterHistory", x => new { x.UserScoreId, x.Id });
                    table.ForeignKey(
                        name: "FK_ScoreFilterHistory_UserScore_UserScoreId",
                        column: x => x.UserScoreId,
                        principalTable: "UserScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreFilterHistory");

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedPayiedAnalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cpf = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisRequest_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(386), new DateTime(2023, 6, 12, 20, 32, 32, 975, DateTimeKind.Unspecified).AddTicks(9675) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1481), new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1451) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1494), new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1488) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1506), new DateTime(2023, 6, 12, 20, 32, 32, 979, DateTimeKind.Unspecified).AddTicks(1500) });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisRequest_BusinessId",
                table: "AnalysisRequest",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisRequest");

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
    }
}

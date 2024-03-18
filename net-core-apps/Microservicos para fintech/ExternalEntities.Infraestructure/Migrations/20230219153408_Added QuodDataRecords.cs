using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExternalEntities.Infraestructure.Migrations
{
    public partial class AddedQuodDataRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuodDataRecords",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataRecords", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataRecords_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Complement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Neighborhood = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResidenceType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_UserAddress_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataAddresses",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Neighborhood = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataAddresses", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataAddresses_QuodDataRecords_ExternalQuodDataRecordAppl~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataCurrentAddress",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Neighborhood = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataCurrentAddress", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId });
                    table.ForeignKey(
                        name: "FK_QuodDataCurrentAddress_QuodDataRecords_ExternalQuodDataRecor~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataNegative",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    PendenciesControlCred = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataNegative", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId });
                    table.ForeignKey(
                        name: "FK_QuodDataNegative_QuodDataRecords_ExternalQuodDataRecordAppli~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataProtests",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Situacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorProtestadosTotal = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalProtestos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataProtests", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuodDataProtests_QuodDataRecords_ExternalQuodDataRecordAppli~",
                        columns: x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId },
                        principalTable: "QuodDataRecords",
                        principalColumns: new[] { "ApplicationUserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuodDataScore",
                columns: table => new
                {
                    ExternalQuodDataRecordApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ExternalQuodDataRecordId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentCommitmentScore = table.Column<int>(type: "int", nullable: true),
                    ProfileScore = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuodDataScore", x => new { x.ExternalQuodDataRecordApplicationUserId, x.ExternalQuodDataRecordId });
                    table.ForeignKey(
                        name: "FK_QuodDataScore_QuodDataRecords_ExternalQuodDataRecordApplicat~",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuodDataAddresses");

            migrationBuilder.DropTable(
                name: "QuodDataCurrentAddress");

            migrationBuilder.DropTable(
                name: "QuodDataNegative");

            migrationBuilder.DropTable(
                name: "QuodDataProtests");

            migrationBuilder.DropTable(
                name: "QuodDataScore");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "QuodDataRecords");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(5356), new DateTime(2023, 1, 2, 22, 27, 37, 248, DateTimeKind.Unspecified).AddTicks(6607) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6298), new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6271) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6310), new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6305) });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6393), new DateTime(2023, 1, 2, 22, 27, 37, 251, DateTimeKind.Unspecified).AddTicks(6386) });
        }
    }
}

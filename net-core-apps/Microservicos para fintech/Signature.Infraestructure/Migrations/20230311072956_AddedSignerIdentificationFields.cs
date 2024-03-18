using Microsoft.EntityFrameworkCore.Migrations;

namespace Signature.Infraestructure.Migrations
{
    public partial class AddedSignerIdentificationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentFileExtension",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserCellphone",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserIdentification",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentFileExtension",
                table: "SignatureInformation");

            migrationBuilder.DropColumn(
                name: "UserCellphone",
                table: "SignatureInformation");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "SignatureInformation");

            migrationBuilder.DropColumn(
                name: "UserIdentification",
                table: "SignatureInformation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SignatureInformation");
        }
    }
}

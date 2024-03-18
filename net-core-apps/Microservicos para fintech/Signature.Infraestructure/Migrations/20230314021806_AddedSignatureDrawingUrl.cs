using Microsoft.EntityFrameworkCore.Migrations;

namespace Signature.Infraestructure.Migrations
{
    public partial class AddedSignatureDrawingUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignatureDrawingUrl",
                table: "SignatureInformation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignatureDrawingUrl",
                table: "SignatureInformation");
        }
    }
}

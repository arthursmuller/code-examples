using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class ToUpperColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInsercao",
                table: "EMAIL_MENSAGEM",
                newName: "DATA_INSERCAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DATA_INSERCAO",
                table: "EMAIL_MENSAGEM",
                newName: "DataInsercao");
        }
    }
}

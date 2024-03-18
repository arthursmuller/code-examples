using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RemoveUsuarioAnexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANEXO_USUARIO_ID_USUARIO",
                table: "ANEXO");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO",
                table: "ANEXO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO",
                table: "ANEXO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ANEXO_USUARIO_ID_USUARIO",
                table: "ANEXO",
                column: "ID_USUARIO",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

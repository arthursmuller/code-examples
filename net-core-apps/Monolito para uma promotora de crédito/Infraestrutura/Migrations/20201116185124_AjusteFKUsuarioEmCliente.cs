using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AjusteFKUsuarioEmCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_USUARIO",
                table: "CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_USUARIO",
                table: "CLIENTE",
                column: "ID_USUARIO",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_USUARIO",
                table: "CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_USUARIO",
                table: "CLIENTE",
                column: "ID_USUARIO");
        }
    }
}

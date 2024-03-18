using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddClienteIdUsuarioMaiusculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_USUARIO_IdUsuario",
                table: "CLIENTE");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "CLIENTE",
                newName: "ID_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENTE_IdUsuario",
                table: "CLIENTE",
                newName: "IX_CLIENTE_ID_USUARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_USUARIO_ID_USUARIO",
                table: "CLIENTE",
                column: "ID_USUARIO",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_USUARIO_ID_USUARIO",
                table: "CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ID_USUARIO",
                table: "CLIENTE",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENTE_ID_USUARIO",
                table: "CLIENTE",
                newName: "IX_CLIENTE_IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_USUARIO_IdUsuario",
                table: "CLIENTE",
                column: "IdUsuario",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO");
        }
    }
}

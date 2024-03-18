using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class LojaDeUsuarioParaCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_LOJA_ID_LOJA",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "ID_LOJA",
                table: "USUARIO");

            migrationBuilder.AddColumn<int>(
                name: "ID_LOJA",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_LOJA",
                table: "CLIENTE",
                column: "ID_LOJA");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_LOJA_ID_LOJA",
                table: "CLIENTE",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_LOJA_ID_LOJA",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_LOJA",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_LOJA",
                table: "CLIENTE");

            migrationBuilder.AddColumn<int>(
                name: "ID_LOJA",
                table: "USUARIO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ID_LOJA",
                table: "USUARIO",
                column: "ID_LOJA");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_LOJA_ID_LOJA",
                table: "USUARIO",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID_LOJA");
        }
    }
}

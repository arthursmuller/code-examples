using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RemocaoDeTodosOsIndicesIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
              name: "IX_ANEXOS_IdUsuario",
              table: "ANEXOS");

            migrationBuilder.DropIndex(
              name: "IX_INTENCOES_OPERACAO_ID_LEAD",
              table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_INTENCOES_OPERACAO_ID_LOJA",
              table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_INTENCOES_OPERACAO_ID_SITUACAO",
              table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_INTENCOES_OPERACAO_ID_TIPO_OPERACAO",
              table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_INTENCOES_OPERACAO_ID_USUARIO",
              table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_LEADS_ID_CONVENIO",
              table: "LEADS");

            migrationBuilder.DropIndex(
              name: "IX_LEADS_ID_LOJA",
              table: "LEADS");

            migrationBuilder.DropIndex(
              name: "IX_PARAMETROS_OPERACAO_ID_CONVENIO",
              table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_PARAMETROS_OPERACAO_ID_TIPO_OPERACAO",
              table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropIndex(
              name: "IX_TELEFONES_LOJAS_ID_LOJA",
              table: "TELEFONES_LOJAS");

            migrationBuilder.DropIndex(
              name: "IX_USUARIOS_ID_LOJA",
              table: "USUARIOS");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

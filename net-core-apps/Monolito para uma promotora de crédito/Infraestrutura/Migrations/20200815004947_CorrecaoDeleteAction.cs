using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class CorrecaoDeleteAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO",
                column: "IdConvenio",
                principalTable: "CONVENIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                column: "IdTipoOperacao",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO",
                column: "IdConvenio",
                principalTable: "CONVENIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                column: "IdTipoOperacao",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

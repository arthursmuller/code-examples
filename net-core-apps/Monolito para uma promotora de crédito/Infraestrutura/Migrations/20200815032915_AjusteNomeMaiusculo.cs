using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AjusteNomeMaiusculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "TentativaRetencao",
                table: "PARAMETROS_OPERACAO",
                newName: "TENTATIVA_RETENCAO");

            migrationBuilder.RenameColumn(
                name: "TaxaPlano",
                table: "PARAMETROS_OPERACAO",
                newName: "TAXA_PLANO");

            migrationBuilder.RenameColumn(
                name: "QuantidadeParcelas",
                table: "PARAMETROS_OPERACAO",
                newName: "QUANTIDADE_PARCELAS");

            migrationBuilder.RenameColumn(
                name: "InstituicaoFinanceira",
                table: "PARAMETROS_OPERACAO",
                newName: "INSTITUICAO_FINANCEIRA");

            migrationBuilder.RenameColumn(
                name: "IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                newName: "ID_TIPO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "IdConvenio",
                table: "PARAMETROS_OPERACAO",
                newName: "ID_CONVENIO");

            migrationBuilder.RenameIndex(
                name: "IX_PARAMETROS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                newName: "IX_PARAMETROS_OPERACAO_ID_TIPO_OPERACAO");

            migrationBuilder.RenameIndex(
                name: "IX_PARAMETROS_OPERACAO_IdConvenio",
                table: "PARAMETROS_OPERACAO",
                newName: "IX_PARAMETROS_OPERACAO_ID_CONVENIO");

            migrationBuilder.AlterColumn<string>(
                name: "SIGLA",
                table: "PRODUTOS",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO",
                table: "PARAMETROS_OPERACAO",
                column: "ID_CONVENIO",
                principalTable: "CONVENIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "TENTATIVA_RETENCAO",
                table: "PARAMETROS_OPERACAO",
                newName: "TentativaRetencao");

            migrationBuilder.RenameColumn(
                name: "TAXA_PLANO",
                table: "PARAMETROS_OPERACAO",
                newName: "TaxaPlano");

            migrationBuilder.RenameColumn(
                name: "QUANTIDADE_PARCELAS",
                table: "PARAMETROS_OPERACAO",
                newName: "QuantidadeParcelas");

            migrationBuilder.RenameColumn(
                name: "INSTITUICAO_FINANCEIRA",
                table: "PARAMETROS_OPERACAO",
                newName: "InstituicaoFinanceira");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                newName: "IdTipoOperacao");

            migrationBuilder.RenameColumn(
                name: "ID_CONVENIO",
                table: "PARAMETROS_OPERACAO",
                newName: "IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_PARAMETROS_OPERACAO_ID_TIPO_OPERACAO",
                table: "PARAMETROS_OPERACAO",
                newName: "IX_PARAMETROS_OPERACAO_IdTipoOperacao");

            migrationBuilder.RenameIndex(
                name: "IX_PARAMETROS_OPERACAO_ID_CONVENIO",
                table: "PARAMETROS_OPERACAO",
                newName: "IX_PARAMETROS_OPERACAO_IdConvenio");

            migrationBuilder.AlterColumn<string>(
                name: "SIGLA",
                table: "PRODUTOS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddConsultaBeneficioInssClienteDominio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaBeneficiosInssCliente_ANEXO_AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaBeneficiosInssCliente_CLIENTE_ClienteID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsultaBeneficiosInssCliente",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropIndex(
                name: "IX_ConsultaBeneficiosInssCliente_AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropIndex(
                name: "IX_ConsultaBeneficiosInssCliente_ClienteID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropColumn(
                name: "AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "ConsultaBeneficiosInssCliente");

            migrationBuilder.RenameTable(
                name: "ConsultaBeneficiosInssCliente",
                newName: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "UsuarioAtualizacao",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "USUARIO_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "IdPaperlessDocumento",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "ID_PAPERLESS_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "ID_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "IdAnexoArquivoTermo",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "ID_ANEXO_ARQUIVO_TERMO");

            migrationBuilder.RenameColumn(
                name: "DataGeracaoArquivoTermo",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "DATA_GERACAO_ARQUIVO_TERMO");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "ChaveAutorizacao",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "CHAVE_AUTORIZACAO");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "ID_CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.AlterColumn<string>(
                name: "USUARIO_ATUALIZACAO",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CONSULTA_BENEFICIO_INSS_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                column: "ID_CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_ANEXO_ARQUIVO_TERMO",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                column: "ID_ANEXO_ARQUIVO_TERMO");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                column: "ID_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_CONSULTA_BENEFICIO_INSS_CLIENTE_ANEXO_ID_ANEXO_ARQUIVO_TERMO",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                column: "ID_ANEXO_ARQUIVO_TERMO",
                principalTable: "ANEXO",
                principalColumn: "ID_ANEXO");

            migrationBuilder.AddForeignKey(
                name: "FK_CONSULTA_BENEFICIO_INSS_CLIENTE_CLIENTE_ID_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                column: "ID_CLIENTE",
                principalTable: "CLIENTE",
                principalColumn: "ID_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONSULTA_BENEFICIO_INSS_CLIENTE_ANEXO_ID_ANEXO_ARQUIVO_TERMO",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CONSULTA_BENEFICIO_INSS_CLIENTE_CLIENTE_ID_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CONSULTA_BENEFICIO_INSS_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_ANEXO_ARQUIVO_TERMO",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_CLIENTE",
                table: "CONSULTA_BENEFICIO_INSS_CLIENTE");

            migrationBuilder.RenameTable(
                name: "CONSULTA_BENEFICIO_INSS_CLIENTE",
                newName: "ConsultaBeneficiosInssCliente");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ATUALIZACAO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "UsuarioAtualizacao");

            migrationBuilder.RenameColumn(
                name: "ID_PAPERLESS_DOCUMENTO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "IdPaperlessDocumento");

            migrationBuilder.RenameColumn(
                name: "ID_CLIENTE",
                table: "ConsultaBeneficiosInssCliente",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "ID_ANEXO_ARQUIVO_TERMO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "IdAnexoArquivoTermo");

            migrationBuilder.RenameColumn(
                name: "DATA_GERACAO_ARQUIVO_TERMO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "DataGeracaoArquivoTermo");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "DataAtualizacao");

            migrationBuilder.RenameColumn(
                name: "CHAVE_AUTORIZACAO",
                table: "ConsultaBeneficiosInssCliente",
                newName: "ChaveAutorizacao");

            migrationBuilder.RenameColumn(
                name: "ID_CONSULTA_BENEFICIO_INSS_CLIENTE",
                table: "ConsultaBeneficiosInssCliente",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioAtualizacao",
                table: "ConsultaBeneficiosInssCliente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "ConsultaBeneficiosInssCliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsultaBeneficiosInssCliente",
                table: "ConsultaBeneficiosInssCliente",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaBeneficiosInssCliente_AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente",
                column: "AnexoArquivoTermoID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaBeneficiosInssCliente_ClienteID",
                table: "ConsultaBeneficiosInssCliente",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaBeneficiosInssCliente_ANEXO_AnexoArquivoTermoID",
                table: "ConsultaBeneficiosInssCliente",
                column: "AnexoArquivoTermoID",
                principalTable: "ANEXO",
                principalColumn: "ID_ANEXO",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaBeneficiosInssCliente_CLIENTE_ClienteID",
                table: "ConsultaBeneficiosInssCliente",
                column: "ClienteID",
                principalTable: "CLIENTE",
                principalColumn: "ID_CLIENTE",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

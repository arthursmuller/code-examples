using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicionadoIdLojaNaLeadEUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "USUARIOS",
                newName: "SENHA");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "USUARIOS",
                newName: "NOME");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "USUARIOS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "LOJAS",
                newName: "NOME");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "LOJAS",
                newName: "LONGITUDE");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "LOJAS",
                newName: "LATITUDE");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "LOJAS",
                newName: "ESTADO");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "LOJAS",
                newName: "ENDERECO");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "LOJAS",
                newName: "CIDADE");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "LOJAS",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "LOJAS",
                newName: "BAIRRO");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "LEADS",
                newName: "TELEFONE");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "LEADS",
                newName: "LONGITUDE");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "LEADS",
                newName: "LATITUDE");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "LEADS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Convenio",
                table: "LEADS",
                newName: "CONVENIO");

            migrationBuilder.RenameColumn(
                name: "OrigemRequisicaoTermo",
                table: "LEADS",
                newName: "ORIGEM_REQUISICAO_TERMO");

            migrationBuilder.RenameColumn(
                name: "OrigemRequisicaoPalavraChave",
                table: "LEADS",
                newName: "ORIGEM_REQUISICAO_PALAVRA_CHAVE");

            migrationBuilder.RenameColumn(
                name: "OrigemRequisicaoMidia",
                table: "LEADS",
                newName: "ORIGEM_REQUISICAO_MIDIA");

            migrationBuilder.RenameColumn(
                name: "OrigemRequisicaoConteudo",
                table: "LEADS",
                newName: "ORIGEM_REQUISICAO_CONTEUDO");

            migrationBuilder.RenameColumn(
                name: "OrigemRequisicaoCampanha",
                table: "LEADS",
                newName: "ORIGEM_REQUISICAO_CAMPANHA");

            migrationBuilder.AddColumn<int>(
                name: "ID_LOJA",
                table: "USUARIOS",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_LOJA",
                table: "LEADS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_ID_LOJA",
                table: "USUARIOS",
                column: "ID_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_LEADS_ID_LOJA",
                table: "LEADS",
                column: "ID_LOJA");

            migrationBuilder.AddForeignKey(
                name: "FK_LEADS_LOJAS_ID_LOJA",
                table: "LEADS",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_LOJAS_ID_LOJA",
                table: "USUARIOS",
                column: "ID_LOJA",
                principalTable: "LOJAS",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LEADS_LOJAS_ID_LOJA",
                table: "LEADS");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_LOJAS_ID_LOJA",
                table: "USUARIOS");

            migrationBuilder.DropIndex(
                name: "IX_USUARIOS_ID_LOJA",
                table: "USUARIOS");

            migrationBuilder.DropIndex(
                name: "IX_LEADS_ID_LOJA",
                table: "LEADS");

            migrationBuilder.DropColumn(
                name: "ID_LOJA",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "ID_LOJA",
                table: "LEADS");

            migrationBuilder.RenameColumn(
                name: "SENHA",
                table: "USUARIOS",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "USUARIOS",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "USUARIOS",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "LOJAS",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "LONGITUDE",
                table: "LOJAS",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "LATITUDE",
                table: "LOJAS",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "ESTADO",
                table: "LOJAS",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "ENDERECO",
                table: "LOJAS",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "CIDADE",
                table: "LOJAS",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "LOJAS",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "BAIRRO",
                table: "LOJAS",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "TELEFONE",
                table: "LEADS",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "LONGITUDE",
                table: "LEADS",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "LATITUDE",
                table: "LEADS",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "LEADS",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CONVENIO",
                table: "LEADS",
                newName: "Convenio");

            migrationBuilder.RenameColumn(
                name: "ORIGEM_REQUISICAO_TERMO",
                table: "LEADS",
                newName: "OrigemRequisicaoTermo");

            migrationBuilder.RenameColumn(
                name: "ORIGEM_REQUISICAO_PALAVRA_CHAVE",
                table: "LEADS",
                newName: "OrigemRequisicaoPalavraChave");

            migrationBuilder.RenameColumn(
                name: "ORIGEM_REQUISICAO_MIDIA",
                table: "LEADS",
                newName: "OrigemRequisicaoMidia");

            migrationBuilder.RenameColumn(
                name: "ORIGEM_REQUISICAO_CONTEUDO",
                table: "LEADS",
                newName: "OrigemRequisicaoConteudo");

            migrationBuilder.RenameColumn(
                name: "ORIGEM_REQUISICAO_CAMPANHA",
                table: "LEADS",
                newName: "OrigemRequisicaoCampanha");
        }
    }
}

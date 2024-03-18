using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlterSolicitacaoAcessoDadosPessoais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_NASCIMENTO",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_NASCIMENTO",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                type: "datetime2",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}

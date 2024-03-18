using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class SolicitacaoImportacaoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_SOLICITACAO_IMPORTACAO_DADOS",
                table: "CLIENTE",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IMPORTACAO_DADOS_SOLICITADA",
                table: "CLIENTE",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_SOLICITACAO_IMPORTACAO_DADOS",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "IMPORTACAO_DADOS_SOLICITADA",
                table: "CLIENTE");
        }
    }
}

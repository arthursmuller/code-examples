using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddColumnsClienteAceiteImportacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_AUTORIZACAO_IMPORTACAO_DADOS",
                table: "CLIENTE",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IMPORTACAO_DADOS_AUTORIZADA",
                table: "CLIENTE",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_AUTORIZACAO_IMPORTACAO_DADOS",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "IMPORTACAO_DADOS_AUTORIZADA",
                table: "CLIENTE");
        }
    }
}

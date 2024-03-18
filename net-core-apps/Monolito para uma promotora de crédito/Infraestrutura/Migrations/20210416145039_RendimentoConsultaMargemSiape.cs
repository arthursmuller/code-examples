using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RendimentoConsultaMargemSiape : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_LIBERACAO_CONSULTA_MARGEM",
                table: "RENDIMENTO_CLIENTE_SIAPE",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_LIBERACAO_CONSULTA_MARGEM",
                table: "RENDIMENTO_CLIENTE_SIAPE");
        }
    }
}

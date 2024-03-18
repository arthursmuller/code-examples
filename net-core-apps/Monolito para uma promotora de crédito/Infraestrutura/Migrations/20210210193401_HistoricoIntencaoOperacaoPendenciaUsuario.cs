using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class HistoricoIntencaoOperacaoPendenciaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PENDENCIA_USUARIO",
                table: "INTENCAO_OPERACAO_HISTORICO",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PENDENCIA_USUARIO",
                table: "INTENCAO_OPERACAO_HISTORICO");
        }
    }
}

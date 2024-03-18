using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlterRelacionamentoIntencaoOperacaoPortabilidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_PortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "PortabilidadeID",
                table: "INTENCAO_OPERACAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "PortabilidadeID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "PortabilidadeID",
                principalTable: "INTENCAO_OPERACAO_PORTABILIDADE",
                principalColumn: "ID_INTENCAO_OPERACAO_PORTABILIDADE",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

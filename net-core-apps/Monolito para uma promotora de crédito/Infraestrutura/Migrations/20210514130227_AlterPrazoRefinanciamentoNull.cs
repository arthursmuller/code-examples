using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlterPrazoRefinanciamentoNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                newName: "PortabilidadeID");

            migrationBuilder.RenameIndex(
                name: "IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                newName: "IX_INTENCAO_OPERACAO_PortabilidadeID");

            migrationBuilder.AlterColumn<int>(
                name: "PRAZO_REFINANCIAMENTO",
                table: "INTENCAO_OPERACAO_PORTABILIDADE",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "PortabilidadeID",
                principalTable: "INTENCAO_OPERACAO_PORTABILIDADE",
                principalColumn: "ID_INTENCAO_OPERACAO_PORTABILIDADE",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                newName: "IntencaoOperacaoPortabilidadeID");

            migrationBuilder.RenameIndex(
                name: "IX_INTENCAO_OPERACAO_PortabilidadeID",
                table: "INTENCAO_OPERACAO",
                newName: "IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID");

            migrationBuilder.AlterColumn<int>(
                name: "PRAZO_REFINANCIAMENTO",
                table: "INTENCAO_OPERACAO_PORTABILIDADE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "IntencaoOperacaoPortabilidadeID",
                principalTable: "INTENCAO_OPERACAO_PORTABILIDADE",
                principalColumn: "ID_INTENCAO_OPERACAO_PORTABILIDADE",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

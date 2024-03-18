using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddsUKIntencaoOperacaoContrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_CONTRATO");

            migrationBuilder.AlterColumn<string>(
                name: "MATRICULA",
                table: "RENDIMENTO_CLIENTE",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO_CONTRATO",
                table: "INTENCAO_OPERACAO_CONTRATO",
                columns: new[] { "ID_INTENCAO_OPERACAO", "CONTRATO" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO_CONTRATO",
                table: "INTENCAO_OPERACAO_CONTRATO");

            migrationBuilder.AlterColumn<string>(
                name: "MATRICULA",
                table: "RENDIMENTO_CLIENTE",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_CONTRATO",
                column: "ID_INTENCAO_OPERACAO");
        }
    }
}

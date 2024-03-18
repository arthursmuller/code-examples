using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddColumnsIntencaoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CUSTO_EFETIVO_TOTAL_ANO",
                table: "INTENCAO_OPERACAO",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CUSTO_EFETIVO_TOTAL_MES",
                table: "INTENCAO_OPERACAO",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IMPOSTO_OPERACAO_FINANCEIRA",
                table: "INTENCAO_OPERACAO",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUSTO_EFETIVO_TOTAL_ANO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "CUSTO_EFETIVO_TOTAL_MES",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "IMPOSTO_OPERACAO_FINANCEIRA",
                table: "INTENCAO_OPERACAO");
        }
    }
}

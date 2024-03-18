using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddTipoOperacaoEmPassosSituacaoIntencaoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.AddColumn<int>(
                name: "ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "PROPOSTA",
                table: "INTENCAO_OPERACAO",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_TIPO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                columns: new[] { "ID_PRODUTO", "ID_TIPO_OPERACAO", "ID_SITUACAO_INTENCAO_OPERACAO" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                columns: new[] { "ID_PRODUTO", "PASSO_INICIAL", "ID_TIPO_OPERACAO" },
                unique: true,
                filter: "[PASSO_INICIAL] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_TIPO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_TIPO_OPERACAO",
                principalTable: "TIPO_OPERACAO",
                principalColumn: "ID_TIPO_OPERACAO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameTable(name: "PRODUTO_INTENCAO_OPERACAO_PASSO", schema: "dbo", newName: "INTENCAO_OPERACAO_SITUACAO_PASSO", newSchema: "dbo");
            migrationBuilder.RenameColumn(name: "ID_PRODUTO_INTENCAO_OPERACAO_PASSO", table: "INTENCAO_OPERACAO_SITUACAO_PASSO", newName: "ID_INTENCAO_OPERACAO_SITUACAO_PASSO", schema: "dbo");
            migrationBuilder.RenameColumn(name: "ID_SITUACAO_INTENCAO_OPERACAO", table: "INTENCAO_OPERACAO_SITUACAO_PASSO", newName: "ID_INTENCAO_OPERACAO_SITUACAO", schema: "dbo");

            migrationBuilder.RenameTable(name: "SITUACAO_INTENCAO_OPERACAO", schema: "dbo", newName: "INTENCAO_OPERACAO_SITUACAO", newSchema: "dbo");
            migrationBuilder.RenameColumn(name: "ID_SITUACAO_INTENCAO_OPERACAO", table: "INTENCAO_OPERACAO_SITUACAO", newName: "ID_INTENCAO_OPERACAO_SITUACAO", schema: "dbo");

            migrationBuilder.RenameColumn(name: "ID_SITUACAO_INTENCAO_OPERACAO", table: "INTENCAO_OPERACAO_HISTORICO", newName: "ID_INTENCAO_OPERACAO_SITUACAO", schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_TIPO_OPERACAO_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_TIPO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropColumn(
                name: "ID_TIPO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropColumn(
                name: "Proposta",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                columns: new[] { "ID_PRODUTO", "ID_SITUACAO_INTENCAO_OPERACAO" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                columns: new[] { "ID_PRODUTO", "PASSO_INICIAL" },
                unique: true,
                filter: "[PASSO_INICIAL] = 1");
        }
    }
}

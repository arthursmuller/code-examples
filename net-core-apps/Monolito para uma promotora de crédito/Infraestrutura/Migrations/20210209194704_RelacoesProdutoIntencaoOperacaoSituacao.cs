using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RelacoesProdutoIntencaoOperacaoSituacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "ID_SITUACAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.AddColumn<string>(
                name: "DESCRICAO_PADRAO",
                table: "SITUACAO_INTENCAO_OPERACAO",
                unicode: false,
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PERMITE_ATUALIZACOES",
                table: "SITUACAO_INTENCAO_OPERACAO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PERMITE_SITUACAO_EXTRAORDINARIA",
                table: "SITUACAO_INTENCAO_OPERACAO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SITUACAO_EXTRAORDINARIA",
                table: "SITUACAO_INTENCAO_OPERACAO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "REQUER_CONVENIO",
                table: "PRODUTO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ID_PRODUTO",
                table: "LEAD",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_PRODUTO",
                table: "INTENCAO_OPERACAO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "INTENCAO_OPERACAO_HISTORICO",
                columns: table => new
                {
                    ID_INTENCAO_OPERACAO_HISTORICO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ID_INTENCAO_OPERACAO = table.Column<int>(nullable: false),
                    ID_SITUACAO_INTENCAO_OPERACAO = table.Column<int>(nullable: false),
                    DESCRICAO_ESPECIFICA = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTENCAO_OPERACAO_HISTORICO", x => x.ID_INTENCAO_OPERACAO_HISTORICO);
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_HISTORICO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO",
                        column: x => x.ID_INTENCAO_OPERACAO,
                        principalTable: "INTENCAO_OPERACAO",
                        principalColumn: "ID_INTENCAO_OPERACAO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_HISTORICO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO",
                        column: x => x.ID_SITUACAO_INTENCAO_OPERACAO,
                        principalTable: "SITUACAO_INTENCAO_OPERACAO",
                        principalColumn: "ID_SITUACAO_INTENCAO_OPERACAO");
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                columns: table => new
                {
                    ID_PRODUTO_INTENCAO_OPERACAO_PASSO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ID_PRODUTO = table.Column<int>(nullable: false),
                    ID_SITUACAO_INTENCAO_OPERACAO = table.Column<int>(nullable: false),
                    ID_PROXIMO_PASSO = table.Column<int>(nullable: true),
                    ID_PROXIMO_PASSO_EXCECAO = table.Column<int>(nullable: true),
                    PASSO_INICIAL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_INTENCAO_OPERACAO_PASSO", x => x.ID_PRODUTO_INTENCAO_OPERACAO_PASSO);
                    table.ForeignKey(
                        name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO",
                        column: x => x.ID_PROXIMO_PASSO,
                        principalTable: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                        principalColumn: "ID_PRODUTO_INTENCAO_OPERACAO_PASSO");
                    table.ForeignKey(
                        name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO_EXCECAO",
                        column: x => x.ID_PROXIMO_PASSO_EXCECAO,
                        principalTable: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                        principalColumn: "ID_PRODUTO_INTENCAO_OPERACAO_PASSO");
                    table.ForeignKey(
                        name: "FK_PRODUTO_INTENCAO_OPERACAO_PASSO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO",
                        column: x => x.ID_SITUACAO_INTENCAO_OPERACAO,
                        principalTable: "SITUACAO_INTENCAO_OPERACAO",
                        principalColumn: "ID_SITUACAO_INTENCAO_OPERACAO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEAD_ID_PRODUTO",
                table: "LEAD",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_HISTORICO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_HISTORICO",
                column: "ID_INTENCAO_OPERACAO");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_HISTORICO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_HISTORICO",
                column: "ID_SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_PROXIMO_PASSO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO_EXCECAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_PROXIMO_PASSO_EXCECAO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_SITUACAO_INTENCAO_OPERACAO",
                table: "PRODUTO_INTENCAO_OPERACAO_PASSO",
                column: "ID_SITUACAO_INTENCAO_OPERACAO");

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

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO",
                column: "ID_PRODUTO",
                principalTable: "PRODUTO",
                principalColumn: "ID_PRODUTO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEAD_PRODUTO_ID_PRODUTO",
                table: "LEAD",
                column: "ID_PRODUTO",
                principalTable: "PRODUTO",
                principalColumn: "ID_PRODUTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_LEAD_PRODUTO_ID_PRODUTO",
                table: "LEAD");

            migrationBuilder.DropTable(
                name: "INTENCAO_OPERACAO_HISTORICO");

            migrationBuilder.DropTable(
                name: "PRODUTO_INTENCAO_OPERACAO_PASSO");

            migrationBuilder.DropIndex(
                name: "IX_LEAD_ID_PRODUTO",
                table: "LEAD");

            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_ID_PRODUTO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "DESCRICAO_PADRAO",
                table: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "PERMITE_ATUALIZACOES",
                table: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "PERMITE_SITUACAO_EXTRAORDINARIA",
                table: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "SITUACAO_EXTRAORDINARIA",
                table: "SITUACAO_INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "REQUER_CONVENIO",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "ID_PRODUTO",
                table: "LEAD");

            migrationBuilder.DropColumn(
                name: "ID_PRODUTO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.AddColumn<int>(
                name: "ID_SITUACAO",
                table: "INTENCAO_OPERACAO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO",
                table: "INTENCAO_OPERACAO",
                column: "ID_SITUACAO",
                principalTable: "SITUACAO_INTENCAO_OPERACAO",
                principalColumn: "ID_SITUACAO_INTENCAO_OPERACAO");
        }
    }
}

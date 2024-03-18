using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SITUACOES_INTENCAO_OPERACAO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITUACOES_INTENCAO_OPERACAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INTENCOES_OPERACAO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TIPO_OPERACAO = table.Column<int>(nullable: false),
                    ID_SITUACAO = table.Column<int>(nullable: false),
                    ID_LOJA = table.Column<int>(nullable: true),
                    PRESTACAO = table.Column<decimal>(nullable: false),
                    VALOR_AUXILIO_FINANCEIRO = table.Column<decimal>(nullable: false),
                    TAXA_MES = table.Column<decimal>(nullable: false),
                    TAXA_ANO = table.Column<decimal>(nullable: false),
                    VALOR_FINANCIADO = table.Column<decimal>(nullable: false),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTENCOES_OPERACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INTENCOES_OPERACAO_LOJAS_ID_LOJA",
                        column: x => x.ID_LOJA,
                        principalTable: "LOJAS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_INTENCOES_OPERACAO_SITUACOES_INTENCAO_OPERACAO_ID_SITUACAO",
                        column: x => x.ID_SITUACAO,
                        principalTable: "SITUACOES_INTENCAO_OPERACAO",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_INTENCOES_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO",
                        column: x => x.ID_TIPO_OPERACAO,
                        principalTable: "TIPOS_OPERACAO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTENCOES_OPERACAO_ID_LOJA",
                table: "INTENCOES_OPERACAO",
                column: "ID_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCOES_OPERACAO_ID_SITUACAO",
                table: "INTENCOES_OPERACAO",
                column: "ID_SITUACAO");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCOES_OPERACAO_ID_TIPO_OPERACAO",
                table: "INTENCOES_OPERACAO",
                column: "ID_TIPO_OPERACAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INTENCOES_OPERACAO");

            migrationBuilder.DropTable(
                name: "SITUACOES_INTENCAO_OPERACAO");
        }
    }
}

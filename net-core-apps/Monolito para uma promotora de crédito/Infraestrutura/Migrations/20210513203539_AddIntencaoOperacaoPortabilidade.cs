using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddIntencaoOperacaoPortabilidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PERMITE_PORTABILIDADE",
                table: "BANCO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "INTENCAO_OPERACAO_PORTABILIDADE",
                columns: table => new
                {
                    ID_INTENCAO_OPERACAO_PORTABILIDADE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_INTENCAO_OPERACAO = table.Column<int>(type: "int", nullable: false),
                    ID_BANCO_ORIGINADOR = table.Column<int>(type: "int", nullable: false),
                    PRAZO_RESTANTE = table.Column<int>(type: "int", nullable: false),
                    PRAZO_TOTAL = table.Column<int>(type: "int", nullable: false),
                    SALDO_DEVEDOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PLANO_REFINANCIAMENTO = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    PRAZO_REFINANCIAMENTO = table.Column<int>(type: "int", nullable: false),
                    VALOR_PRESTACAO_REFINANCIAMENTO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTENCAO_OPERACAO_PORTABILIDADE", x => x.ID_INTENCAO_OPERACAO_PORTABILIDADE);
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_PORTABILIDADE_BANCO_ID_BANCO_ORIGINADOR",
                        column: x => x.ID_BANCO_ORIGINADOR,
                        principalTable: "BANCO",
                        principalColumn: "ID_BANCO");
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_PORTABILIDADE_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO",
                        column: x => x.ID_INTENCAO_OPERACAO,
                        principalTable: "INTENCAO_OPERACAO",
                        principalColumn: "ID_INTENCAO_OPERACAO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "IntencaoOperacaoPortabilidadeID");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_PORTABILIDADE_ID_BANCO_ORIGINADOR",
                table: "INTENCAO_OPERACAO_PORTABILIDADE",
                column: "ID_BANCO_ORIGINADOR");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_PORTABILIDADE_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_PORTABILIDADE",
                column: "ID_INTENCAO_OPERACAO",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO",
                column: "IntencaoOperacaoPortabilidadeID",
                principalTable: "INTENCAO_OPERACAO_PORTABILIDADE",
                principalColumn: "ID_INTENCAO_OPERACAO_PORTABILIDADE",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropTable(
                name: "INTENCAO_OPERACAO_PORTABILIDADE");

            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "IntencaoOperacaoPortabilidadeID",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "PERMITE_PORTABILIDADE",
                table: "BANCO");
        }
    }
}

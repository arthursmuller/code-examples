using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddsIntencaoOperacaoContrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "INTENCAO_OPERACAO_CONTRATO",
                columns: table => new
                {
                    ID_INTENCAO_OPERACAO_CONTRATO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CONTRATO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ID_INTENCAO_OPERACAO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTENCAO_OPERACAO_CONTRATO", x => x.ID_INTENCAO_OPERACAO_CONTRATO);
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_CONTRATO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO",
                        column: x => x.ID_INTENCAO_OPERACAO,
                        principalTable: "INTENCAO_OPERACAO",
                        principalColumn: "ID_INTENCAO_OPERACAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO",
                column: "ID_RENDIMENTO_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_CONTRATO",
                column: "ID_INTENCAO_OPERACAO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCAO_OPERACAO_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO",
                column: "ID_RENDIMENTO_CLIENTE",
                principalTable: "RENDIMENTO_CLIENTE",
                principalColumn: "ID_RENDIMENTO_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCAO_OPERACAO_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropTable(
                name: "INTENCAO_OPERACAO_CONTRATO");

            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "ID_RENDIMENTO_CLIENTE",
                table: "INTENCAO_OPERACAO");
        }
    }
}

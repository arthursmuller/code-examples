using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddObservacoesIntencaoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INTENCAO_OPERACAO_OBSERVACAO",
                columns: table => new
                {
                    ID_INTENCAO_OPERACAO_OBSERVACAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_INTENCAO_OPERACAO = table.Column<int>(type: "int", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    DATA_INCLUSAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTENCAO_OPERACAO_OBSERVACAO", x => x.ID_INTENCAO_OPERACAO_OBSERVACAO);
                    table.ForeignKey(
                        name: "FK_INTENCAO_OPERACAO_OBSERVACAO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO",
                        column: x => x.ID_INTENCAO_OPERACAO,
                        principalTable: "INTENCAO_OPERACAO",
                        principalColumn: "ID_INTENCAO_OPERACAO",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INTENCAO_OPERACAO_OBSERVACAO");
        }
    }
}

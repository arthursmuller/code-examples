using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class SolicitacaoDocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SOLICITACAO_DOCUMENTO",
                columns: table => new
                {
                    ID_SOLICITACAO_DOCUMENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TIPO_DOCUMENTO = table.Column<int>(type: "int", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    DATA_SOLICITACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOLICITACAO_DOCUMENTO", x => x.ID_SOLICITACAO_DOCUMENTO);
                    table.ForeignKey(
                        name: "FK_SOLICITACAO_DOCUMENTO_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOLICITACAO_DOCUMENTO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                        column: x => x.ID_TIPO_DOCUMENTO,
                        principalTable: "TIPO_DOCUMENTO",
                        principalColumn: "ID_TIPO_DOCUMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SOLICITACAO_DOCUMENTO_ID_CLIENTE",
                table: "SOLICITACAO_DOCUMENTO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_SOLICITACAO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                table: "SOLICITACAO_DOCUMENTO",
                column: "ID_TIPO_DOCUMENTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SOLICITACAO_DOCUMENTO");
        }
    }
}

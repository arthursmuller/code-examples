using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddDocumentoIdentificacaoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                columns: table => new
                {
                    ID_DOCUMENTO_IDENTIFICACAO_CLIENTE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ID_CLIENTE = table.Column<int>(nullable: false),
                    ID_TIPO_DOCUMENTO = table.Column<int>(nullable: false),
                    ID_ORGAO_EMISSOR = table.Column<int>(nullable: false),
                    ID_UNIDADE_FEDERATIVA = table.Column<int>(nullable: false),
                    NUMERO = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    DATA_EMISSAO = table.Column<DateTime>(nullable: false),
                    DELETADO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTO_IDENTIFICACAO_CLIENTE", x => x.ID_DOCUMENTO_IDENTIFICACAO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                    table.ForeignKey(
                        name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_ORGAO_EMISSOR_IDENTIFICACAO_ID_ORGAO_EMISSOR",
                        column: x => x.ID_ORGAO_EMISSOR,
                        principalTable: "ORGAO_EMISSOR_IDENTIFICACAO",
                        principalColumn: "ID_ORGAO_EMISSOR_IDENTIFICACAO");
                    table.ForeignKey(
                        name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO",
                        column: x => x.ID_TIPO_DOCUMENTO,
                        principalTable: "TIPO_DOCUMENTO",
                        principalColumn: "ID_TIPO_DOCUMENTO");
                    table.ForeignKey(
                        name: "FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA",
                        column: x => x.ID_UNIDADE_FEDERATIVA,
                        principalTable: "UNIDADE_FEDERATIVA",
                        principalColumn: "ID_UNIDADE_FEDERATIVA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_CLIENTE",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_ORGAO_EMISSOR",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                column: "ID_ORGAO_EMISSOR");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_TIPO_DOCUMENTO",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                column: "ID_TIPO_DOCUMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_UNIDADE_FEDERATIVA",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                column: "ID_UNIDADE_FEDERATIVA");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_NUMERO_ID_TIPO_DOCUMENTO_ID_UNIDADE_FEDERATIVA",
                table: "DOCUMENTO_IDENTIFICACAO_CLIENTE",
                columns: new[] { "NUMERO", "ID_TIPO_DOCUMENTO", "ID_UNIDADE_FEDERATIVA" },
                unique: true,
                filter: "[NUMERO] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOCUMENTO_IDENTIFICACAO_CLIENTE");
        }
    }
}

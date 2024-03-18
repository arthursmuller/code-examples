using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class NovoRendimentosHeranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RENDIMENTO_CLIENTE",
                columns: table => new
                {
                    ID_RENDIMENTO_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_CONVENIO = table.Column<int>(type: "int", nullable: false),
                    ID_CONVENIO_ORGAO = table.Column<int>(type: "int", nullable: false),
                    ID_UF = table.Column<int>(type: "int", nullable: false),
                    ID_BANCO = table.Column<int>(type: "int", nullable: false),
                    ID_TIPO_CONTA = table.Column<int>(type: "int", nullable: false),
                    AGENCIA = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    CONTA = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    VALOR_RENDIMENTO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MATRICULA = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DELETADO = table.Column<bool>(type: "bit", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENDIMENTO_CLIENTE", x => x.ID_RENDIMENTO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO",
                        column: x => x.ID_BANCO,
                        principalTable: "BANCO",
                        principalColumn: "ID_BANCO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO",
                        column: x => x.ID_CONVENIO,
                        principalTable: "CONVENIO",
                        principalColumn: "ID_CONVENIO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_CONVENIO_ORGAO_ID_CONVENIO_ORGAO",
                        column: x => x.ID_CONVENIO_ORGAO,
                        principalTable: "CONVENIO_ORGAO",
                        principalColumn: "ID_CONVENIO_ORGAO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA",
                        column: x => x.ID_TIPO_CONTA,
                        principalTable: "TIPO_CONTA",
                        principalColumn: "ID_TIPO_CONTA");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_UNIDADE_FEDERATIVA_ID_UF",
                        column: x => x.ID_UF,
                        principalTable: "UNIDADE_FEDERATIVA",
                        principalColumn: "ID_UNIDADE_FEDERATIVA");
                });

            migrationBuilder.CreateTable(
                name: "RENDIMENTO_CLIENTE_INSS",
                columns: table => new
                {
                    ID_RENDIMENTO_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_INSS_ESPECIE_BENEFICIO = table.Column<int>(type: "int", nullable: false),
                    DATA_INSCRICAO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENDIMENTO_CLIENTE_INSS", x => x.ID_RENDIMENTO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_INSS_INSS_ESPECIE_BENEFICIO_ID_INSS_ESPECIE_BENEFICIO",
                        column: x => x.ID_INSS_ESPECIE_BENEFICIO,
                        principalTable: "INSS_ESPECIE_BENEFICIO",
                        principalColumn: "ID_INSS_ESPECIE_BENEFICIO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_INSS_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                        column: x => x.ID_RENDIMENTO_CLIENTE,
                        principalTable: "RENDIMENTO_CLIENTE",
                        principalColumn: "ID_RENDIMENTO_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RENDIMENTO_CLIENTE_SIAPE",
                columns: table => new
                {
                    ID_RENDIMENTO_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_SIAPE_TIPO_FUNCIONAL = table.Column<int>(type: "int", nullable: false),
                    MATRICULA_INSTITUIDOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NOME_INSTITUIDOR = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    POSSUI_REPRESENTACAO_POR_PROCURADOR = table.Column<bool>(type: "bit", nullable: false),
                    DATA_ADMISSAO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENDIMENTO_CLIENTE_SIAPE", x => x.ID_RENDIMENTO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_SIAPE_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                        column: x => x.ID_RENDIMENTO_CLIENTE,
                        principalTable: "RENDIMENTO_CLIENTE",
                        principalColumn: "ID_RENDIMENTO_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_SIAPE_SIAPE_TIPO_FUNCIONAL_ID_SIAPE_TIPO_FUNCIONAL",
                        column: x => x.ID_SIAPE_TIPO_FUNCIONAL,
                        principalTable: "SIAPE_TIPO_FUNCIONAL",
                        principalColumn: "ID_SIAPE_TIPO_FUNCIONAL");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_BANCO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_BANCO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CLIENTE",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONVENIO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONVENIO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_CONVENIO_ORGAO",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_CONVENIO_ORGAO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_TIPO_CONTA");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_ID_UF",
                table: "RENDIMENTO_CLIENTE",
                column: "ID_UF");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_INSS_ID_INSS_ESPECIE_BENEFICIO",
                table: "RENDIMENTO_CLIENTE_INSS",
                column: "ID_INSS_ESPECIE_BENEFICIO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_SIAPE_ID_SIAPE_TIPO_FUNCIONAL",
                table: "RENDIMENTO_CLIENTE_SIAPE",
                column: "ID_SIAPE_TIPO_FUNCIONAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RENDIMENTO_CLIENTE_INSS");

            migrationBuilder.DropTable(
                name: "RENDIMENTO_CLIENTE_SIAPE");

            migrationBuilder.DropTable(
                name: "RENDIMENTO_CLIENTE");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class Notificacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NOTIFICACAO_FINALIDADE",
                columns: table => new
                {
                    ID_NOTIFICACAO_FINALIDADE = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO_FINALIDADE", x => x.ID_NOTIFICACAO_FINALIDADE);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACAO_SEVERIDADE",
                columns: table => new
                {
                    ID_NOTIFICACAO_SEVERIDADE = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO_SEVERIDADE", x => x.ID_NOTIFICACAO_SEVERIDADE);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACAO_TEMPLATE",
                columns: table => new
                {
                    ID_NOTIFICACAO_TEMPLATE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITULO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    URL_REFERENCIA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ID_NOTIFICACAO_SEVERIDADE = table.Column<int>(type: "int", nullable: false),
                    ID_NOTIFICACAO_FINALIDADE = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO_TEMPLATE", x => x.ID_NOTIFICACAO_TEMPLATE);
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_TEMPLATE_NOTIFICACAO_FINALIDADE_ID_NOTIFICACAO_FINALIDADE",
                        column: x => x.ID_NOTIFICACAO_FINALIDADE,
                        principalTable: "NOTIFICACAO_FINALIDADE",
                        principalColumn: "ID_NOTIFICACAO_FINALIDADE");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_TEMPLATE_NOTIFICACAO_SEVERIDADE_ID_NOTIFICACAO_SEVERIDADE",
                        column: x => x.ID_NOTIFICACAO_SEVERIDADE,
                        principalTable: "NOTIFICACAO_SEVERIDADE",
                        principalColumn: "ID_NOTIFICACAO_SEVERIDADE");
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACAO",
                columns: table => new
                {
                    ID_NOTIFICACAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    ID_TEMPLATE_NOTIFICACAO = table.Column<int>(type: "int", nullable: false),
                    TITULO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    URL_REFERENCIA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DATA_VISUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_NOTIFICACAO_SEVERIDADE = table.Column<int>(type: "int", nullable: false),
                    ID_NOTIFICACAO_FINALIDADE = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO", x => x.ID_NOTIFICACAO);
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_NOTIFICACAO_FINALIDADE_ID_NOTIFICACAO_FINALIDADE",
                        column: x => x.ID_NOTIFICACAO_FINALIDADE,
                        principalTable: "NOTIFICACAO_FINALIDADE",
                        principalColumn: "ID_NOTIFICACAO_FINALIDADE");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_NOTIFICACAO_SEVERIDADE_ID_NOTIFICACAO_SEVERIDADE",
                        column: x => x.ID_NOTIFICACAO_SEVERIDADE,
                        principalTable: "NOTIFICACAO_SEVERIDADE",
                        principalColumn: "ID_NOTIFICACAO_SEVERIDADE");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_NOTIFICACAO_TEMPLATE_ID_TEMPLATE_NOTIFICACAO",
                        column: x => x.ID_TEMPLATE_NOTIFICACAO,
                        principalTable: "NOTIFICACAO_TEMPLATE",
                        principalColumn: "ID_NOTIFICACAO_TEMPLATE");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_ID_NOTIFICACAO_FINALIDADE",
                table: "NOTIFICACAO",
                column: "ID_NOTIFICACAO_FINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_ID_NOTIFICACAO_SEVERIDADE",
                table: "NOTIFICACAO",
                column: "ID_NOTIFICACAO_SEVERIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_ID_TEMPLATE_NOTIFICACAO",
                table: "NOTIFICACAO",
                column: "ID_TEMPLATE_NOTIFICACAO");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_ID_USUARIO",
                table: "NOTIFICACAO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_TEMPLATE_ID_NOTIFICACAO_FINALIDADE",
                table: "NOTIFICACAO_TEMPLATE",
                column: "ID_NOTIFICACAO_FINALIDADE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_TEMPLATE_ID_NOTIFICACAO_SEVERIDADE",
                table: "NOTIFICACAO_TEMPLATE",
                column: "ID_NOTIFICACAO_SEVERIDADE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTIFICACAO");

            migrationBuilder.DropTable(
                name: "NOTIFICACAO_TEMPLATE");

            migrationBuilder.DropTable(
                name: "NOTIFICACAO_FINALIDADE");

            migrationBuilder.DropTable(
                name: "NOTIFICACAO_SEVERIDADE");
        }
    }
}

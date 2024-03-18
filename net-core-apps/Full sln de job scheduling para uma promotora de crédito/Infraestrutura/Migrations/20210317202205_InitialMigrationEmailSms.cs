using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class InitialMigrationEmailSms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "SITUACAO_ENVIO",
                columns: table => new
                {
                    ID_SITUACAO_ENVIO = table.Column<int>(nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITUACAO_ENVIO", x => x.ID_SITUACAO_ENVIO);
                });

            migrationBuilder.CreateTable(
                name: "SITUACAO_ENVIO_DETALHES",
                columns: table => new
                {
                    ID_SITUACAO_ENVIO_DETALHES = table.Column<int>(nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITUACAO_ENVIO_DETALHES", x => x.ID_SITUACAO_ENVIO_DETALHES);
                });

            migrationBuilder.CreateTable(
                name: "EMAIL_FORNECEDOR",
                columns: table => new
                {
                    ID_EMAIL_FORNECEDOR = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME_EXIBICAO = table.Column<string>(maxLength: 50, nullable: true),
                    USUARIO = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(maxLength: 50, nullable: false),
                    HOST = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PORTA = table.Column<int>(maxLength: 4, nullable: false),
                    SSL = table.Column<bool>(nullable: false),
                    ID_EMPRESA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAIL_FORNECEDOR", x => x.ID_EMAIL_FORNECEDOR);
                    table.ForeignKey(
                        name: "FK_EMAIL_FORNECEDOR_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "SMS_FORNECEDOR",
                columns: table => new
                {
                    ID_SMS_FORNECEDOR = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME_EXIBICAO = table.Column<string>(maxLength: 50, nullable: true),
                    USUARIO = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(maxLength: 50, nullable: false),
                    CODIGO_AGRUPADOR = table.Column<int>(maxLength: 3, nullable: false),
                    ID_EMPRESA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS_FORNECEDOR", x => x.ID_SMS_FORNECEDOR);
                    table.ForeignKey(
                        name: "FK_SMS_FORNECEDOR_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "EMAIL_MENSAGEM",
                columns: table => new
                {
                    ID_EMAIL_MENSAGEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CODIGO_REFERENCIA_EMAIL = table.Column<string>(maxLength: 13, nullable: false),
                    DESTINATARIO = table.Column<string>(nullable: false),
                    COPIA = table.Column<string>(nullable: true),
                    ASSUNTO = table.Column<string>(maxLength: 100, nullable: false),
                    MENSAGEM = table.Column<string>(nullable: false),
                    PRIORITARIO = table.Column<bool>(nullable: false),
                    DataInsercao = table.Column<DateTime>(nullable: false),
                    DATA_ENVIO = table.Column<DateTime>(nullable: true),
                    DATA_RECEBIMENTO = table.Column<DateTime>(nullable: true),
                    ID_EMAIL_FORNECEDOR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAIL_MENSAGEM", x => x.ID_EMAIL_MENSAGEM);
                    table.ForeignKey(
                        name: "FK_EMAIL_MENSAGEM_EMAIL_FORNECEDOR_ID_EMAIL_FORNECEDOR",
                        column: x => x.ID_EMAIL_FORNECEDOR,
                        principalTable: "EMAIL_FORNECEDOR",
                        principalColumn: "ID_EMAIL_FORNECEDOR");
                });

            migrationBuilder.CreateTable(
                name: "SMS_MENSAGEM",
                columns: table => new
                {
                    ID_SMS_MENSAGEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CODIGO_REFERENCIA_MENSAGEM = table.Column<string>(maxLength: 13, nullable: false),
                    NUMERO_TELEFONE = table.Column<string>(maxLength: 14, nullable: false),
                    MENSAGEM = table.Column<string>(nullable: false),
                    OPERADORA = table.Column<string>(maxLength: 20, nullable: true),
                    PROCESSADO = table.Column<bool>(nullable: false),
                    DATA_INSERCAO = table.Column<DateTime>(nullable: false),
                    DATA_ENVIO = table.Column<DateTime>(nullable: true),
                    DATA_RECEBIMENTO = table.Column<DateTime>(nullable: true),
                    ID_SMS_FORNECEDOR = table.Column<int>(nullable: false),
                    ID_SITUACAO_ENVIO = table.Column<int>(nullable: true),
                    ID_SITUACAO_ENVIO_DETALHES = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS_MENSAGEM", x => x.ID_SMS_MENSAGEM);
                    table.ForeignKey(
                        name: "FK_SMS_MENSAGEM_SITUACAO_ENVIO_ID_SITUACAO_ENVIO",
                        column: x => x.ID_SITUACAO_ENVIO,
                        principalTable: "SITUACAO_ENVIO",
                        principalColumn: "ID_SITUACAO_ENVIO");
                    table.ForeignKey(
                        name: "FK_SMS_MENSAGEM_SITUACAO_ENVIO_DETALHES_ID_SITUACAO_ENVIO_DETALHES",
                        column: x => x.ID_SITUACAO_ENVIO_DETALHES,
                        principalTable: "SITUACAO_ENVIO_DETALHES",
                        principalColumn: "ID_SITUACAO_ENVIO_DETALHES");
                    table.ForeignKey(
                        name: "FK_SMS_MENSAGEM_SMS_FORNECEDOR_ID_SMS_FORNECEDOR",
                        column: x => x.ID_SMS_FORNECEDOR,
                        principalTable: "SMS_FORNECEDOR",
                        principalColumn: "ID_SMS_FORNECEDOR");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_FORNECEDOR_ID_EMPRESA",
                table: "EMAIL_FORNECEDOR",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_MENSAGEM_ID_EMAIL_FORNECEDOR",
                table: "EMAIL_MENSAGEM",
                column: "ID_EMAIL_FORNECEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_FORNECEDOR_ID_EMPRESA",
                table: "SMS_FORNECEDOR",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_MENSAGEM_ID_SITUACAO_ENVIO",
                table: "SMS_MENSAGEM",
                column: "ID_SITUACAO_ENVIO");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_MENSAGEM_ID_SITUACAO_ENVIO_DETALHES",
                table: "SMS_MENSAGEM",
                column: "ID_SITUACAO_ENVIO_DETALHES");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_MENSAGEM_ID_SMS_FORNECEDOR",
                table: "SMS_MENSAGEM",
                column: "ID_SMS_FORNECEDOR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMAIL_MENSAGEM");

            migrationBuilder.DropTable(
                name: "SMS_MENSAGEM");

            migrationBuilder.DropTable(
                name: "EMAIL_FORNECEDOR");

            migrationBuilder.DropTable(
                name: "SITUACAO_ENVIO");

            migrationBuilder.DropTable(
                name: "SITUACAO_ENVIO_DETALHES");

            migrationBuilder.DropTable(
                name: "SMS_FORNECEDOR");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}

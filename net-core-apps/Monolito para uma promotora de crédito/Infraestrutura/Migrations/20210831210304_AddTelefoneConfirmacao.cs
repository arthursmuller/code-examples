using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddTelefoneConfirmacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_TEMPLATE_EMAIL_TIPO",
                table: "REDE_SOCIAL",
                newName: "ID_REDE_SOCIAL");

            migrationBuilder.AddColumn<bool>(
                name: "CONFIRMADO",
                table: "TELEFONE_CLIENTE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TIPO_SOLICITACAO_CONFIRMACAO",
                columns: table => new
                {
                    ID_TIPO_SOLICITACAO_CONFIRMACAO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_SOLICITACAO_CONFIRMACAO", x => x.ID_TIPO_SOLICITACAO_CONFIRMACAO);
                });

            migrationBuilder.CreateTable(
                name: "TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO",
                columns: table => new
                {
                    ID_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TIPO_SOLICITACAO_CONFIRMACAO = table.Column<int>(type: "int", nullable: false),
                    ID_TELEFONE_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    TOKEN = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ENVIADA = table.Column<bool>(type: "bit", nullable: false),
                    DATA_ENVIO_SOLICITACAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MENSAGEM_ERRO = table.Column<string>(type: "varchar(max)", nullable: true),
                    QUANTIDADE_ENVIOS_EFETUADOS = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO", x => x.ID_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO);
                    table.ForeignKey(
                        name: "FK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_TELEFONE_CLIENTE_ID_TELEFONE_CLIENTE",
                        column: x => x.ID_TELEFONE_CLIENTE,
                        principalTable: "TELEFONE_CLIENTE",
                        principalColumn: "ID_TELEFONE_CLIENTE");
                    table.ForeignKey(
                        name: "FK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_TIPO_SOLICITACAO_CONFIRMACAO_ID_TIPO_SOLICITACAO_CONFIRMACAO",
                        column: x => x.ID_TIPO_SOLICITACAO_CONFIRMACAO,
                        principalTable: "TIPO_SOLICITACAO_CONFIRMACAO",
                        principalColumn: "ID_TIPO_SOLICITACAO_CONFIRMACAO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTENCAO_OPERACAO_OBSERVACAO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_OBSERVACAO",
                column: "ID_INTENCAO_OPERACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_ID_TELEFONE_CLIENTE",
                table: "TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO",
                column: "ID_TELEFONE_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_ID_TIPO_SOLICITACAO_CONFIRMACAO",
                table: "TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO",
                column: "ID_TIPO_SOLICITACAO_CONFIRMACAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO");

            migrationBuilder.DropTable(
                name: "TIPO_SOLICITACAO_CONFIRMACAO");

            migrationBuilder.DropIndex(
                name: "IX_INTENCAO_OPERACAO_OBSERVACAO_ID_INTENCAO_OPERACAO",
                table: "INTENCAO_OPERACAO_OBSERVACAO");

            migrationBuilder.DropColumn(
                name: "CONFIRMADO",
                table: "TELEFONE_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ID_REDE_SOCIAL",
                table: "REDE_SOCIAL",
                newName: "ID_TEMPLATE_EMAIL_TIPO");
        }
    }
}

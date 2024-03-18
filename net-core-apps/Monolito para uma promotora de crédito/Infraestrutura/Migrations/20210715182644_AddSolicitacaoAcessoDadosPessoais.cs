using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddSolicitacaoAcessoDadosPessoais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                columns: table => new
                {
                    ID_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SOBRENOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    NOME_MAE = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TELEFONE_COMPLETO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NOTIFICACAO_ENVIADA = table.Column<bool>(type: "bit", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE", x => x.ID_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE");
        }
    }
}

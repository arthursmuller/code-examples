using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddMotivoSolicitacaoAcessoDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NOME_MAE",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOTIVO",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                type: "varchar(8000)",
                unicode: false,
                maxLength: 8000,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MOTIVO",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE");

            migrationBuilder.AlterColumn<string>(
                name: "NOME_MAE",
                table: "SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}

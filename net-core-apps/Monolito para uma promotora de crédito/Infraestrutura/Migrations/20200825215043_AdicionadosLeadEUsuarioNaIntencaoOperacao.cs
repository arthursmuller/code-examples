using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicionadosLeadEUsuarioNaIntencaoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "SITUACOES_INTENCAO_OPERACAO",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ID_LEAD",
                table: "INTENCOES_OPERACAO",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO",
                table: "INTENCOES_OPERACAO",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_INTENCOES_OPERACAO_ID_LEAD",
                table: "INTENCOES_OPERACAO",
                column: "ID_LEAD");

            migrationBuilder.CreateIndex(
                name: "IX_INTENCOES_OPERACAO_ID_USUARIO",
                table: "INTENCOES_OPERACAO",
                column: "ID_USUARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_LEADS_ID_LEAD",
                table: "INTENCOES_OPERACAO",
                column: "ID_LEAD",
                principalTable: "LEADS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO",
                table: "INTENCOES_OPERACAO",
                column: "ID_USUARIO",
                principalTable: "USUARIOS",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_LEADS_ID_LEAD",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
                name: "IX_INTENCOES_OPERACAO_ID_LEAD",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropIndex(
                name: "IX_INTENCOES_OPERACAO_ID_USUARIO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropColumn(
                name: "ID_LEAD",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO",
                table: "INTENCOES_OPERACAO");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "SITUACOES_INTENCAO_OPERACAO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}

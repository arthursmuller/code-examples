using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class TesteParametrosOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PARAMETROS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                column: "IdTipoOperacao");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO",
                column: "IdTipoOperacao",
                principalTable: "TIPOS_OPERACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropIndex(
                name: "IX_PARAMETROS_OPERACAO_IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "IdTipoOperacao",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddColumn<string>(
                name: "TipoOperacao",
                table: "PARAMETROS_OPERACAO",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}

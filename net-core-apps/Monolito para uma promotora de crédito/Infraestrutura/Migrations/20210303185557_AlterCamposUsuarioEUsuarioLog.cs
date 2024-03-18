using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AlterCamposUsuarioEUsuarioLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TOKEN_JWT",
                table: "USUARIO_REQUISICAO_LOG");

            migrationBuilder.DropColumn(
                name: "EXCLUSIVO_TENANT",
                table: "USUARIO");

            migrationBuilder.AddColumn<string>(
                name: "CORPO_REQUISICAO",
                table: "USUARIO_REQUISICAO_LOG",
                type: "varchar(8000)",
                unicode: false,
                maxLength: 8000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CORPO_REQUISICAO",
                table: "USUARIO_REQUISICAO_LOG");

            migrationBuilder.AddColumn<string>(
                name: "TOKEN_JWT",
                table: "USUARIO_REQUISICAO_LOG",
                type: "varchar(8000)",
                unicode: false,
                maxLength: 8000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EXCLUSIVO_TENANT",
                table: "USUARIO",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

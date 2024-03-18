using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class UsuarioRecuperacaoSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_RECUPERACAO_SENHA",
                columns: table => new
                {
                    ID_USUARIO_RECUPERACAO_SENHA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    TOKEN = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DATA_REQUISICAO = table.Column<DateTime>(nullable: false),
                    ID_USUARIO = table.Column<int>(nullable: false),
                    VALIDO = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_RECUPERACAO_SENHA", x => x.ID_USUARIO_RECUPERACAO_SENHA);
                    table.ForeignKey(
                        name: "FK_USUARIO_RECUPERACAO_SENHA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_RECUPERACAO_SENHA_ID_USUARIO",
                table: "USUARIO_RECUPERACAO_SENHA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_RECUPERACAO_SENHA_TOKEN",
                table: "USUARIO_RECUPERACAO_SENHA",
                column: "TOKEN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_RECUPERACAO_SENHA");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddUsuarioRequisicaoLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_REQUISICAO_LOG",
                columns: table => new
                {
                    ID_USUARIO_REQUISICAO_LOG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_TENANT = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    URL_REQUISICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    TOKEN_JWT = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_REQUISICAO_LOG", x => x.ID_USUARIO_REQUISICAO_LOG);
                    table.ForeignKey(
                        name: "FK_USUARIO_REQUISICAO_LOG_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_REQUISICAO_LOG_ID_USUARIO",
                table: "USUARIO_REQUISICAO_LOG",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_REQUISICAO_LOG");
        }
    }
}

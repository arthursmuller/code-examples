using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddLoginSocial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "REDE_SOCIAL",
                columns: table => new
                {
                    ID_TEMPLATE_EMAIL_TIPO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REDE_SOCIAL", x => x.ID_TEMPLATE_EMAIL_TIPO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_REDE_SOCIAL",
                columns: table => new
                {
                    ID_USUARIO_REDE_SOCIAL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    ID_REDE_SOCIAL = table.Column<int>(type: "int", nullable: false),
                    LOGIN = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_REDE_SOCIAL", x => x.ID_USUARIO_REDE_SOCIAL);
                    table.ForeignKey(
                        name: "FK_USUARIO_REDE_SOCIAL_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_REDE_SOCIAL_ID_USUARIO_ID_REDE_SOCIAL",
                table: "USUARIO_REDE_SOCIAL",
                columns: new[] { "ID_USUARIO", "ID_REDE_SOCIAL" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REDE_SOCIAL");

            migrationBuilder.DropTable(
                name: "USUARIO_REDE_SOCIAL");
        }
    }
}

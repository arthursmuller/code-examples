using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddTermo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TIPO_TERMO",
                columns: table => new
                {
                    ID_TIPO_TERMO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_TERMO", x => x.ID_TIPO_TERMO);
                });

            migrationBuilder.CreateTable(
                name: "TERMO",
                columns: table => new
                {
                    ID_TERMO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TIPO_TERMO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    DATA_INICIO_VIGENCIA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TERMO", x => x.ID_TERMO);
                    table.ForeignKey(
                        name: "FK_TERMO_TIPO_TERMO_ID_TIPO_TERMO",
                        column: x => x.ID_TIPO_TERMO,
                        principalTable: "TIPO_TERMO",
                        principalColumn: "ID_TIPO_TERMO");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_TERMO",
                columns: table => new
                {
                    ID_USUARIO_TERMO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    ID_TERMO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_TERMO", x => x.ID_USUARIO_TERMO);
                    table.ForeignKey(
                        name: "FK_USUARIO_TERMO_TERMO_ID_TERMO",
                        column: x => x.ID_TERMO,
                        principalTable: "TERMO",
                        principalColumn: "ID_TERMO");
                    table.ForeignKey(
                        name: "FK_USUARIO_TERMO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TERMO_ID_TIPO_TERMO",
                table: "TERMO",
                column: "ID_TIPO_TERMO");

            migrationBuilder.CreateIndex(
                name: "IX_TIPO_TERMO_NOME",
                table: "TIPO_TERMO",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_TERMO_ID_TERMO",
                table: "USUARIO_TERMO",
                column: "ID_TERMO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_TERMO_ID_USUARIO",
                table: "USUARIO_TERMO",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_TERMO");

            migrationBuilder.DropTable(
                name: "TERMO");

            migrationBuilder.DropTable(
                name: "TIPO_TERMO");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddMunicipios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MUNICIPIO",
                columns: table => new
                {
                    ID_MUNICIPIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    ID_UNIDADE_FEDERATIVA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPIO", x => x.ID_MUNICIPIO);
                    table.ForeignKey(
                        name: "FK_MUNICIPIO_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA",
                        column: x => x.ID_UNIDADE_FEDERATIVA,
                        principalTable: "UNIDADE_FEDERATIVA",
                        principalColumn: "ID_UNIDADE_FEDERATIVA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MUNICIPIO_ID_UNIDADE_FEDERATIVA",
                table: "MUNICIPIO",
                column: "ID_UNIDADE_FEDERATIVA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MUNICIPIO");
        }
    }
}

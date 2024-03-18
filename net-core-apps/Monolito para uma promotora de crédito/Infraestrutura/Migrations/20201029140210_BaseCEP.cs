using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class BaseCEP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BASE_CEP",
                columns: table => new
                {
                    ID_BASE_CEP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CEP = table.Column<string>(unicode: false, maxLength: 8, nullable: false),
                    LOGRADOURO = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    BAIRRO = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    INFORMACAO_ADICIONAL = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PERMITE_AJUSTE_LOGRADOURO = table.Column<bool>(nullable: false),
                    IdTipoLogradouro = table.Column<int>(nullable: false),
                    IdMunicipio = table.Column<int>(nullable: false),
                    IdUnidadeFederativa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASE_CEP", x => x.ID_BASE_CEP);
                    table.ForeignKey(
                        name: "FK_BASE_CEP_MUNICIPIO_IdMunicipio",
                        column: x => x.IdMunicipio,
                        principalTable: "MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO");
                    table.ForeignKey(
                        name: "FK_BASE_CEP_TIPO_LOGRADOURO_IdTipoLogradouro",
                        column: x => x.IdTipoLogradouro,
                        principalTable: "TIPO_LOGRADOURO",
                        principalColumn: "ID_TIPO_LOGRADOURO");
                    table.ForeignKey(
                        name: "FK_BASE_CEP_UNIDADE_FEDERATIVA_IdUnidadeFederativa",
                        column: x => x.IdUnidadeFederativa,
                        principalTable: "UNIDADE_FEDERATIVA",
                        principalColumn: "ID_UNIDADE_FEDERATIVA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BASE_CEP_CEP",
                table: "BASE_CEP",
                column: "CEP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BASE_CEP_IdMunicipio",
                table: "BASE_CEP",
                column: "IdMunicipio");

            migrationBuilder.CreateIndex(
                name: "IX_BASE_CEP_IdTipoLogradouro",
                table: "BASE_CEP",
                column: "IdTipoLogradouro");

            migrationBuilder.CreateIndex(
                name: "IX_BASE_CEP_IdUnidadeFederativa",
                table: "BASE_CEP",
                column: "IdUnidadeFederativa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BASE_CEP");
        }
    }
}

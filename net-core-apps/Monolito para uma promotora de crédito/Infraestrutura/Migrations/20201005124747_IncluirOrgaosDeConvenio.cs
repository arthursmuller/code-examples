using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class IncluirOrgaosDeConvenio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONVENIO_ORGAO",
                columns: table => new
                {
                    ID_CONVENIO_ORGAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CODIGO = table.Column<string>(maxLength: 5, nullable: false),
                    NOME = table.Column<string>(maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(maxLength: 14, nullable: true),
                    ID_UNIDADE_FEDERATIVA = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONVENIO_ORGAO", x => x.ID_CONVENIO_ORGAO);
                    table.ForeignKey(
                        name: "FK_CONVENIO_ORGAO_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA",
                        column: x => x.ID_UNIDADE_FEDERATIVA,
                        principalTable: "UNIDADE_FEDERATIVA",
                        principalColumn: "ID_UNIDADE_FEDERATIVA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONVENIO_ORGAO_CODIGO",
                table: "CONVENIO_ORGAO",
                column: "CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONVENIO_ORGAO_ID_UNIDADE_FEDERATIVA",
                table: "CONVENIO_ORGAO",
                column: "ID_UNIDADE_FEDERATIVA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONVENIO_ORGAO");
        }
    }
}

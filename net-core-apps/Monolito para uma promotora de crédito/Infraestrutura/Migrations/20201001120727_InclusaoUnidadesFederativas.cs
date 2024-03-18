using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InclusaoUnidadesFederativas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UNIDADE_FEDERATIVA",
                columns: table => new
                {
                    ID_UNIDADE_FEDERATIVA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(maxLength: 20, nullable: false),
                    SIGLA = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIDADE_FEDERATIVA", x => x.ID_UNIDADE_FEDERATIVA);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UNIDADE_FEDERATIVA");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class IncluirGrausInstrucaoEstadoCivil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTADO_CIVIL",
                columns: table => new
                {
                    ID_ESTADO_CIVIL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(maxLength: 30, nullable: false),
                    SIGLA = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO_CIVIL", x => x.ID_ESTADO_CIVIL);
                });

            migrationBuilder.CreateTable(
                name: "GRAU_INSTRUCAO",
                columns: table => new
                {
                    ID_GRAU_INSTRUCAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRAU_INSTRUCAO", x => x.ID_GRAU_INSTRUCAO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ESTADO_CIVIL");

            migrationBuilder.DropTable(
                name: "GRAU_INSTRUCAO");
        }
    }
}

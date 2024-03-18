using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class LeadCorrespondente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEAD_CORRESPONDENTE",
                columns: table => new
                {
                    ID_LEAD_CORRESPONDENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TELEFONE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ID_MUNICIPIO = table.Column<int>(type: "int", nullable: false),
                    ATIVIDADES = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAD_CORRESPONDENTE", x => x.ID_LEAD_CORRESPONDENTE);
                    table.ForeignKey(
                        name: "FK_LEAD_CORRESPONDENTE_MUNICIPIO_ID_MUNICIPIO",
                        column: x => x.ID_MUNICIPIO,
                        principalTable: "MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEAD_CORRESPONDENTE_ID_MUNICIPIO",
                table: "LEAD_CORRESPONDENTE",
                column: "ID_MUNICIPIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEAD_CORRESPONDENTE");
        }
    }
}

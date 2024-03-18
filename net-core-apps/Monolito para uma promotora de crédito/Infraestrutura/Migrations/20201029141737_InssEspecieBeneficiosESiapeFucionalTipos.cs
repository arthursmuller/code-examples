using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InssEspecieBeneficiosESiapeFucionalTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INSS_ESPECIE_BENEFICIO",
                columns: table => new
                {
                    ID_INSS_ESPECIE_BENEFICIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    CODIGO = table.Column<string>(unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSS_ESPECIE_BENEFICIO", x => x.ID_INSS_ESPECIE_BENEFICIO);
                });

            migrationBuilder.CreateTable(
                name: "SIAPE_TIPO_FUNCIONAL",
                columns: table => new
                {
                    ID_SIAPE_TIPO_FUNCIONAL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    DESCRICAO = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    CODIGO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIAPE_TIPO_FUNCIONAL", x => x.ID_SIAPE_TIPO_FUNCIONAL);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INSS_ESPECIE_BENEFICIO");

            migrationBuilder.DropTable(
                name: "SIAPE_TIPO_FUNCIONAL");
        }
    }
}

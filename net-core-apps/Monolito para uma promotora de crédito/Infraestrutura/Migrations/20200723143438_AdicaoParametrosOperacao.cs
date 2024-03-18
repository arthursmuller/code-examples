using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicaoParametrosOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PARAMETROS_OPERACAO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstituicaoFinanceira = table.Column<string>(maxLength: 100, nullable: true),
                    Convenio = table.Column<string>(maxLength: 100, nullable: true),
                    TipoOperacao = table.Column<string>(maxLength: 100, nullable: true),
                    QuantidadeParcelas = table.Column<string>(maxLength: 100, nullable: true),
                    TaxaPlano = table.Column<string>(maxLength: 100, nullable: true),
                    TentativaRetencao = table.Column<bool>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARAMETROS_OPERACAO", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARAMETROS_OPERACAO");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class correcaoCamposIcatuSeguro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "CODIGO_PONTO_DE_VENDA",
                table: "SEGURO_PRODUTO_ICATU",
                newName: "CODIGO_PONTO_VENDA");

            migrationBuilder.AddColumn<int>(
                name: "CODIGO_GRUPO_APOLICE",
                table: "SEGURO_PRODUTO_ICATU",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CODIGO_GRUPO_APOLICE",
                table: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.RenameColumn(
                name: "CODIGO_PONTO_VENDA",
                table: "SEGURO_PRODUTO_ICATU",
                newName: "CODIGO_PONTO_DE_VENDA");
        }
    }
}

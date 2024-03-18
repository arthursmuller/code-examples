using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class UpdateSeguroProdutoIcatu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CODIGO_PONTO_DE_VENDA",
                table: "SEGURO_PRODUTO_ICATU",
                type: "varchar(2000)",
                unicode: false,
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_PARCERIA",
                table: "SEGURO_PRODUTO_ICATU",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SUBESTIPULANTE",
                table: "SEGURO_PRODUTO_ICATU",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TIPO_NUMERACAO_PROPOSTA",
                table: "SEGURO_PRODUTO_ICATU",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VALOR_PREMIO",
                table: "SEGURO_PRODUTO_ICATU",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CODIGO_PONTO_DE_VENDA",
                table: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.DropColumn(
                name: "ID_PARCERIA",
                table: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.DropColumn(
                name: "SUBESTIPULANTE",
                table: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.DropColumn(
                name: "TIPO_NUMERACAO_PROPOSTA",
                table: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.DropColumn(
                name: "VALOR_PREMIO",
                table: "SEGURO_PRODUTO_ICATU");
        }
    }
}

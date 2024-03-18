using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class intencaoOperacaoDataInclusaoPrazo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_INCLUSAO",
                table: "INTENCAO_OPERACAO",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PRAZO",
                table: "INTENCAO_OPERACAO",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_INCLUSAO",
                table: "INTENCAO_OPERACAO");

            migrationBuilder.DropColumn(
                name: "PRAZO",
                table: "INTENCAO_OPERACAO");
        }
    }
}

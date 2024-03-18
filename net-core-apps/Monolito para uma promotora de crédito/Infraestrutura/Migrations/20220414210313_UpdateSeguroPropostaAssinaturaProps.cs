using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class UpdateSeguroPropostaAssinaturaProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP_ORIGEM",
                table: "SEGURO_PROPOSTA",
                type: "varchar(19)",
                unicode: false,
                maxLength: 19,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LATITUDE",
                table: "SEGURO_PROPOSTA",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LONGITUDE",
                table: "SEGURO_PROPOSTA",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IP_ORIGEM",
                table: "SEGURO_PROPOSTA");

            migrationBuilder.DropColumn(
                name: "LATITUDE",
                table: "SEGURO_PROPOSTA");

            migrationBuilder.DropColumn(
                name: "LONGITUDE",
                table: "SEGURO_PROPOSTA");
        }
    }
}

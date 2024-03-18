using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RelacionamentoParametrosOperacaoConvenio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Convenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddColumn<int>(
                name: "IdConvenio",
                table: "PARAMETROS_OPERACAO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PARAMETROS_OPERACAO_IdConvenio",
                table: "PARAMETROS_OPERACAO",
                column: "IdConvenio");

            migrationBuilder.AddForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO",
                column: "IdConvenio",
                principalTable: "CONVENIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropIndex(
                name: "IX_PARAMETROS_OPERACAO_IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.DropColumn(
                name: "IdConvenio",
                table: "PARAMETROS_OPERACAO");

            migrationBuilder.AddColumn<string>(
                name: "Convenio",
                table: "PARAMETROS_OPERACAO",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}

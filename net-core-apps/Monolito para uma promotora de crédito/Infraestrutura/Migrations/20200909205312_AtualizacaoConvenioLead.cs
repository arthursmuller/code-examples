using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AtualizacaoConvenioLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CONVENIO",
                table: "LEADS");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "LEADS",
                newName: "NOME");

            migrationBuilder.AddColumn<int>(
                name: "ConvenioID",
                table: "LEADS",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_CONVENIO",
                table: "LEADS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LEADS_ConvenioID",
                table: "LEADS",
                column: "ConvenioID");

            migrationBuilder.AddForeignKey(
                name: "FK_LEADS_CONVENIOS_ConvenioID",
                table: "LEADS",
                column: "ConvenioID",
                principalTable: "CONVENIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LEADS_CONVENIOS_ConvenioID",
                table: "LEADS");

            migrationBuilder.DropIndex(
                name: "IX_LEADS_ConvenioID",
                table: "LEADS");

            migrationBuilder.DropColumn(
                name: "ConvenioID",
                table: "LEADS");

            migrationBuilder.DropColumn(
                name: "ID_CONVENIO",
                table: "LEADS");

            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "LEADS",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "CONVENIO",
                table: "LEADS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}

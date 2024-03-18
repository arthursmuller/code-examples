using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AtualizacaoChavesConvenioLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_LEADS_ID_CONVENIO",
                table: "LEADS",
                column: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_LEADS_CONVENIOS_ID_CONVENIO",
                table: "LEADS",
                column: "ID_CONVENIO",
                principalTable: "CONVENIOS",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LEADS_CONVENIOS_ID_CONVENIO",
                table: "LEADS");

            migrationBuilder.DropIndex(
                name: "IX_LEADS_ID_CONVENIO",
                table: "LEADS");

            migrationBuilder.AddColumn<int>(
                name: "ConvenioID",
                table: "LEADS",
                type: "int",
                nullable: true);

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
    }
}

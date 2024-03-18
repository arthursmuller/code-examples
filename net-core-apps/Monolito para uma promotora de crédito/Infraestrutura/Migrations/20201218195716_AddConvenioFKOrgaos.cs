using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddConvenioFKOrgaos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_CONVENIO",
                table: "CONVENIO_ORGAO",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CONVENIO_ORGAO_ID_CONVENIO",
                table: "CONVENIO_ORGAO",
                column: "ID_CONVENIO");

            migrationBuilder.AddForeignKey(
                name: "FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO",
                table: "CONVENIO_ORGAO",
                column: "ID_CONVENIO",
                principalTable: "CONVENIO",
                principalColumn: "ID_CONVENIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO",
                table: "CONVENIO_ORGAO");

            migrationBuilder.DropIndex(
                name: "IX_CONVENIO_ORGAO_ID_CONVENIO",
                table: "CONVENIO_ORGAO");

            migrationBuilder.DropColumn(
                name: "ID_CONVENIO",
                table: "CONVENIO_ORGAO");
        }
    }
}

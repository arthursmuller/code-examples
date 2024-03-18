using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class fixedidSeguroProposta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_CLIENTE_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_PROPOSTA",
                unique: true,
                filter: "[ID_SEGURO_PROPOSTA] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_PROPOSTA",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_PROPOSTA",
                principalTable: "SEGURO_PROPOSTA",
                principalColumn: "ID_SEGURO_PROPOSTA",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_PROPOSTA",
                table: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.DropIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA",
                table: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_CLIENTE_ICATU",
                unique: true,
                filter: "[ID_SEGURO_CLIENTE_ICATU] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_CLIENTE_ICATU",
                principalTable: "SEGURO_PROPOSTA",
                principalColumn: "ID_SEGURO_PROPOSTA",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

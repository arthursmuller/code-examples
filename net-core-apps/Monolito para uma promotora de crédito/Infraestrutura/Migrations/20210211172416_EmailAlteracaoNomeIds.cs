using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class EmailAlteracaoNomeIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                table: "REGISTRO_EMAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_TIPO",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropIndex(
                name: "IX_TEMPLATE_EMAIL_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropIndex(
                name: "IX_TEMPLATE_EMAIL_TIPO_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.RenameColumn(name: "FINALIDADE", table: "TEMPLATE_EMAIL", "ID_FINALIDADE");
            migrationBuilder.RenameColumn(name: "TIPO", table: "TEMPLATE_EMAIL", "ID_TIPO");
            migrationBuilder.RenameColumn(name: "FINALIDADE", table: "REGISTRO_EMAIL", "ID_FINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                column: "ID_FINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_ID_TIPO_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                columns: new[] { "ID_TIPO", "ID_FINALIDADE" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_EMAIL_ID_FINALIDADE",
                table: "REGISTRO_EMAIL",
                column: "ID_FINALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE",
                table: "REGISTRO_EMAIL",
                column: "ID_FINALIDADE",
                principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                principalColumn: "ID_TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                column: "ID_FINALIDADE",
                principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                principalColumn: "ID_TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_ID_TIPO",
                table: "TEMPLATE_EMAIL",
                column: "ID_TIPO",
                principalTable: "TEMPLATE_EMAIL_TIPO",
                principalColumn: "ID_TEMPLATE_EMAIL_TIPO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE",
                table: "REGISTRO_EMAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_ID_TIPO",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropIndex(
                name: "IX_TEMPLATE_EMAIL_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropIndex(
                name: "IX_TEMPLATE_EMAIL_ID_TIPO_ID_FINALIDADE",
                table: "TEMPLATE_EMAIL");

            migrationBuilder.DropIndex(
                name: "IX_REGISTRO_EMAIL_ID_FINALIDADE",
                table: "REGISTRO_EMAIL");

            migrationBuilder.RenameColumn(name: "ID_FINALIDADE", table: "TEMPLATE_EMAIL", "FINALIDADE");
            migrationBuilder.RenameColumn(name: "ID_TIPO", table: "TEMPLATE_EMAIL", "TIPO");
            migrationBuilder.RenameColumn(name: "ID_FINALIDADE", table: "REGISTRO_EMAIL", "IFINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                column: "FINALIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_EMAIL_TIPO_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                columns: new[] { "TIPO", "FINALIDADE" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                table: "REGISTRO_EMAIL",
                column: "FINALIDADE",
                principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                principalColumn: "ID_TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                table: "TEMPLATE_EMAIL",
                column: "FINALIDADE",
                principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                principalColumn: "ID_TEMPLATE_EMAIL_FINALIDADE");

            migrationBuilder.AddForeignKey(
                name: "FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_TIPO",
                table: "TEMPLATE_EMAIL",
                column: "TIPO",
                principalTable: "TEMPLATE_EMAIL_TIPO",
                principalColumn: "ID_TEMPLATE_EMAIL_TIPO");
        }
    }
}

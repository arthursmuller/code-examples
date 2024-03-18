using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddRenameColumnsBaseCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_MUNICIPIO_IdMunicipio",
                table: "BASE_CEP");

            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_TIPO_LOGRADOURO_IdTipoLogradouro",
                table: "BASE_CEP");

            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_UNIDADE_FEDERATIVA_IdUnidadeFederativa",
                table: "BASE_CEP");

            migrationBuilder.RenameColumn(
                name: "IdUnidadeFederativa",
                table: "BASE_CEP",
                newName: "ID_UNIDADE_FEDERATIVA");

            migrationBuilder.RenameColumn(
                name: "IdTipoLogradouro",
                table: "BASE_CEP",
                newName: "ID_TIPO_LOGRADOURO");

            migrationBuilder.RenameColumn(
                name: "IdMunicipio",
                table: "BASE_CEP",
                newName: "ID_MUNICIPIO");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_IdUnidadeFederativa",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_ID_UNIDADE_FEDERATIVA");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_IdTipoLogradouro",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_ID_TIPO_LOGRADOURO");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_IdMunicipio",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_ID_MUNICIPIO");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_MUNICIPIO_ID_MUNICIPIO",
                table: "BASE_CEP",
                column: "ID_MUNICIPIO",
                principalTable: "MUNICIPIO",
                principalColumn: "ID_MUNICIPIO");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "BASE_CEP",
                column: "ID_TIPO_LOGRADOURO",
                principalTable: "TIPO_LOGRADOURO",
                principalColumn: "ID_TIPO_LOGRADOURO");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA",
                table: "BASE_CEP",
                column: "ID_UNIDADE_FEDERATIVA",
                principalTable: "UNIDADE_FEDERATIVA",
                principalColumn: "ID_UNIDADE_FEDERATIVA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_MUNICIPIO_ID_MUNICIPIO",
                table: "BASE_CEP");

            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "BASE_CEP");

            migrationBuilder.DropForeignKey(
                name: "FK_BASE_CEP_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA",
                table: "BASE_CEP");

            migrationBuilder.RenameColumn(
                name: "ID_UNIDADE_FEDERATIVA",
                table: "BASE_CEP",
                newName: "IdUnidadeFederativa");

            migrationBuilder.RenameColumn(
                name: "ID_TIPO_LOGRADOURO",
                table: "BASE_CEP",
                newName: "IdTipoLogradouro");

            migrationBuilder.RenameColumn(
                name: "ID_MUNICIPIO",
                table: "BASE_CEP",
                newName: "IdMunicipio");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_ID_UNIDADE_FEDERATIVA",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_IdUnidadeFederativa");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_ID_TIPO_LOGRADOURO",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_IdTipoLogradouro");

            migrationBuilder.RenameIndex(
                name: "IX_BASE_CEP_ID_MUNICIPIO",
                table: "BASE_CEP",
                newName: "IX_BASE_CEP_IdMunicipio");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_MUNICIPIO_IdMunicipio",
                table: "BASE_CEP",
                column: "IdMunicipio",
                principalTable: "MUNICIPIO",
                principalColumn: "ID_MUNICIPIO");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_TIPO_LOGRADOURO_IdTipoLogradouro",
                table: "BASE_CEP",
                column: "IdTipoLogradouro",
                principalTable: "TIPO_LOGRADOURO",
                principalColumn: "ID_TIPO_LOGRADOURO");

            migrationBuilder.AddForeignKey(
                name: "FK_BASE_CEP_UNIDADE_FEDERATIVA_IdUnidadeFederativa",
                table: "BASE_CEP",
                column: "IdUnidadeFederativa",
                principalTable: "UNIDADE_FEDERATIVA",
                principalColumn: "ID_UNIDADE_FEDERATIVA");
        }
    }
}

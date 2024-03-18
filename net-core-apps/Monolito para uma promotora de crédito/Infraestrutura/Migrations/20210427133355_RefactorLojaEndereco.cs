using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RefactorLojaEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ESTADO",
                table: "LOJA");

            migrationBuilder.DropColumn(
                name: "LATITUDE",
                table: "LOJA");

            migrationBuilder.DropColumn(
                name: "LONGITUDE",
                table: "LOJA");

            migrationBuilder.RenameColumn(
                name: "ENDERECO",
                table: "LOJA",
                newName: "LOGRADOURO");

            migrationBuilder.DropColumn(
                name: "CIDADE",
                table: "LOJA");

            migrationBuilder.AddColumn<string>(
                name: "COMPLEMENTO",
                table: "LOJA",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_MUNICIPIO",
                table: "LOJA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_TIPO_LOGRADOURO",
                table: "LOJA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NUMERO",
                table: "LOJA",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LOJA_ID_MUNICIPIO",
                table: "LOJA",
                column: "ID_MUNICIPIO");

            migrationBuilder.CreateIndex(
                name: "IX_LOJA_ID_TIPO_LOGRADOURO",
                table: "LOJA",
                column: "ID_TIPO_LOGRADOURO");

            migrationBuilder.AddForeignKey(
                name: "FK_LOJA_MUNICIPIO_ID_MUNICIPIO",
                table: "LOJA",
                column: "ID_MUNICIPIO",
                principalTable: "MUNICIPIO",
                principalColumn: "ID_MUNICIPIO");

            migrationBuilder.AddForeignKey(
                name: "FK_LOJA_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "LOJA",
                column: "ID_TIPO_LOGRADOURO",
                principalTable: "TIPO_LOGRADOURO",
                principalColumn: "ID_TIPO_LOGRADOURO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LOJA_MUNICIPIO_ID_MUNICIPIO",
                table: "LOJA");

            migrationBuilder.DropForeignKey(
                name: "FK_LOJA_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "LOJA");

            migrationBuilder.DropIndex(
                name: "IX_LOJA_ID_MUNICIPIO",
                table: "LOJA");

            migrationBuilder.DropIndex(
                name: "IX_LOJA_ID_TIPO_LOGRADOURO",
                table: "LOJA");

            migrationBuilder.DropColumn(
                name: "ID_MUNICIPIO",
                table: "LOJA");

            migrationBuilder.DropColumn(
                name: "ID_TIPO_LOGRADOURO",
                table: "LOJA");

            migrationBuilder.DropColumn(
                name: "NUMERO",
                table: "LOJA");

            migrationBuilder.RenameColumn(
                name: "LOGRADOURO",
                table: "LOJA",
                newName: "ENDERECO");

            migrationBuilder.RenameColumn(
                name: "COMPLEMENTO",
                table: "LOJA",
                newName: "CIDADE");

            migrationBuilder.AddColumn<string>(
                name: "ESTADO",
                table: "LOJA",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LATITUDE",
                table: "LOJA",
                type: "float",
                maxLength: 10,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LONGITUDE",
                table: "LOJA",
                type: "float",
                maxLength: 10,
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

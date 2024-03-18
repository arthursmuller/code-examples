using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AtualizaEnderecoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ENDERECO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.AddColumn<string>(
                name: "COMPLEMENTO",
                table: "ENDERECO_CLIENTE",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LOGRADOURO",
                table: "ENDERECO_CLIENTE",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NUMERO",
                table: "ENDERECO_CLIENTE",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECO_CLIENTE_ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE",
                column: "ID_TIPO_LOGRADOURO");

            migrationBuilder.AddForeignKey(
                name: "FK_ENDERECO_CLIENTE_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE",
                column: "ID_TIPO_LOGRADOURO",
                principalTable: "TIPO_LOGRADOURO",
                principalColumn: "ID_TIPO_LOGRADOURO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENDERECO_CLIENTE_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_ENDERECO_CLIENTE_ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "COMPLEMENTO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_TIPO_LOGRADOURO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "LOGRADOURO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.DropColumn(
                name: "NUMERO",
                table: "ENDERECO_CLIENTE");

            migrationBuilder.AddColumn<string>(
                name: "ENDERECO",
                table: "ENDERECO_CLIENTE",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }
    }
}

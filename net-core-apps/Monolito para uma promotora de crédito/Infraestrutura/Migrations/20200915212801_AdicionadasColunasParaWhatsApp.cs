using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AdicionadasColunasParaWhatsApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "TELEFONES_LOJAS",
                newName: "TELEFONE");

            migrationBuilder.AddColumn<string>(
                name: "MENSAGEM_APRESENTACAO",
                table: "TELEFONES_LOJAS",
                maxLength: 8000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "POSSUI_CONTA_WHATSAPP",
                table: "TELEFONES_LOJAS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MENSAGEM_APRESENTACAO",
                table: "LOJAS",
                maxLength: 8000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DESEJA_CONTATO_WHATSAPP",
                table: "LEADS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LINK_CONTATO_WHATSAPP_LOJA",
                table: "LEADS",
                maxLength: 8000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MENSAGEM_APRESENTACAO",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropColumn(
                name: "POSSUI_CONTA_WHATSAPP",
                table: "TELEFONES_LOJAS");

            migrationBuilder.DropColumn(
                name: "MENSAGEM_APRESENTACAO",
                table: "LOJAS");

            migrationBuilder.DropColumn(
                name: "DESEJA_CONTATO_WHATSAPP",
                table: "LEADS");

            migrationBuilder.DropColumn(
                name: "LINK_CONTATO_WHATSAPP_LOJA",
                table: "LEADS");

            migrationBuilder.RenameColumn(
                name: "TELEFONE",
                table: "TELEFONES_LOJAS",
                newName: "Telefone");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddNomeInstituidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NOME_INSTITUIDOR",
                table: "RENDIMENTO_CLIENTE_SIAPE",
                unicode: false,
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NOME_INSTITUIDOR",
                table: "RENDIMENTO_CLIENTE_SIAPE");
        }
    }
}

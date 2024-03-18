using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddUsuarioAdministradorExclusivoToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ADMINISTRADOR",
                table: "USUARIO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EXCLUSIVO_TENANT",
                table: "USUARIO",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADMINISTRADOR",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "EXCLUSIVO_TENANT",
                table: "USUARIO");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class CrudTipoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS");

            migrationBuilder.RenameTable(
                name: "PRODUTOS",
                newName: "TIPOS_OPERACAO");

            migrationBuilder.AlterColumn<string>(
                name: "SIGLA",
                table: "TIPOS_OPERACAO",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIPOS_OPERACAO",
                table: "TIPOS_OPERACAO",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "TiposOperacao",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Sigla = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposOperacao", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposOperacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIPOS_OPERACAO",
                table: "TIPOS_OPERACAO");

            migrationBuilder.RenameTable(
                name: "TIPOS_OPERACAO",
                newName: "PRODUTOS");

            migrationBuilder.AlterColumn<string>(
                name: "SIGLA",
                table: "PRODUTOS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUTOS",
                table: "PRODUTOS",
                column: "ID");
        }
    }
}

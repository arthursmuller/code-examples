using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InclusaoBancos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BANCO",
                columns: table => new
                {
                    ID_BANCO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(maxLength: 60, nullable: false),
                    CNPJ = table.Column<string>(maxLength: 14, nullable: true),
                    CODIGO = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANCO", x => x.ID_BANCO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BANCO_CODIGO",
                table: "BANCO",
                column: "CODIGO",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BANCO");
        }
    }
}

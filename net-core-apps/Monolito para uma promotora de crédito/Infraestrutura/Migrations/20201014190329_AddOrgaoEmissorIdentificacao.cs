using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddOrgaoEmissorIdentificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ORGAO_EMISSOR_IDENTIFICACAO",
                columns: table => new
                {
                    ID_ORGAO_EMISSOR_IDENTIFICACAO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    CODIGO = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    DESCRICAO = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGAO_EMISSOR_IDENTIFICACAO", x => x.ID_ORGAO_EMISSOR_IDENTIFICACAO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORGAO_EMISSOR_IDENTIFICACAO");
        }
    }
}

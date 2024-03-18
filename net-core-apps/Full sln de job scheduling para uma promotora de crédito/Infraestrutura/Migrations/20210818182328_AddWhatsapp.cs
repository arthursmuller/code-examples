using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class AddWhatsapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WHATSAPP_MENSAGEM",
                columns: table => new
                {
                    ID_WHATSAPP_MENSAGEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ID_TEMPLATE = table.Column<Guid>(nullable: false),
                    DDD = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    TELEFONE = table.Column<string>(unicode: false, maxLength: 9, nullable: false),
                    MENSAGEM_ENVIO = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    MENSAGEM_RETORNO_ERRO = table.Column<string>(unicode: false, maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHATSAPP_MENSAGEM", x => x.ID_WHATSAPP_MENSAGEM);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WHATSAPP_MENSAGEM");
        }
    }
}

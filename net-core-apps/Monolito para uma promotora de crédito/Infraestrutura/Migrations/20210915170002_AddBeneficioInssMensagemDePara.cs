using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddBeneficioInssMensagemDePara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BENEFICIO_INSS_MENSAGEM_DE_PARA",
                columns: table => new
                {
                    ID_BENEFICIO_INSS_MENSAGEM_DE_PARA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO_ORIGINAL = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MENSAGEM_ORIGINAL = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: false),
                    MENSAGEM_TRATADA = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BENEFICIO_INSS_MENSAGEM_DE_PARA", x => x.ID_BENEFICIO_INSS_MENSAGEM_DE_PARA);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BENEFICIO_INSS_MENSAGEM_DE_PARA_CODIGO_ORIGINAL",
                table: "BENEFICIO_INSS_MENSAGEM_DE_PARA",
                column: "CODIGO_ORIGINAL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BENEFICIO_INSS_MENSAGEM_DE_PARA");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddRegistroClubeBeneficioUpdateCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OPERACAO_ATIVA",
                table: "CLIENTE",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "REGISTRO_CLUBE_BENEFICIOS",
                columns: table => new
                {
                    ID_REGISTRO_CLUBE_BENEFICIOS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    DATA_ACESSO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_CLUBE_BENEFICIOS", x => x.ID_REGISTRO_CLUBE_BENEFICIOS);
                    table.ForeignKey(
                        name: "FK_REGISTRO_CLUBE_BENEFICIOS_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                });

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_CLUBE_BENEFICIOS_ID_CLIENTE",
                table: "REGISTRO_CLUBE_BENEFICIOS",
                column: "ID_CLIENTE",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTRO_CLUBE_BENEFICIOS");

            migrationBuilder.DropColumn(
                name: "OPERACAO_ATIVA",
                table: "CLIENTE");
        }
    }
}

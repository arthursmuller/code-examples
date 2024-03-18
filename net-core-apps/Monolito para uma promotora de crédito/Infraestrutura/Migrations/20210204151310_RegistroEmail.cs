using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class RegistroEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "TEMPLATE_EMAIL_TIPO",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.CreateTable(
                name: "REGISTRO_EMAIL",
                columns: table => new
                {
                    ID_REGISTRO_EMAIL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ATUALIZACAO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(nullable: false),
                    FINALIDADE = table.Column<int>(nullable: false),
                    DESTINATARIOS = table.Column<string>(nullable: false),
                    ID_USUARIO = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_EMAIL", x => x.ID_REGISTRO_EMAIL);
                    table.ForeignKey(
                        name: "FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE",
                        column: x => x.FINALIDADE,
                        principalTable: "TEMPLATE_EMAIL_FINALIDADE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REGISTRO_EMAIL_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTRO_EMAIL");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "TEMPLATE_EMAIL_TIPO",
                newName: "DataAtualizacao");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddTipoFuncionaCargoMarinhaAeronautica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AERONAUTICA_CARGO",
                columns: table => new
                {
                    ID_AERONAUTICA_CARGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    SIGLA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AERONAUTICA_CARGO", x => x.ID_AERONAUTICA_CARGO);
                });

            migrationBuilder.CreateTable(
                name: "AERONAUTICA_TIPO_FUNCIONAL",
                columns: table => new
                {
                    ID_AERONAUTICA_TIPO_FUNCIONAL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SIGLA = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AERONAUTICA_TIPO_FUNCIONAL", x => x.ID_AERONAUTICA_TIPO_FUNCIONAL);
                });

            migrationBuilder.CreateTable(
                name: "MARINHA_CARGO",
                columns: table => new
                {
                    ID_MARINHA_CARGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    SIGLA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARINHA_CARGO", x => x.ID_MARINHA_CARGO);
                });

            migrationBuilder.CreateTable(
                name: "MARINHA_TIPO_FUNCIONAL",
                columns: table => new
                {
                    ID_MARINHA_TIPO_FUNCIONAL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SIGLA = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARINHA_TIPO_FUNCIONAL", x => x.ID_MARINHA_TIPO_FUNCIONAL);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AERONAUTICA_CARGO");

            migrationBuilder.DropTable(
                name: "AERONAUTICA_TIPO_FUNCIONAL");

            migrationBuilder.DropTable(
                name: "MARINHA_CARGO");

            migrationBuilder.DropTable(
                name: "MARINHA_TIPO_FUNCIONAL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddRendimentoClienteMarinhaEAeronautica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RENDIMENTO_CLIENTE_AERONAUTICA",
                columns: table => new
                {
                    ID_RENDIMENTO_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_AERONAUTICA_TIPO_FUNCIONAL = table.Column<int>(type: "int", nullable: false),
                    ID_AERONAUTICA_CARGO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENDIMENTO_CLIENTE_AERONAUTICA", x => x.ID_RENDIMENTO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_AERONAUTICA_AERONAUTICA_CARGO_ID_AERONAUTICA_CARGO",
                        column: x => x.ID_AERONAUTICA_CARGO,
                        principalTable: "AERONAUTICA_CARGO",
                        principalColumn: "ID_AERONAUTICA_CARGO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_AERONAUTICA_AERONAUTICA_TIPO_FUNCIONAL_ID_AERONAUTICA_TIPO_FUNCIONAL",
                        column: x => x.ID_AERONAUTICA_TIPO_FUNCIONAL,
                        principalTable: "AERONAUTICA_TIPO_FUNCIONAL",
                        principalColumn: "ID_AERONAUTICA_TIPO_FUNCIONAL");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_AERONAUTICA_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                        column: x => x.ID_RENDIMENTO_CLIENTE,
                        principalTable: "RENDIMENTO_CLIENTE",
                        principalColumn: "ID_RENDIMENTO_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RENDIMENTO_CLIENTE_MARINHA",
                columns: table => new
                {
                    ID_RENDIMENTO_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ID_MARINHA_TIPO_FUNCIONAL = table.Column<int>(type: "int", nullable: false),
                    ID_MARINHA_CARGO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENDIMENTO_CLIENTE_MARINHA", x => x.ID_RENDIMENTO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_MARINHA_MARINHA_CARGO_ID_MARINHA_CARGO",
                        column: x => x.ID_MARINHA_CARGO,
                        principalTable: "MARINHA_CARGO",
                        principalColumn: "ID_MARINHA_CARGO");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_MARINHA_MARINHA_TIPO_FUNCIONAL_ID_MARINHA_TIPO_FUNCIONAL",
                        column: x => x.ID_MARINHA_TIPO_FUNCIONAL,
                        principalTable: "MARINHA_TIPO_FUNCIONAL",
                        principalColumn: "ID_MARINHA_TIPO_FUNCIONAL");
                    table.ForeignKey(
                        name: "FK_RENDIMENTO_CLIENTE_MARINHA_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE",
                        column: x => x.ID_RENDIMENTO_CLIENTE,
                        principalTable: "RENDIMENTO_CLIENTE",
                        principalColumn: "ID_RENDIMENTO_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_AERONAUTICA_ID_AERONAUTICA_CARGO",
                table: "RENDIMENTO_CLIENTE_AERONAUTICA",
                column: "ID_AERONAUTICA_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_AERONAUTICA_ID_AERONAUTICA_TIPO_FUNCIONAL",
                table: "RENDIMENTO_CLIENTE_AERONAUTICA",
                column: "ID_AERONAUTICA_TIPO_FUNCIONAL");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_MARINHA_ID_MARINHA_CARGO",
                table: "RENDIMENTO_CLIENTE_MARINHA",
                column: "ID_MARINHA_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_RENDIMENTO_CLIENTE_MARINHA_ID_MARINHA_TIPO_FUNCIONAL",
                table: "RENDIMENTO_CLIENTE_MARINHA",
                column: "ID_MARINHA_TIPO_FUNCIONAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RENDIMENTO_CLIENTE_AERONAUTICA");

            migrationBuilder.DropTable(
                name: "RENDIMENTO_CLIENTE_MARINHA");
        }
    }
}

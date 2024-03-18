using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class PrimeiraMigraton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEADS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Convenio = table.Column<string>(maxLength: 20, nullable: true),
                    Latitude = table.Column<double>(maxLength: 10, nullable: false),
                    Longitude = table.Column<double>(maxLength: 10, nullable: false),
                    EnderecoIp = table.Column<string>(maxLength: 50, nullable: true),
                    OrigemRequisicaoPalavraChave = table.Column<string>(maxLength: 8000, nullable: true),
                    OrigemRequisicaoMidia = table.Column<string>(maxLength: 8000, nullable: true),
                    OrigemRequisicaoConteudo = table.Column<string>(maxLength: 8000, nullable: true),
                    OrigemRequisicaoTermo = table.Column<string>(maxLength: 8000, nullable: true),
                    OrigemRequisicaoCampanha = table.Column<string>(maxLength: 8000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEADS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOJAS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(maxLength: 10, nullable: false),
                    Longitude = table.Column<double>(maxLength: 10, nullable: false),
                    Endereco = table.Column<string>(maxLength: 255, nullable: true),
                    Cidade = table.Column<string>(maxLength: 100, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true),
                    Bairro = table.Column<string>(maxLength: 100, nullable: true),
                    Cep = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOJAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    CPF = table.Column<string>(maxLength: 50, nullable: true),
                    Senha = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TELEFONES_LOJAS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefone = table.Column<string>(maxLength: 100, nullable: true),
                    ID_LOJA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TELEFONES_LOJAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TELEFONES_LOJAS_LOJAS_ID_LOJA",
                        column: x => x.ID_LOJA,
                        principalTable: "LOJAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TELEFONES_LOJAS_ID_LOJA",
                table: "TELEFONES_LOJAS",
                column: "ID_LOJA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEADS");

            migrationBuilder.DropTable(
                name: "TELEFONES_LOJAS");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "LOJAS");
        }
    }
}

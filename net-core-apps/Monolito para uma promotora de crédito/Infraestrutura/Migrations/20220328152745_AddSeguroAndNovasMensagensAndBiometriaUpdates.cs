using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class AddSeguroAndNovasMensagensAndBiometriaUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRINCIPAL",
                table: "TELEFONE_CLIENTE");

            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "CLIENTE");

            migrationBuilder.AddColumn<bool>(
                name: "EMAIL_CONFIRMADO",
                table: "USUARIO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CODIGO",
                table: "TIPO_DOCUMENTO",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldUnicode: false,
                oldMaxLength: 7);

            migrationBuilder.AddColumn<bool>(
                name: "TIPO_DOCUMENTO_IDENTIFICACAO_PESSOAL",
                table: "TIPO_DOCUMENTO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CODIGO_ORIGEM",
                table: "REGISTRO_EMAIL",
                type: "int",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "CONTA_CLIENTE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ID_CONJUGE",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BIOMETRIA_SITUACAO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIOMETRIA_SITUACAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRO_BIOMETRIA_UNICO",
                columns: table => new
                {
                    ID_REGISTRO_BIOMETRIA_UNICO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    CODIGO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATA_ENVIO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATA_RETORNO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCORE = table.Column<int>(type: "int", nullable: false),
                    LIVENESS = table.Column<bool>(type: "bit", nullable: false),
                    FACE_MATCH = table.Column<bool>(type: "bit", nullable: false),
                    POSSUI_BIOMETRIA = table.Column<bool>(type: "bit", nullable: false),
                    CODIGO_SITUACAO_BIOMETRIA = table.Column<int>(type: "int", nullable: false),
                    CODIGO_ERRO = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_BIOMETRIA_UNICO", x => x.ID_REGISTRO_BIOMETRIA_UNICO);
                    table.ForeignKey(
                        name: "FK_REGISTRO_BIOMETRIA_UNICO_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PARENTESCO",
                columns: table => new
                {
                    ID_SEGURO_PARENTESCO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PARENTESCO", x => x.ID_SEGURO_PARENTESCO);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PRODUTO",
                columns: table => new
                {
                    ID_SEGURO_PRODUTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NOME = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DATA_INICIO_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_FIM_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR_PREMIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ID_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PRODUTO", x => x.ID_SEGURO_PRODUTO);
                    table.ForeignKey(
                        name: "FK_SEGURO_PRODUTO_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID_PRODUTO");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PROFISSAO",
                columns: table => new
                {
                    ID_SEGURO_PROFISSAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PROFISSAO", x => x.ID_SEGURO_PROFISSAO);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE_SMS",
                columns: table => new
                {
                    ID_TEMPLATE_SMS = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CONTEUDO = table.Column<string>(type: "varchar(160)", unicode: false, maxLength: 160, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_SMS", x => x.ID_TEMPLATE_SMS);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE_TORPEDO_VOZ",
                columns: table => new
                {
                    ID_TEMPLATE_TORPEDO_VOZ = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CONTEUDO = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_TORPEDO_VOZ", x => x.ID_TEMPLATE_TORPEDO_VOZ);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE_WHATSAPP",
                columns: table => new
                {
                    ID_TEMPLATE_WHATSAPP = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    GUID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE_WHATSAPP", x => x.ID_TEMPLATE_WHATSAPP);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_REGIME_CASAMENTO",
                columns: table => new
                {
                    ID_TIPO_REGIME_CASAMENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_REGIME_CASAMENTO", x => x.ID_TIPO_REGIME_CASAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_CONFIRMACAO_EMAIL",
                columns: table => new
                {
                    ID_USUARIO_CONFIRMACAO_EMAIL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TOKEN = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DATA_REQUISICAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    VALIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_CONFIRMACAO_EMAIL", x => x.ID_USUARIO_CONFIRMACAO_EMAIL);
                    table.ForeignKey(
                        name: "FK_USUARIO_CONFIRMACAO_EMAIL_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "BIOMETRIA_CLIENTE",
                columns: table => new
                {
                    ID_BIOMETRIA_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    DATA_ENVIO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SCORE = table.Column<int>(type: "int", nullable: false),
                    VALIDO = table.Column<bool>(type: "bit", nullable: false),
                    ID_BIOMETRIA_SITUACAO = table.Column<int>(type: "int", nullable: false),
                    DATA_RETORNO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRegistroBiometriaUnico = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIOMETRIA_CLIENTE", x => x.ID_BIOMETRIA_CLIENTE);
                    table.ForeignKey(
                        name: "FK_BIOMETRIA_CLIENTE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE");
                    table.ForeignKey(
                        name: "FK_BIOMETRIA_CLIENTE_REGISTRO_BIOMETRIA_UNICO_IdRegistroBiometriaUnico",
                        column: x => x.IdRegistroBiometriaUnico,
                        principalTable: "REGISTRO_BIOMETRIA_UNICO",
                        principalColumn: "ID_REGISTRO_BIOMETRIA_UNICO");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PARENTESCO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_PARENTESCO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    ID_SEGURO_PARENTESCO_BEM = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PARENTESCO_ICATU", x => x.ID_SEGURO_PARENTESCO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_PARENTESCO_ICATU_SEGURO_PARENTESCO_ID_SEGURO_PARENTESCO_BEM",
                        column: x => x.ID_SEGURO_PARENTESCO_BEM,
                        principalTable: "SEGURO_PARENTESCO",
                        principalColumn: "ID_SEGURO_PARENTESCO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_COBERTURA",
                columns: table => new
                {
                    ID_SEGURO_COBERTURA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO_COBERTURA = table.Column<int>(type: "int", nullable: false),
                    TIPO = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    VALOR_CAPITAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VALOR_PREMIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIPO_PROPONENTE = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    ID_SEGURO_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_COBERTURA", x => x.ID_SEGURO_COBERTURA);
                    table.ForeignKey(
                        name: "FK_SEGURO_COBERTURA_SEGURO_PRODUTO_ID_SEGURO_PRODUTO",
                        column: x => x.ID_SEGURO_PRODUTO,
                        principalTable: "SEGURO_PRODUTO",
                        principalColumn: "ID_SEGURO_PRODUTO");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PRODUTO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_PRODUTO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRUPO_APOLICE = table.Column<int>(type: "int", nullable: false),
                    DATA_INICIO_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_FIM_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    MODULO = table.Column<int>(type: "int", nullable: false),
                    ID_SEGURO_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PRODUTO_ICATU", x => x.ID_SEGURO_PRODUTO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_PRODUTO_ICATU_SEGURO_PRODUTO_ID_SEGURO_PRODUTO",
                        column: x => x.ID_SEGURO_PRODUTO,
                        principalTable: "SEGURO_PRODUTO",
                        principalColumn: "ID_SEGURO_PRODUTO");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PROPOSTA",
                columns: table => new
                {
                    ID_SEGURO_PROPOSTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA_ASSINATURA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_INICIO_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_FIM_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_PROTOCOLO = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_VENCIMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR_PREMIO_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VALOR_PRIMEIRO_PREMIO_PAGO = table.Column<bool>(type: "bit", nullable: false),
                    NUMERO_PROPOSTA_ICATU = table.Column<long>(type: "bigint", nullable: false),
                    ID_SEGURO_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PROPOSTA", x => x.ID_SEGURO_PROPOSTA);
                    table.ForeignKey(
                        name: "FK_SEGURO_PROPOSTA_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SEGURO_PROPOSTA_SEGURO_PRODUTO_ID_SEGURO_PRODUTO",
                        column: x => x.ID_SEGURO_PRODUTO,
                        principalTable: "SEGURO_PRODUTO",
                        principalColumn: "ID_SEGURO_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PROFISSAO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_PROFISSAO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO = table.Column<int>(type: "int", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    ID_SEGURO_PROFISSAO_BEM = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PROFISSAO_ICATU", x => x.ID_SEGURO_PROFISSAO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_PROFISSAO_ICATU_SEGURO_PROFISSAO_ID_SEGURO_PROFISSAO_BEM",
                        column: x => x.ID_SEGURO_PROFISSAO_BEM,
                        principalTable: "SEGURO_PROFISSAO",
                        principalColumn: "ID_SEGURO_PROFISSAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRO_SMS",
                columns: table => new
                {
                    ID_REGISTRO_SMS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TEMPLATE_SMS = table.Column<int>(type: "int", nullable: false),
                    NUMERO_TELEFONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    MENSAGEM = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true),
                    CODIGO_ORIGEM = table.Column<int>(type: "int", unicode: false, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_SMS", x => x.ID_REGISTRO_SMS);
                    table.ForeignKey(
                        name: "FK_REGISTRO_SMS_TEMPLATE_SMS_ID_TEMPLATE_SMS",
                        column: x => x.ID_TEMPLATE_SMS,
                        principalTable: "TEMPLATE_SMS",
                        principalColumn: "ID_TEMPLATE_SMS");
                    table.ForeignKey(
                        name: "FK_REGISTRO_SMS_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRO_TORPEDO_VOZ",
                columns: table => new
                {
                    ID_REGISTRO_TORPEDO_VOZ = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NUMERO_TELEFONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    MENSAGEM = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ID_TEMPLATE_TORPEDO_VOZ = table.Column<int>(type: "int", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true),
                    CODIGO_ORIGEM = table.Column<int>(type: "int", unicode: false, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_TORPEDO_VOZ", x => x.ID_REGISTRO_TORPEDO_VOZ);
                    table.ForeignKey(
                        name: "FK_REGISTRO_TORPEDO_VOZ_TEMPLATE_TORPEDO_VOZ_ID_TEMPLATE_TORPEDO_VOZ",
                        column: x => x.ID_TEMPLATE_TORPEDO_VOZ,
                        principalTable: "TEMPLATE_TORPEDO_VOZ",
                        principalColumn: "ID_TEMPLATE_TORPEDO_VOZ");
                    table.ForeignKey(
                        name: "FK_REGISTRO_TORPEDO_VOZ_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRO_WHATSAPP",
                columns: table => new
                {
                    ID_REGISTRO_WHATSAPP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_TEMPLATE_WHATSAPP = table.Column<int>(type: "int", nullable: false),
                    NUMERO_TELEFONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    PARAMETROS_MENSAGEM = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true),
                    CODIGO_ORIGEM = table.Column<int>(type: "int", unicode: false, nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_WHATSAPP", x => x.ID_REGISTRO_WHATSAPP);
                    table.ForeignKey(
                        name: "FK_REGISTRO_WHATSAPP_TEMPLATE_WHATSAPP_ID_TEMPLATE_WHATSAPP",
                        column: x => x.ID_TEMPLATE_WHATSAPP,
                        principalTable: "TEMPLATE_WHATSAPP",
                        principalColumn: "ID_TEMPLATE_WHATSAPP");
                    table.ForeignKey(
                        name: "FK_REGISTRO_WHATSAPP_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CONJUGE",
                columns: table => new
                {
                    ID_CONJUGE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "date", nullable: true),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: true),
                    ID_GENERO = table.Column<int>(type: "int", nullable: true),
                    ID_TIPO_REGIME_CASAMENTO = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONJUGE", x => x.ID_CONJUGE);
                    table.ForeignKey(
                        name: "FK_CONJUGE_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CONJUGE_GENERO_ID_GENERO",
                        column: x => x.ID_GENERO,
                        principalTable: "GENERO",
                        principalColumn: "ID_GENERO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CONJUGE_TIPO_REGIME_CASAMENTO_ID_TIPO_REGIME_CASAMENTO",
                        column: x => x.ID_TIPO_REGIME_CASAMENTO,
                        principalTable: "TIPO_REGIME_CASAMENTO",
                        principalColumn: "ID_TIPO_REGIME_CASAMENTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_REGIME_CASAMENTO_BEM",
                columns: table => new
                {
                    ID_TIPO_REGIME_CASAMENTO_BEM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    ID_TIPO_REGIME_CASAMENTO = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_REGIME_CASAMENTO_BEM", x => x.ID_TIPO_REGIME_CASAMENTO_BEM);
                    table.ForeignKey(
                        name: "FK_TIPO_REGIME_CASAMENTO_BEM_TIPO_REGIME_CASAMENTO_ID_TIPO_REGIME_CASAMENTO",
                        column: x => x.ID_TIPO_REGIME_CASAMENTO,
                        principalTable: "TIPO_REGIME_CASAMENTO",
                        principalColumn: "ID_TIPO_REGIME_CASAMENTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_COBERTURA_ICATU",
                columns: table => new
                {
                    ID_SEGURO_COBERTURA_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO_COBERTURA = table.Column<int>(type: "int", nullable: false),
                    TIPO = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    VALOR_CAPITAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VALOR_PREMIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIPO_PROPONENTE = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    ID_SEGURO_PRODUTO_ICATU = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_COBERTURA_ICATU", x => x.ID_SEGURO_COBERTURA_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_COBERTURA_ICATU_SEGURO_PRODUTO_ICATU_ID_SEGURO_PRODUTO_ICATU",
                        column: x => x.ID_SEGURO_PRODUTO_ICATU,
                        principalTable: "SEGURO_PRODUTO_ICATU",
                        principalColumn: "ID_SEGURO_PRODUTO_ICATU");
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_CLIENTE_ICATU",
                columns: table => new
                {
                    ID_SEGURO_CLIENTE_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NOME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NACIONALIDADE = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PPE = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    RENDA_MENSAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RESIDENTE_PAIS = table.Column<bool>(type: "bit", nullable: false),
                    RELACIONAMENTO_ELETRONICO = table.Column<bool>(type: "bit", nullable: false),
                    APOSENTADO = table.Column<bool>(type: "bit", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: true),
                    ID_ESTADO_CIVIL = table.Column<int>(type: "int", nullable: true),
                    ID_GENERO = table.Column<int>(type: "int", nullable: true),
                    ID_PROFISSAO_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_CLIENTE_ICATU", x => x.ID_SEGURO_CLIENTE_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_CLIENTE_ICATU_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_CLIENTE_ICATU_ESTADO_CIVIL_ID_ESTADO_CIVIL",
                        column: x => x.ID_ESTADO_CIVIL,
                        principalTable: "ESTADO_CIVIL",
                        principalColumn: "ID_ESTADO_CIVIL",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_CLIENTE_ICATU_GENERO_ID_GENERO",
                        column: x => x.ID_GENERO,
                        principalTable: "GENERO",
                        principalColumn: "ID_GENERO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_CLIENTE_ICATU_SEGURO_PROFISSAO_ICATU_ID_PROFISSAO_ICATU",
                        column: x => x.ID_PROFISSAO_ICATU,
                        principalTable: "SEGURO_PROFISSAO_ICATU",
                        principalColumn: "ID_SEGURO_PROFISSAO_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_BENEFICIARIO",
                columns: table => new
                {
                    ID_SEGURO_BENEFICIARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CPF = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR_PERCENTUAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ID_SEGURO_PROPOSTA = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_CLIENTE = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_PARENTESCO = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_BENEFICIARIO", x => x.ID_SEGURO_BENEFICIARIO);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE",
                        column: x => x.ID_SEGURO_CLIENTE,
                        principalTable: "SEGURO_CLIENTE_ICATU",
                        principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_SEGURO_PARENTESCO_ID_SEGURO_PARENTESCO",
                        column: x => x.ID_SEGURO_PARENTESCO,
                        principalTable: "SEGURO_PARENTESCO",
                        principalColumn: "ID_SEGURO_PARENTESCO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_SEGURO_PROPOSTA_ID_SEGURO_PROPOSTA",
                        column: x => x.ID_SEGURO_PROPOSTA,
                        principalTable: "SEGURO_PROPOSTA",
                        principalColumn: "ID_SEGURO_PROPOSTA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_CLIENTE_TELEFONE",
                columns: table => new
                {
                    ID_SEGURO_CLIENTE_TELEFONE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    FONE = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true),
                    DELETADO = table.Column<bool>(type: "bit", nullable: false),
                    PRINCIPAL = table.Column<bool>(type: "bit", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_CLIENTE_TELEFONE", x => x.ID_SEGURO_CLIENTE_TELEFONE);
                    table.ForeignKey(
                        name: "FK_SEGURO_CLIENTE_TELEFONE_SEGURO_CLIENTE_ICATU_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "SEGURO_CLIENTE_ICATU",
                        principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_ENDERECO_CLIENTE",
                columns: table => new
                {
                    ID_SEGURO_ENDERECO_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BAIRRO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CEP = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    COMPLEMENTO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LOGRADOURO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    NUMERO = table.Column<int>(type: "int", nullable: false),
                    ID_MUNICIPIO = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_CLIENTE_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_ENDERECO_CLIENTE", x => x.ID_SEGURO_ENDERECO_CLIENTE);
                    table.ForeignKey(
                        name: "FK_SEGURO_ENDERECO_CLIENTE_MUNICIPIO_ID_MUNICIPIO",
                        column: x => x.ID_MUNICIPIO,
                        principalTable: "MUNICIPIO",
                        principalColumn: "ID_MUNICIPIO");
                    table.ForeignKey(
                        name: "FK_SEGURO_ENDERECO_CLIENTE_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU",
                        column: x => x.ID_SEGURO_CLIENTE_ICATU,
                        principalTable: "SEGURO_CLIENTE_ICATU",
                        principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_PROPOSTA_ICATU",
                columns: table => new
                {
                    ID_SEGURO_PROPOSTA_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA_ASSINATURA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_INICIO_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_FIM_VIGENCIA = table.Column<DateTime>(type: "date", nullable: false),
                    DATA_PROTOCOLO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR_PREMIO_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VALOR_PRIMEIRO_PREMIO_PAGO = table.Column<bool>(type: "bit", nullable: false),
                    NUMERO_PROPOSTA_ICATU = table.Column<long>(type: "bigint", nullable: false),
                    ID_SEGURO_PROPOSTA = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_CLIENTE_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_PROPOSTA_ICATU", x => x.ID_SEGURO_PROPOSTA_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU",
                        column: x => x.ID_SEGURO_CLIENTE_ICATU,
                        principalTable: "SEGURO_CLIENTE_ICATU",
                        principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_CLIENTE_ICATU",
                        column: x => x.ID_SEGURO_CLIENTE_ICATU,
                        principalTable: "SEGURO_PROPOSTA",
                        principalColumn: "ID_SEGURO_PROPOSTA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_BENEFICIARIO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_BENEFICIARIO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CPF = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR_PERCENTUAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ID_SEGURO_PROPOSTA = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_CLIENTE_ICATU = table.Column<int>(type: "int", nullable: true),
                    ID_SEGURO_PARENTESCO_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_BENEFICIARIO_ICATU", x => x.ID_SEGURO_BENEFICIARIO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU",
                        column: x => x.ID_SEGURO_CLIENTE_ICATU,
                        principalTable: "SEGURO_CLIENTE_ICATU",
                        principalColumn: "ID_SEGURO_CLIENTE_ICATU",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_PARENTESCO_ICATU_ID_SEGURO_PARENTESCO_ICATU",
                        column: x => x.ID_SEGURO_PARENTESCO_ICATU,
                        principalTable: "SEGURO_PARENTESCO_ICATU",
                        principalColumn: "ID_SEGURO_PARENTESCO_ICATU",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA",
                        column: x => x.ID_SEGURO_PROPOSTA,
                        principalTable: "SEGURO_PROPOSTA_ICATU",
                        principalColumn: "ID_SEGURO_PROPOSTA_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                columns: table => new
                {
                    ID_SEGURO_COBRANCA_PROPOSTA_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA_VENCIMENTO = table.Column<DateTime>(type: "date", nullable: false),
                    ID_CONVENIO = table.Column<int>(type: "int", nullable: false),
                    LINK_PAGAMENTO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ID_PEDIDO_PAGAMENTO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ID_LINK_PAGAMENTO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ID_COBRANCA = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IdSeguroPropostaIcatu = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_COBRANCA_PROPOSTA_ICATU", x => x.ID_SEGURO_COBRANCA_PROPOSTA_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_IdSeguroPropostaIcatu",
                        column: x => x.IdSeguroPropostaIcatu,
                        principalTable: "SEGURO_PROPOSTA_ICATU",
                        principalColumn: "ID_SEGURO_PROPOSTA_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_SITUACAO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_SITUACAO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STATUS = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    ID_SEGURO_PROPOSTA_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_SITUACAO_ICATU", x => x.ID_SEGURO_SITUACAO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_SITUACAO_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU",
                        column: x => x.ID_SEGURO_PROPOSTA_ICATU,
                        principalTable: "SEGURO_PROPOSTA_ICATU",
                        principalColumn: "ID_SEGURO_PROPOSTA_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                columns: table => new
                {
                    ID_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CARTAO = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ID_COBRANCA = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    QUATRO_ULTIMOS_DIGITOS_CARTAO = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    ID_SEGURO_COBRANCA_PROPOSTA_ICATU = table.Column<int>(type: "int", nullable: true),
                    USUARIO_ATUALIZACAO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU", x => x.ID_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU);
                    table.ForeignKey(
                        name: "FK_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU_SEGURO_COBRANCA_PROPOSTA_ICATU_ID_SEGURO_COBRANCA_PROPOSTA_ICATU",
                        column: x => x.ID_SEGURO_COBRANCA_PROPOSTA_ICATU,
                        principalTable: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                        principalColumn: "ID_SEGURO_COBRANCA_PROPOSTA_ICATU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE",
                column: "ID_ENDERECO_PRINCIPAL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE",
                column: "ID_ENDERECO_SECUNDARIO");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE",
                column: "ID_TELEFONE_PRINCIPAL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE",
                column: "ID_TELEFONE_SECUNDARIO");

            migrationBuilder.CreateIndex(
                name: "IX_BIOMETRIA_CLIENTE_ID_CLIENTE",
                table: "BIOMETRIA_CLIENTE",
                column: "ID_CLIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BIOMETRIA_CLIENTE_IdRegistroBiometriaUnico",
                table: "BIOMETRIA_CLIENTE",
                column: "IdRegistroBiometriaUnico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONJUGE_ID_CLIENTE",
                table: "CONJUGE",
                column: "ID_CLIENTE",
                unique: true,
                filter: "[ID_CLIENTE] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CONJUGE_ID_GENERO",
                table: "CONJUGE",
                column: "ID_GENERO");

            migrationBuilder.CreateIndex(
                name: "IX_CONJUGE_ID_TIPO_REGIME_CASAMENTO",
                table: "CONJUGE",
                column: "ID_TIPO_REGIME_CASAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_BIOMETRIA_UNICO_ID_CLIENTE",
                table: "REGISTRO_BIOMETRIA_UNICO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_SMS_ID_TEMPLATE_SMS",
                table: "REGISTRO_SMS",
                column: "ID_TEMPLATE_SMS");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_SMS_ID_USUARIO",
                table: "REGISTRO_SMS",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_TORPEDO_VOZ_ID_TEMPLATE_TORPEDO_VOZ",
                table: "REGISTRO_TORPEDO_VOZ",
                column: "ID_TEMPLATE_TORPEDO_VOZ");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_TORPEDO_VOZ_ID_USUARIO",
                table: "REGISTRO_TORPEDO_VOZ",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_WHATSAPP_ID_TEMPLATE_WHATSAPP",
                table: "REGISTRO_WHATSAPP",
                column: "ID_TEMPLATE_WHATSAPP");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_WHATSAPP_ID_USUARIO",
                table: "REGISTRO_WHATSAPP",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ID_SEGURO_CLIENTE",
                table: "SEGURO_BENEFICIARIO",
                column: "ID_SEGURO_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ID_SEGURO_PARENTESCO",
                table: "SEGURO_BENEFICIARIO",
                column: "ID_SEGURO_PARENTESCO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ID_SEGURO_PROPOSTA",
                table: "SEGURO_BENEFICIARIO",
                column: "ID_SEGURO_PROPOSTA");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_BENEFICIARIO_ICATU",
                column: "ID_SEGURO_CLIENTE_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_PARENTESCO_ICATU",
                table: "SEGURO_BENEFICIARIO_ICATU",
                column: "ID_SEGURO_PARENTESCO_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_PROPOSTA",
                table: "SEGURO_BENEFICIARIO_ICATU",
                column: "ID_SEGURO_PROPOSTA");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_CLIENTE",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_CLIENTE",
                unique: true,
                filter: "[ID_CLIENTE] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_ESTADO_CIVIL",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_ESTADO_CIVIL");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_GENERO",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_GENERO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_ICATU_ID_PROFISSAO_ICATU",
                table: "SEGURO_CLIENTE_ICATU",
                column: "ID_PROFISSAO_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_CLIENTE_TELEFONE_ID_CLIENTE",
                table: "SEGURO_CLIENTE_TELEFONE",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_COBERTURA_ID_SEGURO_PRODUTO",
                table: "SEGURO_COBERTURA",
                column: "ID_SEGURO_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_COBERTURA_ICATU_ID_SEGURO_PRODUTO_ICATU",
                table: "SEGURO_COBERTURA_ICATU",
                column: "ID_SEGURO_PRODUTO_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU_ID_SEGURO_COBRANCA_PROPOSTA_ICATU",
                table: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU",
                column: "ID_SEGURO_COBRANCA_PROPOSTA_ICATU",
                unique: true,
                filter: "[ID_SEGURO_COBRANCA_PROPOSTA_ICATU] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_COBRANCA_PROPOSTA_ICATU_IdSeguroPropostaIcatu",
                table: "SEGURO_COBRANCA_PROPOSTA_ICATU",
                column: "IdSeguroPropostaIcatu");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_ENDERECO_CLIENTE_ID_MUNICIPIO",
                table: "SEGURO_ENDERECO_CLIENTE",
                column: "ID_MUNICIPIO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_ENDERECO_CLIENTE_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_ENDERECO_CLIENTE",
                column: "ID_SEGURO_CLIENTE_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PARENTESCO_ICATU_ID_SEGURO_PARENTESCO_BEM",
                table: "SEGURO_PARENTESCO_ICATU",
                column: "ID_SEGURO_PARENTESCO_BEM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PRODUTO_ID_PRODUTO",
                table: "SEGURO_PRODUTO",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PRODUTO_ICATU_ID_SEGURO_PRODUTO",
                table: "SEGURO_PRODUTO_ICATU",
                column: "ID_SEGURO_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROFISSAO_ICATU_ID_SEGURO_PROFISSAO_BEM",
                table: "SEGURO_PROFISSAO_ICATU",
                column: "ID_SEGURO_PROFISSAO_BEM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ID_CLIENTE",
                table: "SEGURO_PROPOSTA",
                column: "ID_CLIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ID_SEGURO_PRODUTO",
                table: "SEGURO_PROPOSTA",
                column: "ID_SEGURO_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU",
                table: "SEGURO_PROPOSTA_ICATU",
                column: "ID_SEGURO_CLIENTE_ICATU",
                unique: true,
                filter: "[ID_SEGURO_CLIENTE_ICATU] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SEGURO_SITUACAO_ICATU_ID_SEGURO_PROPOSTA_ICATU",
                table: "SEGURO_SITUACAO_ICATU",
                column: "ID_SEGURO_PROPOSTA_ICATU");

            migrationBuilder.CreateIndex(
                name: "IX_TIPO_REGIME_CASAMENTO_BEM_ID_TIPO_REGIME_CASAMENTO",
                table: "TIPO_REGIME_CASAMENTO_BEM",
                column: "ID_TIPO_REGIME_CASAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_CONFIRMACAO_EMAIL_ID_USUARIO",
                table: "USUARIO_CONFIRMACAO_EMAIL",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_CONFIRMACAO_EMAIL_TOKEN",
                table: "USUARIO_CONFIRMACAO_EMAIL",
                column: "TOKEN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE",
                column: "ID_ENDERECO_PRINCIPAL",
                principalTable: "ENDERECO_CLIENTE",
                principalColumn: "ID_ENDERECO_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE",
                column: "ID_ENDERECO_SECUNDARIO",
                principalTable: "ENDERECO_CLIENTE",
                principalColumn: "ID_ENDERECO_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE",
                column: "ID_TELEFONE_PRINCIPAL",
                principalTable: "TELEFONE_CLIENTE",
                principalColumn: "ID_TELEFONE_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE",
                column: "ID_TELEFONE_SECUNDARIO",
                principalTable: "TELEFONE_CLIENTE",
                principalColumn: "ID_TELEFONE_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.DropTable(
                name: "BIOMETRIA_CLIENTE");

            migrationBuilder.DropTable(
                name: "BIOMETRIA_SITUACAO");

            migrationBuilder.DropTable(
                name: "CONJUGE");

            migrationBuilder.DropTable(
                name: "REGISTRO_SMS");

            migrationBuilder.DropTable(
                name: "REGISTRO_TORPEDO_VOZ");

            migrationBuilder.DropTable(
                name: "REGISTRO_WHATSAPP");

            migrationBuilder.DropTable(
                name: "SEGURO_BENEFICIARIO");

            migrationBuilder.DropTable(
                name: "SEGURO_BENEFICIARIO_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_CLIENTE_TELEFONE");

            migrationBuilder.DropTable(
                name: "SEGURO_COBERTURA");

            migrationBuilder.DropTable(
                name: "SEGURO_COBERTURA_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_ENDERECO_CLIENTE");

            migrationBuilder.DropTable(
                name: "SEGURO_SITUACAO_ICATU");

            migrationBuilder.DropTable(
                name: "TIPO_REGIME_CASAMENTO_BEM");

            migrationBuilder.DropTable(
                name: "USUARIO_CONFIRMACAO_EMAIL");

            migrationBuilder.DropTable(
                name: "REGISTRO_BIOMETRIA_UNICO");

            migrationBuilder.DropTable(
                name: "TEMPLATE_SMS");

            migrationBuilder.DropTable(
                name: "TEMPLATE_TORPEDO_VOZ");

            migrationBuilder.DropTable(
                name: "TEMPLATE_WHATSAPP");

            migrationBuilder.DropTable(
                name: "SEGURO_PARENTESCO_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_PRODUTO_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_COBRANCA_PROPOSTA_ICATU");

            migrationBuilder.DropTable(
                name: "TIPO_REGIME_CASAMENTO");

            migrationBuilder.DropTable(
                name: "SEGURO_PARENTESCO");

            migrationBuilder.DropTable(
                name: "SEGURO_PROPOSTA_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_CLIENTE_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_PROPOSTA");

            migrationBuilder.DropTable(
                name: "SEGURO_PROFISSAO_ICATU");

            migrationBuilder.DropTable(
                name: "SEGURO_PRODUTO");

            migrationBuilder.DropTable(
                name: "SEGURO_PROFISSAO");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropIndex(
                name: "IX_CLIENTE_ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "EMAIL_CONFIRMADO",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "TIPO_DOCUMENTO_IDENTIFICACAO_PESSOAL",
                table: "TIPO_DOCUMENTO");

            migrationBuilder.DropColumn(
                name: "CODIGO_ORIGEM",
                table: "REGISTRO_EMAIL");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "CONTA_CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_CONJUGE",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_ENDERECO_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_ENDERECO_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_TELEFONE_PRINCIPAL",
                table: "CLIENTE");

            migrationBuilder.DropColumn(
                name: "ID_TELEFONE_SECUNDARIO",
                table: "CLIENTE");

            migrationBuilder.AlterColumn<string>(
                name: "CODIGO",
                table: "TIPO_DOCUMENTO",
                type: "varchar(7)",
                unicode: false,
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30);

            migrationBuilder.AddColumn<bool>(
                name: "PRINCIPAL",
                table: "TELEFONE_CLIENTE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "CLIENTE",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}

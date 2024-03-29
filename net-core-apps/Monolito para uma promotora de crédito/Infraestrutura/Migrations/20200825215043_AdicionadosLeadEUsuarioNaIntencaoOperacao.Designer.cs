﻿// <auto-generated />
using System;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repositorio.Migrations
{
    [DbContext(typeof(PlataformaClienteContexto))]
    [Migration("20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao")]
    partial class AdicionadosLeadEUsuarioNaIntencaoOperacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.AnexoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdUsuario");

                    b.ToTable("ANEXOS");
                });

            modelBuilder.Entity("Dominio.ConvenioDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnName("CODIGO")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("CONVENIOS");
                });

            modelBuilder.Entity("Dominio.IntencaoOperacaoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdLead")
                        .HasColumnName("ID_LEAD")
                        .HasColumnType("int");

                    b.Property<int?>("IdLoja")
                        .HasColumnName("ID_LOJA")
                        .HasColumnType("int");

                    b.Property<int>("IdSituacao")
                        .HasColumnName("ID_SITUACAO")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoOperacao")
                        .HasColumnName("ID_TIPO_OPERACAO")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuario")
                        .HasColumnName("ID_USUARIO")
                        .HasColumnType("int");

                    b.Property<decimal>("Prestacao")
                        .HasColumnName("PRESTACAO")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxaAno")
                        .HasColumnName("TAXA_ANO")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxaMes")
                        .HasColumnName("TAXA_MES")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorAuxilioFinanceiro")
                        .HasColumnName("VALOR_AUXILIO_FINANCEIRO")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorFinanciado")
                        .HasColumnName("VALOR_FINANCIADO")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("IdLead");

                    b.HasIndex("IdLoja");

                    b.HasIndex("IdSituacao");

                    b.HasIndex("IdTipoOperacao");

                    b.HasIndex("IdUsuario");

                    b.ToTable("INTENCOES_OPERACAO");
                });

            modelBuilder.Entity("Dominio.LeadDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Convenio")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasMaxLength(10);

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasMaxLength(10);

                    b.Property<string>("OrigemRequisicaoCampanha")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(8000);

                    b.Property<string>("OrigemRequisicaoConteudo")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(8000);

                    b.Property<string>("OrigemRequisicaoMidia")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(8000);

                    b.Property<string>("OrigemRequisicaoPalavraChave")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(8000);

                    b.Property<string>("OrigemRequisicaoTermo")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(8000);

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("LEADS");
                });

            modelBuilder.Entity("Dominio.LojaDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasMaxLength(10);

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasMaxLength(10);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("LOJAS");
                });

            modelBuilder.Entity("Dominio.ParametroOperacaoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdConvenio")
                        .HasColumnName("ID_CONVENIO")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoOperacao")
                        .HasColumnName("ID_TIPO_OPERACAO")
                        .HasColumnType("int");

                    b.Property<string>("InstituicaoFinanceira")
                        .HasColumnName("INSTITUICAO_FINANCEIRA")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("QuantidadeParcelas")
                        .HasColumnName("QUANTIDADE_PARCELAS")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TaxaPlano")
                        .HasColumnName("TAXA_PLANO")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("TentativaRetencao")
                        .HasColumnName("TENTATIVA_RETENCAO")
                        .HasColumnType("bit")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("IdConvenio");

                    b.HasIndex("IdTipoOperacao");

                    b.ToTable("PARAMETROS_OPERACAO");
                });

            modelBuilder.Entity("Dominio.ProdutoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("SIGLA")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("ID");

                    b.ToTable("PRODUTOS");
                });

            modelBuilder.Entity("Dominio.SituacaoIntencaoOperacaoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("SITUACOES_INTENCAO_OPERACAO");
                });

            modelBuilder.Entity("Dominio.TelefoneLojaDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdLoja")
                        .HasColumnName("ID_LOJA")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("IdLoja");

                    b.ToTable("TELEFONES_LOJAS");
                });

            modelBuilder.Entity("Dominio.TipoOperacaoDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("SIGLA")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("ID");

                    b.ToTable("TIPOS_OPERACAO");
                });

            modelBuilder.Entity("Dominio.UsuarioDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("USUARIOS");
                });

            modelBuilder.Entity("Dominio.AnexoDominio", b =>
                {
                    b.HasOne("Dominio.UsuarioDominio", "Usuario")
                        .WithMany("Anexos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.IntencaoOperacaoDominio", b =>
                {
                    b.HasOne("Dominio.LeadDominio", "Lead")
                        .WithMany("IntencoesOperacao")
                        .HasForeignKey("IdLead")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Dominio.LojaDominio", "Loja")
                        .WithMany("IntencoesOperacao")
                        .HasForeignKey("IdLoja")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Dominio.SituacaoIntencaoOperacaoDominio", "Situacao")
                        .WithMany("IntencoesOperacao")
                        .HasForeignKey("IdSituacao")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dominio.TipoOperacaoDominio", "TipoOperacao")
                        .WithMany("IntencoesOperacao")
                        .HasForeignKey("IdTipoOperacao")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dominio.UsuarioDominio", "Usuario")
                        .WithMany("IntencoesOperacao")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Dominio.ParametroOperacaoDominio", b =>
                {
                    b.HasOne("Dominio.ConvenioDominio", "Convenio")
                        .WithMany("ParametrosOperacao")
                        .HasForeignKey("IdConvenio")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dominio.TipoOperacaoDominio", "TipoOperacao")
                        .WithMany("ParametrosOperacao")
                        .HasForeignKey("IdTipoOperacao")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.TelefoneLojaDominio", b =>
                {
                    b.HasOne("Dominio.LojaDominio", "Loja")
                        .WithMany("Telefones")
                        .HasForeignKey("IdLoja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

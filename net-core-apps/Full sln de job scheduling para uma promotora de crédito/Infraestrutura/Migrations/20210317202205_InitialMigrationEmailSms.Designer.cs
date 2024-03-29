﻿// <auto-generated />
using System;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestrutura.Migrations
{
    [DbContext(typeof(PlataformaClienteContexto))]
    [Migration("20210317202205_InitialMigrationEmailSms")]
    partial class InitialMigrationEmailSms
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.Entidades.EmailFornecedorDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_EMAIL_FORNECEDOR")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnName("HOST")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("ID_EMPRESA")
                        .HasColumnType("int");

                    b.Property<string>("NomeExibicao")
                        .HasColumnName("NOME_EXIBICAO")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Porta")
                        .HasColumnName("PORTA")
                        .HasColumnType("int")
                        .HasMaxLength(4);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("SENHA")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Ssl")
                        .HasColumnName("SSL")
                        .HasColumnType("bit");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnName("USUARIO")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("EMAIL_FORNECEDOR");
                });

            modelBuilder.Entity("Dominio.Entidades.EmailMensagemDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_EMAIL_MENSAGEM")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnName("ASSUNTO")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CodigoReferenciaEmail")
                        .IsRequired()
                        .HasColumnName("CODIGO_REFERENCIA_EMAIL")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<string>("Copia")
                        .HasColumnName("COPIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataEnvio")
                        .HasColumnName("DATA_ENVIO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInsercao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataRecebimento")
                        .HasColumnName("DATA_RECEBIMENTO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destinatario")
                        .IsRequired()
                        .HasColumnName("DESTINATARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEmailFornecedor")
                        .HasColumnName("ID_EMAIL_FORNECEDOR")
                        .HasColumnType("int");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnName("MENSAGEM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Prioritario")
                        .HasColumnName("PRIORITARIO")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("IdEmailFornecedor");

                    b.ToTable("EMAIL_MENSAGEM");
                });

            modelBuilder.Entity("Dominio.Entidades.EmpresaDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_EMPRESA")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("EMPRESA");
                });

            modelBuilder.Entity("Dominio.Entidades.SituacaoEnvioDetalhesDominio", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnName("ID_SITUACAO_ENVIO_DETALHES")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("DESCRICAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("SITUACAO_ENVIO_DETALHES");
                });

            modelBuilder.Entity("Dominio.Entidades.SituacaoEnvioDominio", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnName("ID_SITUACAO_ENVIO")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("DESCRICAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("SITUACAO_ENVIO");
                });

            modelBuilder.Entity("Dominio.Entidades.SmsFornecedorDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_SMS_FORNECEDOR")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoAgrupador")
                        .HasColumnName("CODIGO_AGRUPADOR")
                        .HasColumnType("int")
                        .HasMaxLength(3);

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("ID_EMPRESA")
                        .HasColumnType("int");

                    b.Property<string>("NomeExibicao")
                        .HasColumnName("NOME_EXIBICAO")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("SENHA")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnName("USUARIO")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("SMS_FORNECEDOR");
                });

            modelBuilder.Entity("Dominio.Entidades.SmsMensagemDominio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_SMS_MENSAGEM")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoReferenciaMensagem")
                        .IsRequired()
                        .HasColumnName("CODIGO_REFERENCIA_MENSAGEM")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnName("DATA_ATUALIZACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataEnvio")
                        .HasColumnName("DATA_ENVIO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInsercao")
                        .HasColumnName("DATA_INSERCAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataRecebimento")
                        .HasColumnName("DATA_RECEBIMENTO")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdSituacaoEnvio")
                        .HasColumnName("ID_SITUACAO_ENVIO")
                        .HasColumnType("int");

                    b.Property<int?>("IdSituacaoEnvioDetalhes")
                        .HasColumnName("ID_SITUACAO_ENVIO_DETALHES")
                        .HasColumnType("int");

                    b.Property<int>("IdSmsFornecedor")
                        .HasColumnName("ID_SMS_FORNECEDOR")
                        .HasColumnType("int");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnName("MENSAGEM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroTelefone")
                        .IsRequired()
                        .HasColumnName("NUMERO_TELEFONE")
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Operadora")
                        .HasColumnName("OPERADORA")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<bool>("Processado")
                        .HasColumnName("PROCESSADO")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioAtualizacao")
                        .HasColumnName("USUARIO_ATUALIZACAO")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("IdSituacaoEnvio");

                    b.HasIndex("IdSituacaoEnvioDetalhes");

                    b.HasIndex("IdSmsFornecedor");

                    b.ToTable("SMS_MENSAGEM");
                });

            modelBuilder.Entity("Dominio.Entidades.EmailFornecedorDominio", b =>
                {
                    b.HasOne("Dominio.Entidades.EmpresaDominio", "Empresa")
                        .WithMany("EmailFornecedores")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.EmailMensagemDominio", b =>
                {
                    b.HasOne("Dominio.Entidades.EmailFornecedorDominio", "EmailFornecedor")
                        .WithMany("EmailMensagens")
                        .HasForeignKey("IdEmailFornecedor")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.SmsFornecedorDominio", b =>
                {
                    b.HasOne("Dominio.Entidades.EmpresaDominio", "Empresa")
                        .WithMany("SmsFornecedores")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.SmsMensagemDominio", b =>
                {
                    b.HasOne("Dominio.Entidades.SituacaoEnvioDominio", "SituacaoEnvio")
                        .WithMany("SmsMensagens")
                        .HasForeignKey("IdSituacaoEnvio")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Dominio.Entidades.SituacaoEnvioDetalhesDominio", "SituacaoEnvioDetalhes")
                        .WithMany("SmsMensagens")
                        .HasForeignKey("IdSituacaoEnvioDetalhes")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Dominio.Entidades.SmsFornecedorDominio", "SmsFornecedor")
                        .WithMany("SmsMensagens")
                        .HasForeignKey("IdSmsFornecedor")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

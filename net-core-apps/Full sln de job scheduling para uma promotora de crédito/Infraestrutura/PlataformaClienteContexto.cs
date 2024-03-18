using Dominio.Entidades;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Infraestrutura
{
    [ExcludeFromCodeCoverage]
    public class PlataformaClienteContexto : DbContext
    {
        public DbSet<EmpresaDominio> Empresas { get; set; }
        public DbSet<EmailFornecedorDominio> EmailFornecedores { get; set; }
        public DbSet<EmailMensagemDominio> EmailMensagens { get; set; }
        public DbSet<SmsFornecedorDominio> SmsFornecedores { get; set; }
        public DbSet<SmsMensagemDominio> SmsMensagens { get; set; }
        public DbSet<SituacaoEnvioDominio> SituacoesEnvio { get; set; }
        public DbSet<SituacaoEnvioDetalhesDominio> SituacoesEnvioDetalhes { get; set; }
        public DbSet<WhatsappMensagemDominio> WhatsappMensagens { get; set; }
        public DbSet<WhatsappFornecedorDominio> WhatsappFornecedores { get; set; }
        public DbSet<TorpedoVozFornecedorDominio> TorpedoVozFornecedores { get; set; }
        public DbSet<TorpedoVozMensagemDominio> TorpedoVozMensagens { get; set; }

        public DbSet<SeguroParentescoBemDominio> SeguroParentescoBem { get; set; }
        public DbSet<SeguroParentescoIcatuDominio> SeguroParentescoIcatu { get; set; }
        public DbSet<SeguroProfissaoBemDominio> SeguroProfissaoBem { get; set; }
        public DbSet<SeguroProfissaoIcatuDominio> SeguroProfissaoIcatu { get; set; }

        public PlataformaClienteContexto() { }

        public PlataformaClienteContexto(DbContextOptions<PlataformaClienteContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<EmpresaDominio>());
            modelBuilder.Entity<EmpresaDominio>().Property(empresa => empresa.Nome).HasSnakeCaseColumnName().HasMaxLength(50).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<EmailFornecedorDominio>());
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.Host).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.Porta).HasSnakeCaseColumnName().HasMaxLength(4).IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.Usuario).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.Senha).HasSnakeCaseColumnName().HasMaxLength(50).IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.NomeExibicao).HasSnakeCaseColumnName().HasMaxLength(50);
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.Ssl).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>().Property(email => email.IdEmpresa).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailFornecedorDominio>()
                .HasOne(email => email.Empresa)
                .WithMany(empresa => empresa.EmailFornecedores)
                .HasForeignKey(email => email.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<EmailMensagemDominio>());
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.CodigoReferenciaEmail).HasSnakeCaseColumnName().IsRequired().HasMaxLength(13);
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.Assunto).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.Destinatario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.Copia).HasSnakeCaseColumnName();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.Mensagem).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.Prioritario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.DataInsercao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.DataEnvio).HasSnakeCaseColumnName();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.DataRecebimento).HasSnakeCaseColumnName();
            modelBuilder.Entity<EmailMensagemDominio>().Property(mensagem => mensagem.IdEmailFornecedor).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EmailMensagemDominio>()
                .HasOne(mensagem => mensagem.EmailFornecedor)
                .WithMany(email => email.EmailMensagens)
                .HasForeignKey(email => email.IdEmailFornecedor)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SmsFornecedorDominio>());
            modelBuilder.Entity<SmsFornecedorDominio>().Property(fornecedor => fornecedor.CodigoAgrupador).HasSnakeCaseColumnName().HasMaxLength(3).IsRequired();
            modelBuilder.Entity<SmsFornecedorDominio>().Property(fornecedor => fornecedor.Usuario).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SmsFornecedorDominio>().Property(fornecedor => fornecedor.Senha).HasSnakeCaseColumnName().HasMaxLength(50).IsRequired();
            modelBuilder.Entity<SmsFornecedorDominio>().Property(fornecedor => fornecedor.NomeExibicao).HasSnakeCaseColumnName().HasMaxLength(50);
            modelBuilder.Entity<SmsFornecedorDominio>().Property(fornecedor => fornecedor.IdEmpresa).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SmsFornecedorDominio>()
                .HasOne(fornecedor => fornecedor.Empresa)
                .WithMany(empresa => empresa.SmsFornecedores)
                .HasForeignKey(fornecedor => fornecedor.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            string situacaoEnvioNome = setPropriedadesDeEntidadeBase(modelBuilder.Entity<SituacaoEnvioDominio>(), false);
            modelBuilder.Entity<SituacaoEnvioDominio>().ToTable(situacaoEnvioNome.CastToUpperSnakeCase());
            modelBuilder.Entity<SituacaoEnvioDominio>().Property(e => e.ID).HasSnakeCaseColumnName(situacaoEnvioNome).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<SituacaoEnvioDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SituacaoEnvioDominio>().HasKey(e => e.ID);

            string situacaoEnvioDetalhesNome = setPropriedadesDeEntidadeBase(modelBuilder.Entity<SituacaoEnvioDetalhesDominio>(), false);
            modelBuilder.Entity<SituacaoEnvioDetalhesDominio>().ToTable(situacaoEnvioDetalhesNome.CastToUpperSnakeCase());
            modelBuilder.Entity<SituacaoEnvioDetalhesDominio>().Property(e => e.ID).HasSnakeCaseColumnName(situacaoEnvioDetalhesNome).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<SituacaoEnvioDetalhesDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();

            modelBuilder.Entity<SituacaoEnvioDetalhesDominio>().HasKey(e => e.ID);

            string smsMensagemNome = setPropriedadesDeEntidadeBase(modelBuilder.Entity<SmsMensagemDominio>());
            modelBuilder.Entity<SmsMensagemDominio>().Property(e => e.ID).HasSnakeCaseColumnName(smsMensagemNome).HasConversion<int>();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.CodigoReferenciaMensagem).HasSnakeCaseColumnName().IsRequired().HasMaxLength(13);
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.NumeroTelefone).HasSnakeCaseColumnName().IsRequired().HasMaxLength(14);
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.IdSituacaoEnvio).HasSnakeCaseColumnName();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.Operadora).HasSnakeCaseColumnName().HasMaxLength(20);
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.Mensagem).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.Processado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.DataInsercao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.DataEnvio).HasSnakeCaseColumnName();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.DataRecebimento).HasSnakeCaseColumnName();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.IdSmsFornecedor).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.IdSituacaoEnvioDetalhes).HasSnakeCaseColumnName();
            modelBuilder.Entity<SmsMensagemDominio>().Property(mensagem => mensagem.IdSmsFornecedor).HasSnakeCaseColumnName();
            modelBuilder.Entity<SmsMensagemDominio>().HasKey(e => e.ID);
            modelBuilder.Entity<SmsMensagemDominio>()
                .HasOne(mensagem => mensagem.SmsFornecedor)
                .WithMany(fornecedor => fornecedor.SmsMensagens)
                .HasForeignKey(fornecedor => fornecedor.IdSmsFornecedor)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SmsMensagemDominio>()
                .HasOne(mensagem => mensagem.SituacaoEnvioDetalhes)
                .WithMany(situacao => situacao.SmsMensagens)
                .HasForeignKey(fornecedor => fornecedor.IdSituacaoEnvioDetalhes)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SmsMensagemDominio>()
                .HasOne(mensagem => mensagem.SituacaoEnvio)
                .WithMany(situacao => situacao.SmsMensagens)
                .HasForeignKey(fornecedor => fornecedor.IdSituacaoEnvio)
                .OnDelete(DeleteBehavior.NoAction);

            var nomeWhatsappFornecedor = setPropriedadesDeEntidadeBase(modelBuilder.Entity<WhatsappFornecedorDominio>());
            modelBuilder.Entity<WhatsappFornecedorDominio>().Property(e => e.ID).HasSnakeCaseColumnName(nomeWhatsappFornecedor).HasConversion<int>();
            modelBuilder.Entity<WhatsappFornecedorDominio>().Property(fornecedor => fornecedor.NomeExibicao).HasSnakeCaseColumnName().HasMaxLength(50).IsRequired();
            modelBuilder.Entity<WhatsappFornecedorDominio>().Property(fornecedor => fornecedor.Chave).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
            modelBuilder.Entity<WhatsappFornecedorDominio>().Property(fornecedor => fornecedor.IdEmpresa).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<WhatsappFornecedorDominio>()
                .HasOne(fornecedor => fornecedor.Empresa)
                .WithMany(empresa => empresa.WhatsappFornecedores)
                .HasForeignKey(fornecedor => fornecedor.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            var nomeWhatsappMensagem = setPropriedadesDeEntidadeBase(modelBuilder.Entity<WhatsappMensagemDominio>());
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(e => e.ID).HasSnakeCaseColumnName(nomeWhatsappMensagem).HasConversion<int>();
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.CodigoReferenciaMensagem).HasMaxLength(13).HasSnakeCaseColumnName().IsUnicode(false).IsRequired();
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.IdTemplate).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.NumeroTelefone).HasMaxLength(15).HasSnakeCaseColumnName().IsUnicode(false).IsRequired();
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.MensagemEnvio).HasSnakeCaseColumnName().HasColumnType("varchar(MAX)").IsRequired();
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.MensagemRetornoErro).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(4000);
            modelBuilder.Entity<WhatsappMensagemDominio>().Property(mensagem => mensagem.IdWhatsappFornecedor).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<WhatsappMensagemDominio>().HasKey(e => e.ID);
            modelBuilder.Entity<WhatsappMensagemDominio>()
                .HasOne(mensagem => mensagem.WhatsappFornecedor)
                .WithMany(fornecedor => fornecedor.WhatsappMensagens)
                .HasForeignKey(fornecedor => fornecedor.IdWhatsappFornecedor)
                .OnDelete(DeleteBehavior.NoAction);

            var torpedoVozFornecedorNome = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TorpedoVozFornecedorDominio>());
            modelBuilder.Entity<TorpedoVozFornecedorDominio>().Property(fornecedor => fornecedor.NomeExibicao).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired();
            modelBuilder.Entity<TorpedoVozFornecedorDominio>().Property(fornecedor => fornecedor.ChaveEnvio).HasSnakeCaseColumnName().HasMaxLength(50).IsRequired();
            modelBuilder.Entity<TorpedoVozFornecedorDominio>().Property(fornecedor => fornecedor.IdEmpresa).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TorpedoVozFornecedorDominio>()
                .HasOne(fornecedor => fornecedor.Empresa)
                .WithMany(empresa => empresa.TorpedoVozFornecedores)
                .HasForeignKey(fornecedor => fornecedor.IdEmpresa)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TorpedoVozMensagemDominio>());
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.CodigoReferenciaMensagem).HasSnakeCaseColumnName().HasMaxLength(13).IsRequired();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.NumeroTelefone).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(12).IsRequired();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.Mensagem).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.IdTorpedoVozFornecedor).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.Situacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().Property(mensagem => mensagem.DataInsercao).HasSnakeCaseColumnName();
            modelBuilder.Entity<TorpedoVozMensagemDominio>().HasKey(e => e.ID);
            modelBuilder.Entity<TorpedoVozMensagemDominio>()
                .HasOne(mensagem => mensagem.TorpedoVozFornecedor)
                .WithMany(fornecedor => fornecedor.TorpedoVozMensagens)
                .HasForeignKey(mensagem => mensagem.IdTorpedoVozFornecedor)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroParentescoBemDominio>());
            modelBuilder.Entity<SeguroParentescoBemDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000);
            modelBuilder.Entity<SeguroParentescoBemDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroParentescoIcatuDominio>());
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000);
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.IdSeguroParentescoBem).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroParentescoIcatuDominio>()
                .HasOne(spi => spi.SeguroParentescoBem)
                .WithOne(spb => spb.SeguroParentescoIcatu)
                .HasForeignKey<SeguroParentescoIcatuDominio>(spi => spi.IdSeguroParentescoBem);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProfissaoBemDominio>());
            modelBuilder.Entity<SeguroProfissaoBemDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000);
            modelBuilder.Entity<SeguroProfissaoBemDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProfissaoIcatuDominio>());
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000);
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.IdSeguroProfissaoBem).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>()
                .HasOne(spi => spi.SeguroProfissaoBem)
                .WithOne(spb => spb.SeguroProfissaoIcatu)
                .HasForeignKey<SeguroProfissaoIcatuDominio>(spi => spi.IdSeguroProfissaoBem);
        }
        private string setPropriedadesDeEntidadeBase<T>(EntityTypeBuilder<T> entityTypeBuilder, bool gerarId = true) where T : EntidadeBase
        {
            string entidade = entityTypeBuilder.Metadata.Name
                .Split(".")
                .Last()
                .Replace("Dominio", "");




            entityTypeBuilder.ToTable(entidade.CastToUpperSnakeCase());
            entityTypeBuilder.Property(model => model.UsuarioAtualizacao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(10);
            entityTypeBuilder.Property(model => model.DataAtualizacao).HasSnakeCaseColumnName();

            if (gerarId)
            {
                entityTypeBuilder.Property(model => model.ID).HasSnakeCaseColumnName(entidade);
            }

            return entidade;
        }
    }
}

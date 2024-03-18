using Dominio.Entidades;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Infraestrutura
{
    public class ProdutoClienteContexto : DbContext
    {
        public DbSet<SeguroParentescoBemDominio> SeguroParentescoBem { get; set; }
        public DbSet<SeguroParentescoIcatuDominio> SeguroParentescoIcatu { get; set; }
        public DbSet<SeguroProfissaoBemDominio> SeguroProfissaoBem { get; set; }
        public DbSet<SeguroProfissaoIcatuDominio> SeguroProfissaoIcatu { get; set; }

        public ProdutoClienteContexto() { }

        public ProdutoClienteContexto (DbContextOptions<ProdutoClienteContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

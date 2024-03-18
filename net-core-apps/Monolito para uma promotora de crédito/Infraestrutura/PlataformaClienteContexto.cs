using Dominio;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Infraestrutura
{
    public class PlataformaClienteContexto : DbContext
    {
        public DbSet<LeadDominio> Leads { get; set; }
        public DbSet<LeadCorrespondenteDominio> LeadsCorrespondente { get; set; }
        public DbSet<UsuarioDominio> Usuarios { get; set; }
        public DbSet<UsuarioRedeSocialDominio> UsuariosRedesSociais { get; set; }
        public DbSet<UsuarioRecuperacaoSenhaDominio> UsuarioRecuperacaoSenhas { get; set; }
        public DbSet<UsuarioConfirmacaoEmailDominio> UsuarioConfirmacaoEmailDominio { get; set; }
        public DbSet<UsuarioRequisicaoLogDominio> UsuariosRequisicaoLog { get; set; }
        public DbSet<ClienteDominio> Clientes { get; set; }
        public DbSet<TelefoneClienteDominio> TelefonesCliente { get; set; }
        public DbSet<DocumentoIdentificacaoClienteDominio> DocumentosCliente { get; set; }
        public DbSet<EnderecoClienteDominio> EnderecosCliente { get; set; }
        public DbSet<RendimentoClienteDominio> RendimentoCliente { get; set; }
        public DbSet<LojaDominio> Lojas { get; set; }
        public DbSet<TelefoneLojaDominio> TelefonesLojas { get; set; }
        public DbSet<ParametroOperacaoDominio> ParametroOperacao { get; set; }
        public DbSet<AnexoDominio> Anexos { get; set; }
        public DbSet<ConvenioDominio> Convenios { get; set; }
        public DbSet<SiapeTipoFuncionalDominio> SiapeTiposFunacionais { get; set; }
        public DbSet<InssEspecieBeneficioDominio> InssEspeciesBeneficios { get; set; }
        public DbSet<ConvenioOrgaoDominio> ConvenioOrgaos { get; set; }
        public DbSet<ProdutoDominio> Produtos { get; set; }
        public DbSet<IntencaoOperacaoSituacaoPassoDominio> IntencaoOperacaoSituacaoPassos { get; set; }
        public DbSet<TipoOperacaoDominio> TiposOperacao { get; set; }
        public DbSet<IntencaoOperacaoSituacaoDominio> IntencaoOperacaoSituacoes { get; set; }
        public DbSet<IntencaoOperacaoDominio> IntencoesOperacao { get; set; }
        public DbSet<IntencaoOperacaoPortabilidadeDominio> IntencoesOperacaoPortabilidade { get; set; }
        public DbSet<IntencaoOperacaoContratoDominio> IntencoesOperacaoContrato { get; set; }
        public DbSet<IntencaoOperacaoObservacaoDominio> IntencoesOperacaoObservacoes { get; set; }
        public DbSet<TipoContaDominio> TiposConta { get; set; }
        public DbSet<ContaClienteDominio> ContasCliente { get; set; }
        public DbSet<FormaRecebimentoDominio> FormasRecebimento { get; set; }
        public DbSet<BancoDominio> Bancos { get; set; }
        public DbSet<UnidadeFederativaDominio> UnidadesFederativas { get; set; }
        public DbSet<EstadoCivilDominio> EstadosCivil { get; set; }
        public DbSet<GrauInstrucaoDominio> GrausInstrucao { get; set; }
        public DbSet<TipoVinculoInstitucionalDominio> TiposVinculoInsticional { get; set; }
        public DbSet<TipoDocumentoDominio> TiposDocumento { get; set; }
        public DbSet<SolicitacaoDocumentoDominio> SolicitacoesDocumento { get; set; }
        public DbSet<OrgaoEmissorIdentificacaoDominio> OrgaosEmissoresIdentificacao { get; set; }
        public DbSet<GeneroDominio> Generos { get; set; }
        public DbSet<MunicipioDominio> Municipios { get; set; }
        public DbSet<TipoLogradouroDominio> TiposLogradouro { get; set; }
        public DbSet<BaseCepDominio> CEPs { get; set; }
        public DbSet<TemplateEmailDominio> TemplatesEmail { get; set; }
        public DbSet<TemplateEmailFinalidadeDominio> TemplateEmailFinalidades { get; set; }
        public DbSet<TemplateEmailTipoDominio> TemplateEmailTipos { get; set; }
        public DbSet<TemplateSmsDominio> TemplatesSms { get; set; }
        public DbSet<TemplateTorpedoVozDominio> TemplatesTorpedoVoz { get; set; }
        public DbSet<TemplateWhatsappDominio> TemplatesWhatsapp { get; set; }
        public DbSet<RegistroEmailDominio> RegistrosEmail { get; set; }
        public DbSet<RegistroSmsDominio> RegistrosSms { get; set; }
        public DbSet<RegistroTorpedoVozDominio> RegistrosTorpedoVoz { get; set; }
        public DbSet<RegistroWhatsappDominio> RegistrosWhatsapp { get; set; }
        public DbSet<TipoTermoDominio> TiposTermo { get; set; }
        public DbSet<TermoDominio> Termos { get; set; }
        public DbSet<UsuarioTermoDominio> UsuariosTermos { get; set; }
        public DbSet<NotificacaoDominio> Notificacoes { get; set; }
        public DbSet<NotificacaoTemplateDominio> TemplatesNotificacao { get; set; }
        public DbSet<NotificacaoFinalidadeDominio> NotificacaoFinalidades { get; set; }
        public DbSet<NotificacaoSeveridadeDominio> NotificacaoSeveridades { get; set; }
        public DbSet<ConsultaBeneficioInssClienteDominio> ConsultaBeneficiosInssCliente { get; set; }
        public DbSet<FeatureFlagDominio> FeatureFlags { get; set; }
        public DbSet<RedeSocialDominio> RedesSociais { get; set; }
        public DbSet<SolicitacaoAcessoDadosPessoaisClienteDominio> SolicitacoesAcessoDadosPessoaisClientes { get; set; }
        public DbSet<TipoSolicitacaoConfirmacaoDominio> TiposSolicitacaoConfirmacao { get; set; }
        public DbSet<TelefoneClienteSolicitacaoConfirmacaoDominio> TelefoneClienteSolicitacoesConfirmacao { get; set; }
        public DbSet<BeneficioInssMensagemDeParaDominio> BeneficioInssMensagensDePara { get; set; }
        public DbSet<SeguroProdutoDominio> SeguroProduto { get; set; }
        public DbSet<SeguroCoberturaDominio> SeguroCobertura { get; set; }
        public DbSet<SeguroProdutoIcatuDominio> SeguroProdutoIcatu { get; set; }
        public DbSet<SeguroCoberturaIcatuDominio> SeguroCoberturaIcatu { get; set; }
        public DbSet<SeguroPropostaDominio> SeguroProposta { get; set; }
        public DbSet<SeguroParentescoDominio> SeguroParentesco { get; set; }
        public DbSet<SeguroParentescoIcatuDominio> SeguroParentescoIcatu { get; set; }
        public DbSet<SeguroProfissaoDominio> SeguroProfissao { get; set; }
        public DbSet<SeguroProfissaoIcatuDominio> SeguroProfissaoIcatu { get; set; }
        public DbSet<ConjugeDominio> Conjuge { get; set; }
        public DbSet<TipoRegimeCasamentoDominio> TipoRegimeCasamento { get; set; }
        public DbSet<TipoRegimeCasamentoBemDominio> TipoRegimeCasamentoBem { get; set; }
        public DbSet<SeguroCobrancaPropostaIcatuDominio> SeguroCobrancaPropostaIcatu { get; set; }
        public DbSet<SeguroSituacaoIcatuDominio> SeguroSituacaoIcatu { get; set; }
        public DbSet<SeguroPropostaIcatuDominio> SeguroPropostaIcatu { get; set; }
        public DbSet<SeguroCobrancaPropostaCartaoIcatuDominio> SeguroCobrancaPropostaCartaoIcatu { get; set; }
        public DbSet<SeguroClienteIcatuDominio> SeguroClienteIcatu { get; set; }
        public DbSet<SeguroEnderecoClienteDominio> SeguroEnderecoCliente { get; set; }
        public DbSet<SeguroBeneficiarioDominio> SeguroBeneficiario { get; set; }
        public DbSet<SeguroBeneficiarioIcatuDominio> SeguroBeneficiarioIcatu { get; set; }
        public DbSet<SeguroClienteTelefoneDominio> SeguroClienteTelefone { get; set; }
        public DbSet<BiometriaClienteDominio> BiometriaClientes { get; set; }
        public DbSet<BiometriaSituacaoDominio> BiometriaSituacoes { get; set; }
        public DbSet<RegistroBiometriaUnicoDominio> RegistrosBiometriaCliente { get; set; }
        public DbSet<RegistroClubeBeneficiosDominio> RegistroClubeBeneficiosDominio { get; set; }
        public DbSet<MarinhaCargoDominio> MarinhaCargos { get; set; }
        public DbSet<MarinhaTipoFuncionalDominio> MarinhaTiposFuncionais { get; set; }
        public DbSet<AeronauticaCargoDominio> AeronauticaCargos { get; set; }
        public DbSet<AeronauticaTipoFuncionalDominio> AeronauticaTiposFuncionais { get; set; }
        public DbSet<MeioPagamentoSeguroDominio> MeiosPagamentoSeguro { get; set; }
        public DbSet<ContaBancariaDominio> ContasBancarias { get; set; }

        public PlataformaClienteContexto(DbContextOptions<PlataformaClienteContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            setPropriedadesDeEntidadeBase(modelBuilder.Entity<FeatureFlagDominio>());
            modelBuilder.Entity<FeatureFlagDominio>().Property(featureFlag => featureFlag.Chave).IsRequired().HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<FeatureFlagDominio>().Property(featureFlag => featureFlag.Habilitado).IsRequired().HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<LeadDominio>());
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.CPF).HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Telefone).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Celular).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Email).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.IdConvenio).HasSnakeCaseColumnName();
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Latitude).HasSnakeCaseColumnName().HasMaxLength(10);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.Longitude).HasSnakeCaseColumnName().HasMaxLength(10);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.OrigemRequisicaoPalavraChave).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.OrigemRequisicaoMidia).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.OrigemRequisicaoConteudo).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.OrigemRequisicaoTermo).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.OrigemRequisicaoCampanha).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.DesejaContatoWhatsApp).HasColumnName("DESEJA_CONTATO_WHATSAPP");
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.LinkContatoWhatsAppLoja).HasColumnName("LINK_CONTATO_WHATSAPP_LOJA").HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.IdLoja).HasSnakeCaseColumnName();
            modelBuilder.Entity<LeadDominio>().Property(lead => lead.IdProduto).HasSnakeCaseColumnName();
            modelBuilder.Entity<LeadDominio>()
                .HasOne(lead => lead.Convenio)
                .WithMany()
                .HasForeignKey(lead => lead.IdConvenio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeadDominio>()
                .HasOne(lead => lead.Loja)
                .WithMany(lojaDominio => lojaDominio.Leads)
                .HasForeignKey(lead => lead.IdLoja)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeadDominio>()
                .HasOne(lead => lead.Produto)
                .WithMany()
                .HasForeignKey(lead => lead.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<LeadCorrespondenteDominio>());
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.CNPJ).HasMaxLength(20).IsRequired().IsUnicode(false);
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired().IsUnicode(false);
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.Telefone).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired().IsUnicode(false);
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.Email).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired().IsUnicode(false);
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.IdMunicipio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<LeadCorrespondenteDominio>().Property(lead => lead.Atividades).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(300);
            modelBuilder.Entity<LeadCorrespondenteDominio>()
                .HasOne(lead => lead.Municipio)
                .WithMany()
                .HasForeignKey(lead => lead.IdMunicipio)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioDominio>());
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.Email).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.EmailConfirmado).HasSnakeCaseColumnName();
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.CPF).HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.Senha).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.Administrador).HasSnakeCaseColumnName();
            modelBuilder.Entity<UsuarioDominio>().Property(usuarioDominio => usuarioDominio.EmailConfirmado).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioRedeSocialDominio>());
            modelBuilder.Entity<UsuarioRedeSocialDominio>().Property(usuarioRedeSocial => usuarioRedeSocial.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioRedeSocialDominio>().Property(usuarioRedeSocial => usuarioRedeSocial.IdRedeSocial).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<UsuarioRedeSocialDominio>().Property(usuarioRedeSocial => usuarioRedeSocial.Login).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UsuarioRedeSocialDominio>().Property(usuarioRedeSocial => usuarioRedeSocial.Ativo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioRedeSocialDominio>().HasIndex(usuarioRedeSocial => new { usuarioRedeSocial.IdUsuario, usuarioRedeSocial.IdRedeSocial }).IsUnique();
            modelBuilder.Entity<UsuarioRedeSocialDominio>()
                .HasOne(usuarioRedeSocial => usuarioRedeSocial.Usuario)
                .WithMany(usuarioDominio => usuarioDominio.UsuariosRedesSociais)
                .HasForeignKey(usuarioRedeSocial => usuarioRedeSocial.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>());
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>().Property(requisicao => requisicao.Token).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>().Property(requisicao => requisicao.DataRequisicao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>().Property(requisicao => requisicao.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>().Property(requisicao => requisicao.Valido).HasSnakeCaseColumnName().IsRequired().HasDefaultValue(true);
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>().HasIndex(requisicao => requisicao.Token).IsUnique();
            modelBuilder.Entity<UsuarioRecuperacaoSenhaDominio>()
                .HasOne(requisicao => requisicao.Usuario)
                .WithMany()
                .HasForeignKey(requisicao => requisicao.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>());
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>().Property(requisicao => requisicao.Token).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>().Property(requisicao => requisicao.DataRequisicao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>().Property(requisicao => requisicao.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>().Property(requisicao => requisicao.Valido).HasSnakeCaseColumnName().IsRequired().HasDefaultValue(true);
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>().HasIndex(requisicao => requisicao.Token).IsUnique();
            modelBuilder.Entity<UsuarioConfirmacaoEmailDominio>()
                .HasOne(requisicao => requisicao.Usuario)
                .WithMany()
                .HasForeignKey(requisicao => requisicao.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioRequisicaoLogDominio>());
            modelBuilder.Entity<UsuarioRequisicaoLogDominio>().Property(log => log.UsuarioTenant).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<UsuarioRequisicaoLogDominio>().Property(log => log.UrlRequisicao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<UsuarioRequisicaoLogDominio>().Property(log => log.CorpoRequisicao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<UsuarioRequisicaoLogDominio>().Property(log => log.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioRequisicaoLogDominio>()
                .HasOne(log => log.Usuario)
                .WithMany()
                .HasForeignKey(log => log.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ClienteDominio>());
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdGenero).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdEstadoCivil).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdGrauInstrucao).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdCidadeNatal).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.Filiacao1).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.Filiacao2).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.DeficienteVisual).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.DataImportacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().HasIndex(cliente => cliente.IdUsuario).IsUnique();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdLoja).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.ImportacaoDadosAutorizada).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.DataAutorizacaoImportacaoDados).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.ImportacaoDadosSolicitada).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.DataSolicitacaoImportacaoDados).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdTelefonePrincipal).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdTelefoneSecundario).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdEnderecoPrincipal).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdEnderecoSecundario).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdConjuge).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.IdProfissao).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>().Property(cliente => cliente.OperacaoAtiva).HasSnakeCaseColumnName();
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.Loja)
                .WithMany(lojaDominio => lojaDominio.Clientes)
                .HasForeignKey(clienteDominio => clienteDominio.IdLoja)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.Genero)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdGenero)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.EstadoCivil)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdEstadoCivil)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.GrauInstrucao)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdGrauInstrucao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.CidadeNatal)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdCidadeNatal)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.Usuario)
                .WithOne(usuario => usuario.Cliente)
                .HasForeignKey<ClienteDominio>(cliente => cliente.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.TelefonePrincipal)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdTelefonePrincipal)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.TelefoneSecundario)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdTelefoneSecundario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.EnderecoPrincipal)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdEnderecoPrincipal)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.EnderecoSecundario)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdEnderecoSecundario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.Profissao)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdProfissao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClienteDominio>()
                .HasOne(cliente => cliente.ContaBancaria)
                .WithMany()
                .HasForeignKey(cliente => cliente.IdContaBancaria)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TelefoneClienteDominio>());
            modelBuilder.Entity<TelefoneClienteDominio>().Property(telefone => telefone.DDD).HasSnakeCaseColumnName().IsRequired().HasMaxLength(3).IsUnicode(false);
            modelBuilder.Entity<TelefoneClienteDominio>().Property(telefone => telefone.Fone).HasSnakeCaseColumnName().IsRequired().HasMaxLength(9).IsUnicode(false);
            modelBuilder.Entity<TelefoneClienteDominio>().Property(telefone => telefone.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteDominio>().Property(telefone => telefone.Confirmado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteDominio>().Property(telefone => telefone.Deletado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteDominio>()
                .HasOne(telefone => telefone.Cliente)
                .WithMany(cliente => cliente.Telefones)
                .HasForeignKey(telefone => telefone.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>());
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.IdUnidadeFederativa).HasSnakeCaseColumnName();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.IdOrgaoEmissor).HasSnakeCaseColumnName();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.IdTipoDocumento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.Deletado).HasSnakeCaseColumnName();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.Numero).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false);
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>().Property(documentoIdentificacao => documentoIdentificacao.DataEmissao).HasSnakeCaseColumnName();
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>()
                .HasOne(documento => documento.Cliente)
                .WithMany(cliente => cliente.DocumentosIdentificacao)
                .HasForeignKey(documento => documento.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>()
                .HasOne(documento => documento.TipoDocumento)
                .WithMany()
                .HasForeignKey(documento => documento.IdTipoDocumento)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>()
                .HasOne(documento => documento.Uf)
                .WithMany()
                .HasForeignKey(documento => documento.IdUnidadeFederativa)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DocumentoIdentificacaoClienteDominio>()
                .HasOne(documento => documento.OrgaoEmissor)
                .WithMany()
                .HasForeignKey(documento => documento.IdOrgaoEmissor)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<EnderecoClienteDominio>());
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Titulo).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.IdMunicipio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Bairro).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.IdTipoLogradouro).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Logradouro).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Numero).HasSnakeCaseColumnName();
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Complemento).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Cep).HasSnakeCaseColumnName().HasMaxLength(8).IsUnicode(false);
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Principal).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EnderecoClienteDominio>().Property(endereco => endereco.Deletado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<EnderecoClienteDominio>()
                .HasOne(endereco => endereco.Cliente)
                .WithMany(cliente => cliente.Enderecos)
                .HasForeignKey(endereco => endereco.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EnderecoClienteDominio>()
                .HasOne(endereco => endereco.Municipio)
                .WithMany()
                .HasForeignKey(documento => documento.IdMunicipio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EnderecoClienteDominio>()
                .HasOne(endereco => endereco.TipoLogradouro)
                .WithMany()
                .HasForeignKey(documento => documento.IdTipoLogradouro)
                .OnDelete(DeleteBehavior.NoAction);

            var entidadeFormaRecebimento = setPropriedadesDeEntidadeBase(modelBuilder.Entity<FormaRecebimentoDominio>(), false);
            modelBuilder.Entity<FormaRecebimentoDominio>().Property(formaRecebimento => formaRecebimento.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<FormaRecebimentoDominio>().Property(formaRecebimento => formaRecebimento.ID).HasSnakeCaseColumnName(entidadeFormaRecebimento).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<FormaRecebimentoDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ContaClienteDominio>());
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.IdBanco).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.IdTipoConta).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.Agencia).HasSnakeCaseColumnName().HasMaxLength(15).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.Conta).HasSnakeCaseColumnName().HasMaxLength(15).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ContaClienteDominio>().Property(conta => conta.IdFormaRecebimento).HasSnakeCaseColumnName();
            modelBuilder.Entity<ContaClienteDominio>()
                .HasOne(conta => conta.Cliente)
                .WithMany(cliente => cliente.Contas)
                .HasForeignKey(conta => conta.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ContaClienteDominio>()
                .HasOne(conta => conta.Banco)
                .WithMany()
                .HasForeignKey(conta => conta.IdBanco)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ContaClienteDominio>()
                .HasOne(conta => conta.TipoConta)
                .WithMany()
                .HasForeignKey(conta => conta.IdTipoConta)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ContaClienteDominio>()
                .HasOne(conta => conta.FormaRecebimento)
                .WithMany()
                .HasForeignKey(conta => conta.IdFormaRecebimento)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RendimentoClienteDominio>());
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdConvenio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdConvenioOrgao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdUf).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.Matricula).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.Deletado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.ValorRendimento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.MargemDisponivel).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.MargemDisponivelCartao).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdContaCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>().Property(rendimento => rendimento.IdContaClienteRecebimento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.Cliente)
                .WithMany(cliente => cliente.Rendimentos)
                .HasForeignKey(rendimento => rendimento.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.Convenio)
                .WithMany()
                .HasForeignKey(rendimento => rendimento.IdConvenio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.ConvenioOrgao)
                .WithMany()
                .HasForeignKey(rendimento => rendimento.IdConvenioOrgao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.Uf)
                .WithMany()
                .HasForeignKey(rendimento => rendimento.IdUf)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.ContaCliente)
                .WithMany(contaCliente => contaCliente.Rendimentos)
                .HasForeignKey(rendimento => rendimento.IdContaCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteDominio>()
                .HasOne(rendimento => rendimento.ContaClienteRecebimento)
                .WithMany(contaCliente => contaCliente.RendimentosRecebimento)
                .HasForeignKey(rendimento => rendimento.IdContaClienteRecebimento)
                .OnDelete(DeleteBehavior.NoAction);

            string entidadeRendimentoInss = modelBuilder.Entity<RendimentoClienteInssDominio>().Metadata.Name.Replace("Dominio", "").Replace(".", "");
            modelBuilder.Entity<RendimentoClienteInssDominio>().ToTable(entidadeRendimentoInss.CastToUpperSnakeCase());
            modelBuilder.Entity<RendimentoClienteInssDominio>().Property(rendimentoInss => rendimentoInss.IdInssEspecieBeneficio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RendimentoClienteInssDominio>().Property(rendimentoInss => rendimentoInss.Matricula).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RendimentoClienteInssDominio>().Property(rendimentoInss => rendimentoInss.DataInscricao).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<RendimentoClienteInssDominio>().Property(rendimentoInss => rendimentoInss.MargemDisponivel).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteInssDominio>().Property(rendimentoInss => rendimentoInss.MargemDisponivelCartao).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteInssDominio>()
                .HasOne(rendimentoInss => rendimentoInss.EspecieBeneficio)
                .WithMany()
                .HasForeignKey(rendimentoInss => rendimentoInss.IdInssEspecieBeneficio)
                .OnDelete(DeleteBehavior.NoAction);

            string entidadeRendimentoSiape = modelBuilder.Entity<RendimentoClienteSiapeDominio>().Metadata.Name.Replace("Dominio", "").Replace(".", "");
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().ToTable(entidadeRendimentoSiape.CastToUpperSnakeCase());
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.IdSiapeTipoFuncional).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.Matricula).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.MatriculaInstituidor).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false);
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.NomeInstituidor).HasSnakeCaseColumnName().HasMaxLength(80).IsUnicode(false);
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.PossuiRepresentacaoPorProcurador).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.DataAdmissao).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.DataLiberacaoConsultaMargem).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.MargemDisponivel).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>().Property(rendimentoSiape => rendimentoSiape.MargemDisponivelCartao).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteSiapeDominio>()
                .HasOne(rendimentoSiape => rendimentoSiape.TipoFuncional)
                .WithMany()
                .HasForeignKey(rendimentoSiape => rendimentoSiape.IdSiapeTipoFuncional)
                .OnDelete(DeleteBehavior.NoAction);


            string entidadeRendimentoMarinha = modelBuilder.Entity<RendimentoClienteMarinhaDominio>().Metadata.Name.Replace("Dominio", "").Replace(".", "");
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>().ToTable(entidadeRendimentoMarinha.CastToUpperSnakeCase());
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>().Property(rendimentoMarinha => rendimentoMarinha.IdMarinhaTipoFuncional).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>().Property(rendimentoMarinha => rendimentoMarinha.Matricula).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>().Property(rendimentoMarinha => rendimentoMarinha.IdMarinhaCargo).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>()
                .HasOne(rendimentoMarinha => rendimentoMarinha.TipoFuncional)
                .WithMany()
                .HasForeignKey(rendimentoMarinha => rendimentoMarinha.IdMarinhaTipoFuncional)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteMarinhaDominio>()
                .HasOne(rendimentoMarinha => rendimentoMarinha.Cargo)
                .WithMany()
                .HasForeignKey(rendimentoMarinha => rendimentoMarinha.IdMarinhaCargo)
                .OnDelete(DeleteBehavior.NoAction);

            string entidadeRendimentoAeronautica = modelBuilder.Entity<RendimentoClienteAeronauticaDominio>().Metadata.Name.Replace("Dominio", "").Replace(".", "");
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>().ToTable(entidadeRendimentoAeronautica.CastToUpperSnakeCase());
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>().Property(rendimentoAeronautica => rendimentoAeronautica.IdAeronauticaTipoFuncional).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>().Property(rendimentoAeronautica => rendimentoAeronautica.Matricula).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>().Property(rendimentoAeronautica => rendimentoAeronautica.IdAeronauticaCargo).HasSnakeCaseColumnName();
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>()
                .HasOne(rendimentoMarinha => rendimentoMarinha.TipoFuncional)
                .WithMany()
                .HasForeignKey(rendimentoMarinha => rendimentoMarinha.IdAeronauticaTipoFuncional)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RendimentoClienteAeronauticaDominio>()
                .HasOne(rendimentoMarinha => rendimentoMarinha.Cargo)
                .WithMany()
                .HasForeignKey(rendimentoMarinha => rendimentoMarinha.IdAeronauticaCargo)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<LojaDominio>());
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Geolocalizacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.MensagemApresentacao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.IdMunicipio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Bairro).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.IdTipoLogradouro).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Logradouro).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Numero).HasSnakeCaseColumnName();
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Complemento).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>().Property(lojaDominio => lojaDominio.Cep).HasSnakeCaseColumnName().HasMaxLength(8).IsUnicode(false);
            modelBuilder.Entity<LojaDominio>()
                .HasOne(endereco => endereco.Municipio)
                .WithMany()
                .HasForeignKey(documento => documento.IdMunicipio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LojaDominio>()
                .HasOne(endereco => endereco.TipoLogradouro)
                .WithMany()
                .HasForeignKey(documento => documento.IdTipoLogradouro)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TelefoneLojaDominio>());
            modelBuilder.Entity<TelefoneLojaDominio>().Property(telefoneLojaDominio => telefoneLojaDominio.Telefone).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<TelefoneLojaDominio>().Property(telefoneLojaDominio => telefoneLojaDominio.PossuiContaWhatsApp).HasColumnName("POSSUI_CONTA_WHATSAPP");
            modelBuilder.Entity<TelefoneLojaDominio>().Property(telefoneLojaDominio => telefoneLojaDominio.MensagemApresentacao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<TelefoneLojaDominio>()
                .HasOne(telefoneLojaDominio => telefoneLojaDominio.Loja)
                .WithMany(lojaDominio => lojaDominio.Telefones)
                .HasForeignKey(telefone => telefone.IdLoja);
            modelBuilder.Entity<TelefoneLojaDominio>().Property(telefoneLojaDominio => telefoneLojaDominio.IdLoja).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ParametroOperacaoDominio>());
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.InstituicaoFinanceira).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.IdConvenio).HasSnakeCaseColumnName();
            modelBuilder.Entity<ParametroOperacaoDominio>()
                .HasOne(parametro => parametro.Convenio)
                .WithMany()
                .HasForeignKey(parametro => parametro.IdConvenio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.IdTipoOperacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<ParametroOperacaoDominio>()
                .HasOne(parametro => parametro.TipoOperacao)
                .WithMany()
                .HasForeignKey(parametro => parametro.IdTipoOperacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.QuantidadeParcelas).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.TaxaPlano).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ParametroOperacaoDominio>().Property(parametro => parametro.TentativaRetencao).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<AnexoDominio>());
            modelBuilder.Entity<AnexoDominio>().Property(anexo => anexo.Link).HasSnakeCaseColumnName().HasMaxLength(4000).IsUnicode(false);
            modelBuilder.Entity<AnexoDominio>().Property(anexo => anexo.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<AnexoDominio>().Property(anexo => anexo.IdTipoDocumento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<AnexoDominio>().Property(anexo => anexo.DataCadastro).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<AnexoDominio>().Property(anexo => anexo.Extensao).HasSnakeCaseColumnName().HasMaxLength(5).IsUnicode(false);
            modelBuilder.Entity<AnexoDominio>()
                .HasOne(anexo => anexo.Cliente)
                .WithMany(clienteDominio => clienteDominio.Anexos)
                .HasForeignKey(anexo => anexo.IdCliente);
            modelBuilder.Entity<AnexoDominio>()
                .HasOne(anexo => anexo.TipoDocumento)
                .WithMany()
                .HasForeignKey(anexo => anexo.IdTipoDocumento);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SolicitacaoDocumentoDominio>());
            modelBuilder.Entity<SolicitacaoDocumentoDominio>().Property(solicitacao => solicitacao.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SolicitacaoDocumentoDominio>().Property(solicitacao => solicitacao.Concluido).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SolicitacaoDocumentoDominio>().Property(solicitacao => solicitacao.IdTipoDocumento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SolicitacaoDocumentoDominio>().Property(solicitacao => solicitacao.DataSolicitacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SolicitacaoDocumentoDominio>().Property(solicitacao => solicitacao.Motivo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(500);
            modelBuilder.Entity<SolicitacaoDocumentoDominio>()
                .HasOne(solicitacao => solicitacao.Cliente)
                .WithMany()
                .HasForeignKey(solicitacao => solicitacao.IdCliente);
            modelBuilder.Entity<SolicitacaoDocumentoDominio>()
                .HasOne(solicitacao => solicitacao.TipoDocumento)
                .WithMany()
                .HasForeignKey(solicitacao => solicitacao.IdTipoDocumento);

            var entidadeConvenio = setPropriedadesDeEntidadeBase(modelBuilder.Entity<ConvenioDominio>(), false);
            modelBuilder.Entity<ConvenioDominio>().Property(convenio => convenio.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ConvenioDominio>().Property(convenio => convenio.Codigo).HasSnakeCaseColumnName().HasMaxLength(6).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ConvenioDominio>().Property(convenio => convenio.Descricao).HasSnakeCaseColumnName().HasMaxLength(150).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ConvenioDominio>().Property(convenio => convenio.ID).HasSnakeCaseColumnName(entidadeConvenio).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<ConvenioDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SiapeTipoFuncionalDominio>());
            modelBuilder.Entity<SiapeTipoFuncionalDominio>().Property(convenio => convenio.Descricao).HasSnakeCaseColumnName().HasMaxLength(60).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SiapeTipoFuncionalDominio>().Property(convenio => convenio.Codigo).HasSnakeCaseColumnName().HasMaxLength(1).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<InssEspecieBeneficioDominio>());
            modelBuilder.Entity<InssEspecieBeneficioDominio>().Property(convenio => convenio.Descricao).HasSnakeCaseColumnName().HasMaxLength(200).IsUnicode(false).IsRequired();
            modelBuilder.Entity<InssEspecieBeneficioDominio>().Property(convenio => convenio.Codigo).HasSnakeCaseColumnName().HasMaxLength(3).IsUnicode(false).IsRequired();

            var entidadeProduto = setPropriedadesDeEntidadeBase(modelBuilder.Entity<ProdutoDominio>(), false);
            modelBuilder.Entity<ProdutoDominio>().Property(produto => produto.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ProdutoDominio>().Property(produto => produto.Sigla).HasSnakeCaseColumnName().HasMaxLength(5).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ProdutoDominio>().Property(produto => produto.RequerConvenio).HasSnakeCaseColumnName().IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<ProdutoDominio>().Property(produto => produto.ID).HasSnakeCaseColumnName(entidadeProduto).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<ProdutoDominio>().HasKey(e => e.ID);

            var entidadeTipoOperacao = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoOperacaoDominio>(), false);
            modelBuilder.Entity<TipoOperacaoDominio>().Property(tipoOperacao => tipoOperacao.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoOperacaoDominio>().Property(tipoOperacao => tipoOperacao.Sigla).HasSnakeCaseColumnName().HasMaxLength(5).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoOperacaoDominio>().Property(tipoOperacao => tipoOperacao.ID).HasSnakeCaseColumnName(entidadeTipoOperacao).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TipoOperacaoDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>());
            modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>().Property(situacao => situacao.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>().Property(situacao => situacao.DescricaoPadrao).HasSnakeCaseColumnName().HasMaxLength(250).IsUnicode(false).IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>().Property(situacao => situacao.PermiteAtualizacoes).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>().Property(situacao => situacao.PermiteSituacaoExtraordinaria).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoDominio>().Property(situacao => situacao.SituacaoExtraordinaria).HasSnakeCaseColumnName().IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoDominio>());
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdTipoOperacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdLoja).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdLead).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdProduto).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdUsuario).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.IdRendimentoCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.Prestacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.Proposta).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false);
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.ValorAuxilioFinanceiro).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.TaxaMes).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.TaxaAno).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.Prazo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.ValorFinanciado).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.ImpostoOperacaoFinanceira).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.CustoEfetivoTotalMes).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.CustoEfetivoTotalAno).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.DataInclusao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>().Property(intencao => intencao.PrimeiroVencimento).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.Usuario)
                .WithMany(usuarioDominio => usuarioDominio.IntencoesOperacao)
                .HasForeignKey(intencao => intencao.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.Lead)
                .WithMany(leadDominio => leadDominio.IntencoesOperacao)
                .HasForeignKey(intencao => intencao.IdLead)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.Loja)
                .WithMany(lojaDominio => lojaDominio.IntencoesOperacao)
                .HasForeignKey(intencao => intencao.IdLoja)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.TipoOperacao)
                .WithMany()
                .HasForeignKey(intencao => intencao.IdTipoOperacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.Produto)
                .WithMany()
                .HasForeignKey(intencao => intencao.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoDominio>()
                .HasOne(intencao => intencao.RendimentoCliente)
                .WithMany(rendimento => rendimento.IntencoesOperacao)
                .HasForeignKey(intencao => intencao.IdRendimentoCliente)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>());
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.IdIntencaoOperacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.IdBancoOriginador).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.PrazoTotal).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.PrazoRestante).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.SaldoDevedor).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.PlanoRefinanciamento).HasMaxLength(4).IsUnicode(false).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.PrazoRefinanciamento).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().Property(intencao => intencao.ValorPrestacaoRefinanciamento).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>().HasIndex(intencao => intencao.IdIntencaoOperacao).IsUnique();
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>()
                .HasOne(intencao => intencao.IntencaoOperacao)
                .WithOne(intencaoOperacao => intencaoOperacao.Portabilidade)
                .HasForeignKey<IntencaoOperacaoPortabilidadeDominio>(intencao => intencao.IdIntencaoOperacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoPortabilidadeDominio>()
                .HasOne(intencao => intencao.BancoOriginador)
                .WithMany()
                .HasForeignKey(intencao => intencao.IdBancoOriginador)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoContratoDominio>());
            modelBuilder.Entity<IntencaoOperacaoContratoDominio>().Property(contrato => contrato.Contrato).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<IntencaoOperacaoContratoDominio>().Property(contrato => contrato.IdIntencaoOperacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoContratoDominio>().HasIndex(contrato => new { contrato.IdIntencaoOperacao, contrato.Contrato }).IsUnique();
            modelBuilder.Entity<IntencaoOperacaoContratoDominio>()
                .HasOne(contrato => contrato.IntencaoOperacao)
                .WithMany(intencao => intencao.Contratos)
                .HasForeignKey(contrato => contrato.IdIntencaoOperacao)
                .OnDelete(DeleteBehavior.Cascade);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoObservacaoDominio>());
            modelBuilder.Entity<IntencaoOperacaoObservacaoDominio>().Property(contrato => contrato.IdIntencaoOperacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoObservacaoDominio>().Property(observacao => observacao.DataInclusao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoObservacaoDominio>().Property(observacao => observacao.Observacao).HasSnakeCaseColumnName().HasMaxLength(400).IsUnicode(false).IsRequired();
            modelBuilder.Entity<IntencaoOperacaoObservacaoDominio>()
                .HasOne(observacao => observacao.IntencaoOperacao)
                .WithMany(intencao => intencao.Observacoes)
                .HasForeignKey(observacao => observacao.IdIntencaoOperacao)
                .OnDelete(DeleteBehavior.Cascade);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>());
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>().Property(historico => historico.IdIntencaoOperacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>().Property(historico => historico.IdIntencaoOperacaoSituacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>().Property(historico => historico.DescricaoEspecifica).HasSnakeCaseColumnName().HasMaxLength(250).IsUnicode(false);
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>().Property(historico => historico.PendenciaUsuario).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>()
                .HasOne(historico => historico.IntencaoOperacao)
                .WithMany(intencao => intencao.Historico)
                .HasForeignKey(historico => historico.IdIntencaoOperacao)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IntencaoOperacaoHistoricoDominio>()
                .HasOne(historico => historico.SituacaoIntencaoOperacao)
                .WithMany()
                .HasForeignKey(historico => historico.IdIntencaoOperacaoSituacao)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>());
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.IdProduto).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.IdTipoOperacao).HasSnakeCaseColumnName().IsRequired().HasDefaultValue(Dominio.Enum.TipoOperacao.Novo);
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.IdIntencaoOperacaoSituacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.IdProximoPasso).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.IdProximoPassoExcecao).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>().Property(passo => passo.PassoInicial).HasSnakeCaseColumnName();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasIndex(passo => new { passo.IdProduto, passo.IdTipoOperacao, passo.IdIntencaoOperacaoSituacao })
                .IsUnique();
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasIndex(passo => new { passo.IdProduto, passo.PassoInicial, passo.IdTipoOperacao })
                .IsUnique()
                .HasFilter($"[{nameof(IntencaoOperacaoSituacaoPassoDominio.PassoInicial).CastToUpperSnakeCase()}] = 1");
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasOne(passo => passo.Produto)
                .WithMany(produto => produto.PassosIntencaoOperacao)
                .HasForeignKey(passo => passo.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasOne(passo => passo.TipoOperacao)
                .WithMany(tipoOperacao => tipoOperacao.PassosIntencaoOperacao)
                .HasForeignKey(passo => passo.IdTipoOperacao)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasOne(passo => passo.SituacaoIntencaoOperacao)
                .WithMany()
                .HasForeignKey(passo => passo.IdIntencaoOperacaoSituacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasOne(passo => passo.ProximoPasso)
                .WithMany()
                .HasForeignKey(passo => passo.IdProximoPasso)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IntencaoOperacaoSituacaoPassoDominio>()
                .HasOne(passo => passo.ProximoPassoExcecao)
                .WithMany()
                .HasForeignKey(passo => passo.IdProximoPassoExcecao)
                .OnDelete(DeleteBehavior.NoAction);

            var entidadeTipoConta = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoContaDominio>(), false);
            modelBuilder.Entity<TipoContaDominio>().Property(tipoConta => tipoConta.ID).HasSnakeCaseColumnName(entidadeTipoConta).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TipoContaDominio>().Property(tipoConta => tipoConta.Nome).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoContaDominio>().Property(tipoConta => tipoConta.Sigla).HasSnakeCaseColumnName().HasMaxLength(2).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoContaDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoVinculoInstitucionalDominio>());
            modelBuilder.Entity<TipoVinculoInstitucionalDominio>().Property(tipoVinculoInstitucional => tipoVinculoInstitucional.Nome).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UnidadeFederativaDominio>());
            modelBuilder.Entity<UnidadeFederativaDominio>().Property(m => m.Nome).HasSnakeCaseColumnName().HasMaxLength(40).IsUnicode(false).IsRequired();
            modelBuilder.Entity<UnidadeFederativaDominio>().Property(m => m.Sigla).HasSnakeCaseColumnName().HasMaxLength(2).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<BancoDominio>());
            modelBuilder.Entity<BancoDominio>().Property(m => m.Nome).HasSnakeCaseColumnName().HasMaxLength(60).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BancoDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().HasMaxLength(4).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BancoDominio>().Property(m => m.CNPJ).HasSnakeCaseColumnName().HasMaxLength(14).IsUnicode(false);
            modelBuilder.Entity<BancoDominio>().Property(m => m.PermitePortabilidade).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<BancoDominio>().HasIndex(m => m.Codigo).IsUnique();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ConvenioOrgaoDominio>());
            modelBuilder.Entity<ConvenioOrgaoDominio>().Property(m => m.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ConvenioOrgaoDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().HasMaxLength(5).IsUnicode(false).IsRequired();
            modelBuilder.Entity<ConvenioOrgaoDominio>().Property(m => m.CNPJ).HasSnakeCaseColumnName().HasMaxLength(14).IsUnicode(false);
            modelBuilder.Entity<ConvenioOrgaoDominio>().HasIndex(m => m.Codigo).IsUnique();
            modelBuilder.Entity<ConvenioOrgaoDominio>().Property(m => m.IdUnidadeFederativa).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConvenioOrgaoDominio>()
                .HasOne(orgao => orgao.UF)
                .WithMany()
                .HasForeignKey(orgao => orgao.IdUnidadeFederativa)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ConvenioOrgaoDominio>().Property(m => m.IdConvenio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ConvenioOrgaoDominio>()
                .HasOne(orgao => orgao.Convenio)
                .WithMany()
                .HasForeignKey(orgao => orgao.IdConvenio)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<EstadoCivilDominio>());
            modelBuilder.Entity<EstadoCivilDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false).IsRequired();
            modelBuilder.Entity<EstadoCivilDominio>().Property(m => m.Sigla).HasSnakeCaseColumnName().HasMaxLength(1).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<GrauInstrucaoDominio>());
            modelBuilder.Entity<GrauInstrucaoDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false).IsRequired();

            var entidadeTipoDocumento = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoDocumentoDominio>(), false);
            modelBuilder.Entity<TipoDocumentoDominio>().Property(m => m.Nome).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoDocumentoDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoDocumentoDominio>().Property(m => m.TipoDocumentoIdentificacaoPessoal).HasSnakeCaseColumnName().IsRequired(true).HasDefaultValue(false);
            modelBuilder.Entity<TipoDocumentoDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeTipoDocumento).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TipoDocumentoDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<OrgaoEmissorIdentificacaoDominio>());
            modelBuilder.Entity<OrgaoEmissorIdentificacaoDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<OrgaoEmissorIdentificacaoDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<GeneroDominio>());
            modelBuilder.Entity<GeneroDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(30).IsUnicode(false).IsRequired();
            modelBuilder.Entity<GeneroDominio>().Property(m => m.Sigla).HasSnakeCaseColumnName().HasMaxLength(1).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<MunicipioDominio>());
            modelBuilder.Entity<MunicipioDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(40).IsUnicode(false).IsRequired();
            modelBuilder.Entity<MunicipioDominio>().Property(m => m.IdUnidadeFederativa).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<MunicipioDominio>()
                .HasOne(municipio => municipio.UF)
                .WithMany()
                .HasForeignKey(municipio => municipio.IdUnidadeFederativa)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoLogradouroDominio>());
            modelBuilder.Entity<TipoLogradouroDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(40).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoLogradouroDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().HasMaxLength(12).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<BaseCepDominio>());
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.Logradouro).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.Bairro).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.CEP).HasSnakeCaseColumnName().HasMaxLength(8).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.InformacaoAdicional).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.PermiteAjusteLogradouro).HasSnakeCaseColumnName();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.IdUnidadeFederativa).HasSnakeCaseColumnName();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.IdMunicipio).HasSnakeCaseColumnName();
            modelBuilder.Entity<BaseCepDominio>().Property(m => m.IdTipoLogradouro).HasSnakeCaseColumnName();
            modelBuilder.Entity<BaseCepDominio>()
                .HasOne(cep => cep.UF)
                .WithMany()
                .HasForeignKey(cep => cep.IdUnidadeFederativa)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BaseCepDominio>()
                .HasOne(cep => cep.Municipio)
                .WithMany()
                .HasForeignKey(cep => cep.IdMunicipio)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BaseCepDominio>()
                .HasOne(cep => cep.TipoLogradouro)
                .WithMany()
                .HasForeignKey(cep => cep.IdTipoLogradouro)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BaseCepDominio>()
                .HasIndex(cep => cep.CEP)
                .IsUnique();

            var entidadeTipo = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateEmailTipoDominio>(), false);
            modelBuilder.Entity<TemplateEmailTipoDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeTipo).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TemplateEmailTipoDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TemplateEmailTipoDominio>().HasKey(e => e.ID);

            var entidadeFinalidade = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateEmailFinalidadeDominio>(), false);
            modelBuilder.Entity<TemplateEmailFinalidadeDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeFinalidade).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TemplateEmailFinalidadeDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TemplateEmailFinalidadeDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateEmailDominio>());
            modelBuilder.Entity<TemplateEmailDominio>().Property(template => template.Conteudo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TemplateEmailDominio>().Property(template => template.IdTipo).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<TemplateEmailDominio>().Property(template => template.IdFinalidade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<TemplateEmailDominio>()
                .HasOne(template => template.TemplateEmailTipo)
                .WithMany()
                .HasForeignKey(template => template.IdTipo)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TemplateEmailDominio>()
                .HasOne(template => template.TemplateEmailFinalidade)
                .WithMany()
                .HasForeignKey(template => template.IdFinalidade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TemplateEmailDominio>()
                .HasIndex(template => new { template.IdTipo, template.IdFinalidade })
                .IsUnique();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroEmailDominio>());
            modelBuilder.Entity<RegistroEmailDominio>().Property(email => email.IdFinalidade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<RegistroEmailDominio>().Property(email => email.Destinatarios).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<RegistroEmailDominio>().Property(email => email.CodigoOrigem).HasSnakeCaseColumnName().HasConversion<int>().IsUnicode(false);
            modelBuilder.Entity<RegistroEmailDominio>().Property(email => email.IdUsuario).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroEmailDominio>()
                .HasOne(email => email.TemplateEmailFinalidade)
                .WithMany()
                .HasForeignKey(email => email.IdFinalidade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RegistroEmailDominio>()
                .HasOne(email => email.Usuario)
                .WithMany()
                .HasForeignKey(email => email.IdUsuario);


            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroSmsDominio>());
            modelBuilder.Entity<RegistroSmsDominio>().Property(sms => sms.IdTemplateSms).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<RegistroSmsDominio>().Property(sms => sms.NumeroTelefone).HasSnakeCaseColumnName().HasMaxLength(15).IsUnicode(false).IsRequired().IsRequired();
            modelBuilder.Entity<RegistroSmsDominio>().Property(sms => sms.Mensagem).HasSnakeCaseColumnName().HasMaxLength(200).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RegistroSmsDominio>().Property(sms => sms.CodigoOrigem).HasSnakeCaseColumnName().HasConversion<int>().IsUnicode(false);
            modelBuilder.Entity<RegistroSmsDominio>().Property(sms => sms.IdUsuario).HasSnakeCaseColumnName().HasConversion<int>();
            modelBuilder.Entity<RegistroSmsDominio>()
                .HasOne(sms => sms.TemplateSMS)
                .WithMany()
                .HasForeignKey(sms => sms.IdTemplateSms)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RegistroSmsDominio>()
                .HasOne(sms => sms.Usuario)
                .WithMany()
                .HasForeignKey(sms => sms.IdUsuario);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroTorpedoVozDominio>());
            modelBuilder.Entity<RegistroTorpedoVozDominio>().Property(torpedo => torpedo.IdTemplateTorpedoVoz).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<RegistroTorpedoVozDominio>().Property(torpedo => torpedo.NumeroTelefone).HasSnakeCaseColumnName().HasMaxLength(15).IsUnicode(false).IsRequired().IsRequired();
            modelBuilder.Entity<RegistroTorpedoVozDominio>().Property(torpedo => torpedo.Mensagem).HasSnakeCaseColumnName().HasMaxLength(500).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RegistroTorpedoVozDominio>().Property(torpedo => torpedo.CodigoOrigem).HasSnakeCaseColumnName().HasConversion<int>().IsUnicode(false);
            modelBuilder.Entity<RegistroTorpedoVozDominio>().Property(torpedo => torpedo.IdUsuario).HasSnakeCaseColumnName().HasConversion<int>();
            modelBuilder.Entity<RegistroTorpedoVozDominio>()
                .HasOne(torpedo => torpedo.TemplateTorpedoVoz)
                .WithMany()
                .HasForeignKey(torpedo => torpedo.IdTemplateTorpedoVoz)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RegistroTorpedoVozDominio>()
                .HasOne(torpedo => torpedo.Usuario)
                .WithMany()
                .HasForeignKey(torpedo => torpedo.IdUsuario);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroWhatsappDominio>());
            modelBuilder.Entity<RegistroWhatsappDominio>().Property(whatsapp => whatsapp.IdTemplateWhatsapp).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<RegistroWhatsappDominio>().Property(whatsapp => whatsapp.NumeroTelefone).HasSnakeCaseColumnName().HasMaxLength(15).IsUnicode(false).IsRequired().IsRequired();
            modelBuilder.Entity<RegistroWhatsappDominio>().Property(whatsapp => whatsapp.ParametrosMensagem).HasSnakeCaseColumnName().HasMaxLength(400).IsUnicode(false).IsRequired();
            modelBuilder.Entity<RegistroWhatsappDominio>().Property(whatsapp => whatsapp.CodigoOrigem).HasSnakeCaseColumnName().HasConversion<int>().IsUnicode(false);
            modelBuilder.Entity<RegistroWhatsappDominio>().Property(whatsapp => whatsapp.IdUsuario).HasSnakeCaseColumnName().HasConversion<int>();
            modelBuilder.Entity<RegistroWhatsappDominio>()
                .HasOne(whatsapp => whatsapp.TemplateWhatsapp)
                .WithMany()
                .HasForeignKey(whatsapp => whatsapp.IdTemplateWhatsapp)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RegistroWhatsappDominio>()
                .HasOne(whatsapp => whatsapp.Usuario)
                .WithMany()
                .HasForeignKey(whatsapp => whatsapp.IdUsuario);


            var entidadeTipoTermo = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoTermoDominio>(), false);
            modelBuilder.Entity<TipoTermoDominio>().Property(tipo => tipo.Nome).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<TipoTermoDominio>().HasIndex(tipo => tipo.Nome).IsUnique();
            modelBuilder.Entity<TipoTermoDominio>().Property(tipo => tipo.ID).HasSnakeCaseColumnName(entidadeTipoTermo).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TipoTermoDominio>().HasKey(tipo => tipo.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TermoDominio>());
            modelBuilder.Entity<TermoDominio>().Property(termo => termo.IdTipoTermo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TermoDominio>().Property(termo => termo.Nome).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<TermoDominio>().Property(termo => termo.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(8000);
            modelBuilder.Entity<TermoDominio>().Property(termo => termo.DataInicioVigencia).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TermoDominio>()
                .HasOne(termo => termo.TipoTermo)
                .WithMany()
                .HasForeignKey(termo => termo.IdTipoTermo)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<UsuarioTermoDominio>());
            modelBuilder.Entity<UsuarioTermoDominio>().Property(termo => termo.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioTermoDominio>().Property(termo => termo.IdTermo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<UsuarioTermoDominio>()
                .HasOne(usuarioTermo => usuarioTermo.Usuario)
                .WithMany()
                .HasForeignKey(usuarioTermo => usuarioTermo.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UsuarioTermoDominio>()
                .HasOne(usuarioTermo => usuarioTermo.Termo)
                .WithMany(termo => termo.UsuariosTermos)
                .HasForeignKey(usuarioTermo => usuarioTermo.IdTermo)
                .OnDelete(DeleteBehavior.NoAction);

            var entidadeNotificacaoFinalidade = setPropriedadesDeEntidadeBase(modelBuilder.Entity<NotificacaoFinalidadeDominio>(), false);
            modelBuilder.Entity<NotificacaoFinalidadeDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeNotificacaoFinalidade).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<NotificacaoFinalidadeDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<NotificacaoFinalidadeDominio>().HasKey(e => e.ID);

            var entidadeNotificacaoSeveridade = setPropriedadesDeEntidadeBase(modelBuilder.Entity<NotificacaoSeveridadeDominio>(), false);
            modelBuilder.Entity<NotificacaoSeveridadeDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeNotificacaoSeveridade).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<NotificacaoSeveridadeDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<NotificacaoSeveridadeDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<NotificacaoTemplateDominio>());
            modelBuilder.Entity<NotificacaoTemplateDominio>().Property(template => template.Titulo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<NotificacaoTemplateDominio>().Property(template => template.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<NotificacaoTemplateDominio>().Property(template => template.UrlReferencia).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<NotificacaoTemplateDominio>().Property(template => template.IdNotificacaoSeveridade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<NotificacaoTemplateDominio>().Property(template => template.IdNotificacaoFinalidade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<NotificacaoTemplateDominio>()
                .HasOne(template => template.Finalidade)
                .WithMany()
                .HasForeignKey(template => template.IdNotificacaoFinalidade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NotificacaoTemplateDominio>()
                .HasOne(template => template.Severidade)
                .WithMany()
                .HasForeignKey(template => template.IdNotificacaoSeveridade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NotificacaoTemplateDominio>()
                .HasIndex(template => template.IdNotificacaoFinalidade)
                .IsUnique();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<NotificacaoDominio>());
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.IdUsuario).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.IdTemplateNotificacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.Titulo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.UrlReferencia).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.DataVisualizacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.DataCriacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.Completo).HasSnakeCaseColumnName();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.IdNotificacaoSeveridade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.IdNotificacaoFinalidade).HasSnakeCaseColumnName().HasConversion<int>().IsRequired();
            modelBuilder.Entity<NotificacaoDominio>().Property(notificacao => notificacao.IdTemplateNotificacao).HasSnakeCaseColumnName().HasConversion<int>();
            modelBuilder.Entity<NotificacaoDominio>()
                .HasOne(notificacao => notificacao.Finalidade)
                .WithMany()
                .HasForeignKey(template => template.IdNotificacaoFinalidade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NotificacaoDominio>()
                .HasOne(template => template.Severidade)
                .WithMany()
                .HasForeignKey(template => template.IdNotificacaoSeveridade)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NotificacaoDominio>()
                .HasOne(template => template.Template)
                .WithMany()
                .HasForeignKey(template => template.IdTemplateNotificacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NotificacaoDominio>()
                .HasOne(template => template.Usuario)
                .WithMany()
                .HasForeignKey(template => template.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>());
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>().Property(consulta => consulta.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>().Property(consulta => consulta.IdPaperlessDocumento).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>().Property(consulta => consulta.IdAnexoArquivoTermo).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>().Property(consulta => consulta.ChaveAutorizacao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(200);
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>().Property(consulta => consulta.DataGeracaoArquivoTermo).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>()
                .HasOne(consulta => consulta.Cliente)
                .WithMany()
                .HasForeignKey(consulta => consulta.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ConsultaBeneficioInssClienteDominio>()
                .HasOne(consulta => consulta.AnexoArquivoTermo)
                .WithMany()
                .HasForeignKey(consulta => consulta.IdAnexoArquivoTermo)
                .OnDelete(DeleteBehavior.NoAction);

            var redeSocial = setPropriedadesDeEntidadeBase(modelBuilder.Entity<RedeSocialDominio>(), false);
            modelBuilder.Entity<RedeSocialDominio>().Property(e => e.ID).HasSnakeCaseColumnName(redeSocial).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<RedeSocialDominio>().Property(e => e.Nome).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<RedeSocialDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>());
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.Nome).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.Sobrenome).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.NomeMae).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.Email).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.TelefoneCompleto).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.Motivo).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false).IsRequired();
            modelBuilder.Entity<SolicitacaoAcessoDadosPessoaisClienteDominio>().Property(solicitacao => solicitacao.NotificacaoEnviada).HasSnakeCaseColumnName().IsRequired();

            var tipoSolicitacaoConfirmacao = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoSolicitacaoConfirmacaoDominio>(), false);
            modelBuilder.Entity<TipoSolicitacaoConfirmacaoDominio>().Property(e => e.ID).HasSnakeCaseColumnName(tipoSolicitacaoConfirmacao).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TipoSolicitacaoConfirmacaoDominio>().Property(e => e.Nome).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<TipoSolicitacaoConfirmacaoDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>());
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.IdTipoSolicitacaoConfirmacao).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.IdTelefoneCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.Token).HasSnakeCaseColumnName().HasMaxLength(4).IsRequired();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.Enviada).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.DataEnvioSolicitacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.MensagemErro).HasSnakeCaseColumnName().HasColumnType("varchar(max)");
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>().Property(solicitacao => solicitacao.QuantidadeEnviosEfetuados).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>()
               .HasOne(solicitacao => solicitacao.TipoSolicitacaoConfirmacao)
               .WithMany()
               .HasForeignKey(solicitacao => solicitacao.IdTipoSolicitacaoConfirmacao)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TelefoneClienteSolicitacaoConfirmacaoDominio>()
               .HasOne(solicitacao => solicitacao.TelefoneCliente)
               .WithMany()
               .HasForeignKey(solicitacao => solicitacao.IdTelefoneCliente)
               .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<BeneficioInssMensagemDeParaDominio>());
            modelBuilder.Entity<BeneficioInssMensagemDeParaDominio>().Property(mensagem => mensagem.CodigoOriginal).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BeneficioInssMensagemDeParaDominio>().Property(mensagem => mensagem.MensagemOriginal).HasSnakeCaseColumnName().HasMaxLength(4000).IsUnicode(false).IsRequired();
            modelBuilder.Entity<BeneficioInssMensagemDeParaDominio>().Property(mensagem => mensagem.MensagemTratada).HasSnakeCaseColumnName().HasMaxLength(4000).IsUnicode(false);
            modelBuilder.Entity<BeneficioInssMensagemDeParaDominio>().HasIndex(mensagem => mensagem.CodigoOriginal).IsUnique();
       
       
            entidadeTipo = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateSmsDominio>(), false);
            modelBuilder.Entity<TemplateSmsDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeTipo).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TemplateSmsDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<TemplateSmsDominio>().Property(e => e.Conteudo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(160);
            modelBuilder.Entity<TemplateSmsDominio>().HasKey(e => e.ID);

            entidadeTipo = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateTorpedoVozDominio>(), false);
            modelBuilder.Entity<TemplateTorpedoVozDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeTipo).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TemplateTorpedoVozDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().IsRequired().IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<TemplateTorpedoVozDominio>().Property(e => e.Conteudo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(500);
            modelBuilder.Entity<TemplateTorpedoVozDominio>().HasKey(e => e.ID);

            entidadeTipo = setPropriedadesDeEntidadeBase(modelBuilder.Entity<TemplateWhatsappDominio>(), false);
            modelBuilder.Entity<TemplateWhatsappDominio>().Property(e => e.ID).HasSnakeCaseColumnName(entidadeTipo).HasConversion<int>().ValueGeneratedNever();
            modelBuilder.Entity<TemplateWhatsappDominio>().Property(e => e.Descricao).HasSnakeCaseColumnName().HasMaxLength(50).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TemplateWhatsappDominio>().Property(e => e.GUID).HasSnakeCaseColumnName().HasMaxLength(20).IsUnicode(false);
            modelBuilder.Entity<TemplateWhatsappDominio>().HasKey(e => e.ID);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProdutoDominio>());
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.Nome).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.Descricao).HasSnakeCaseColumnName().IsUnicode(false).IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.ValorPremio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.DataFimVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.DataInicioVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.Nome).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroProdutoDominio>().Property(seguro => seguro.IdProduto).HasSnakeCaseColumnName().IsRequired();

            modelBuilder.Entity<SeguroProdutoDominio>()
                .HasOne(seguroProduto => seguroProduto.Produto)
                .WithMany()
                .HasForeignKey(seguroProduto => seguroProduto.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroCoberturaDominio>());
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.CodigoCobertura).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.Tipo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.ValorCapital).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.ValorPremio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.TipoProponente).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>().Property(seguro => seguro.IdSeguroProduto).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaDominio>()
                .HasOne(seguroCobertura => seguroCobertura.SeguroProduto)
                .WithMany()
                .HasForeignKey(seguroCobertura => seguroCobertura.IdSeguroProduto)
                .OnDelete(DeleteBehavior.NoAction);
            
            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroCoberturaIcatuDominio>());
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.CodigoCobertura).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.Tipo).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.ValorCapital).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.ValorPremio).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.TipoProponente).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>().Property(seguro => seguro.IdSeguroProdutoIcatu).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroCoberturaIcatuDominio>()
                .HasOne(seguroCoberturaIcatu => seguroCoberturaIcatu.SeguroProdutoIcatu)
                .WithMany()
                .HasForeignKey(seguroCoberturaIcatu => seguroCoberturaIcatu.IdSeguroProdutoIcatu)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProdutoIcatuDominio>());
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.CodigoGrupoApolice).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.DataInicioVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.DataFimVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.Modulo).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.Subestipulante).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.IdParceria).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.TipoNumeracaoProposta).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.ValorPremio).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.CodigoPontoVenda).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(2000);
            modelBuilder.Entity<SeguroProdutoIcatuDominio>().Property(seguro => seguro.IdSeguroProduto).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroProdutoIcatuDominio>()
               .HasOne(seguroCobertura => seguroCobertura.SeguroProduto)
               .WithMany()
               .HasForeignKey(seguroCobertura => seguroCobertura.IdSeguroProduto)
               .OnDelete(DeleteBehavior.NoAction);


            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroPropostaDominio>());
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.DataAssinatura).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.DataInicioVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.DataFimVigencia).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.DataProtocolo).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.DataVencimento).HasSnakeCaseColumnName().HasColumnType("date").IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.ValorPremioTotal).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.UrlPdfContrato).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.Longitude).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.Latitude).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.IPOrigem).HasSnakeCaseColumnName().HasMaxLength(19).IsUnicode(false);
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.ValorPrimeiroPremioPago).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.IdCliente).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.NumeroPropostaIcatu).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>().Property(seguro => seguro.IdSeguroProduto).HasSnakeCaseColumnName().IsRequired();
            modelBuilder.Entity<SeguroPropostaDominio>()
                .HasOne(seguroProposta => seguroProposta.Cliente)
                .WithOne(cliente => cliente.SeguroProposta)
                .HasForeignKey<SeguroPropostaDominio>(seguroProposta => seguroProposta.IdCliente);
            modelBuilder.Entity<SeguroPropostaDominio>()
                .HasOne(seguroProposta => seguroProposta.Produto)
                .WithMany()
                .HasForeignKey(seguroProposta => seguroProposta.IdSeguroProduto);
            modelBuilder.Entity<SeguroPropostaDominio>()
                .HasOne(seguroProposta => seguroProposta.Produto)
                .WithMany()
                .HasForeignKey(seguroProposta => seguroProposta.IdSeguroProduto);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroParentescoDominio>());
            modelBuilder.Entity<SeguroParentescoDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<SeguroParentescoDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroParentescoIcatuDominio>());
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroParentescoIcatuDominio>().Property(mensagem => mensagem.IdSeguroParentescoBem).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroParentescoIcatuDominio>()
                .HasOne(spi => spi.SeguroParentescoBem)
                .WithOne()
                .HasForeignKey<SeguroParentescoIcatuDominio>(spi => spi.IdSeguroParentescoBem);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProfissaoDominio>());
            modelBuilder.Entity<SeguroProfissaoDominio>().Property(seguroProfissao => seguroProfissao.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(8000);
            modelBuilder.Entity<SeguroProfissaoDominio>().Property(seguroProfissao => seguroProfissao.Codigo).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProfissaoIcatuDominio>());
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(seguroProfissao => seguroProfissao.Descricao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(8000);
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(seguroProfissao => seguroProfissao.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(seguroProfissao => seguroProfissao.IdSeguroProfissaoBem).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>()
                .HasOne(spi => spi.SeguroProfissaoBem)
                .WithOne()
                .HasForeignKey<SeguroProfissaoIcatuDominio>(spi => spi.IdSeguroProfissaoBem);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ConjugeDominio>());
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.CPF).HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.Nome).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.IdGenero).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConjugeDominio>().Property(conjuge => conjuge.IdTipoRegimeCasamento).HasSnakeCaseColumnName();
            modelBuilder.Entity<ConjugeDominio>()
                .HasOne(conjuge => conjuge.Cliente)
                .WithOne(cliente => cliente.Conjuge)
                .HasForeignKey<ConjugeDominio>(conjuge => conjuge.IdCliente);

            modelBuilder.Entity<ConjugeDominio>()
                .HasOne(conjuge => conjuge.TipoRegimeCasamento)
                .WithMany()
                .HasForeignKey(conjuge => conjuge.IdTipoRegimeCasamento);

            modelBuilder.Entity<ConjugeDominio>()
               .HasOne(conjuge => conjuge.Genero)
               .WithMany()
               .HasForeignKey(conjuge => conjuge.IdGenero);
            
            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoRegimeCasamentoDominio>());
            modelBuilder.Entity<TipoRegimeCasamentoDominio>().Property(convenio => convenio.Descricao).HasSnakeCaseColumnName().HasMaxLength(150).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<TipoRegimeCasamentoBemDominio>());
            modelBuilder.Entity<TipoRegimeCasamentoBemDominio>().Property(convenio => convenio.Descricao).HasSnakeCaseColumnName().HasMaxLength(150).IsUnicode(false).IsRequired();
            modelBuilder.Entity<TipoRegimeCasamentoBemDominio>().Property(mensagem => mensagem.IdTipoRegimeCasamento).HasSnakeCaseColumnName();
            modelBuilder.Entity<TipoRegimeCasamentoBemDominio>()
                .HasOne(conjuge => conjuge.TipoRegimeCasamento)
                .WithMany()
                .HasForeignKey(conjuge => conjuge.IdTipoRegimeCasamento);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroProfissaoIcatuDominio>());
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.Descricao).HasSnakeCaseColumnName().HasMaxLength(8000).IsUnicode(false);
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>().Property(mensagem => mensagem.IdSeguroProfissaoBem).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroProfissaoIcatuDominio>()
                .HasOne(spi => spi.SeguroProfissaoBem)
                .WithOne()
                .HasForeignKey<SeguroProfissaoIcatuDominio>(spi => spi.IdSeguroProfissaoBem);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>());
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.DataVencimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.IdConvenio).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.LinkPagamento).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(255);
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.IdPedidoPagamento).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(255);
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.IdLinkPagamento).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(255);
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.IdCobranca).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(255);
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>().Property(seguroCobranca => seguroCobranca.IdSeguroPropostaIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroCobrancaPropostaIcatuDominio>()
                .HasOne(spi => spi.SeguroPropostaIcatu)
                .WithMany()
                .HasForeignKey(spi => spi.IdSeguroPropostaIcatu);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroSituacaoIcatuDominio>());
            modelBuilder.Entity<SeguroSituacaoIcatuDominio>().Property(seguroSituacao => seguroSituacao.Status).HasSnakeCaseColumnName().HasMaxLength(4000).IsUnicode(false);
            modelBuilder.Entity<SeguroSituacaoIcatuDominio>().Property(seguroSituacao => seguroSituacao.IdSeguroPropostaIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroSituacaoIcatuDominio>()
                .HasOne(spi => spi.SeguroPropostaIcatu)
                .WithMany()
                .HasForeignKey(spi => spi.IdSeguroPropostaIcatu);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroPropostaIcatuDominio>());
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.DataAssinatura).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.DataInicioVigencia).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.DataFimVigencia).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.DataProtocolo).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.ValorPremioTotal).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.ValorPrimeiroPremioPago).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.NumeroPropostaIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.IdSeguroProposta).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaIcatuDominio>().Property(seguroProposta => seguroProposta.IdSeguroClienteIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroPropostaIcatuDominio>()
                .HasOne(spi => spi.SeguroProposta)
                .WithOne()
                .HasForeignKey<SeguroPropostaIcatuDominio>(spi => spi.IdSeguroProposta);
            modelBuilder.Entity<SeguroPropostaIcatuDominio>()
                .HasOne(spi => spi.SeguroClienteIcatu)
                .WithMany()
                .HasForeignKey(spi => spi.IdSeguroClienteIcatu);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>());
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.IdCartao).IsUnicode(false).HasSnakeCaseColumnName().HasMaxLength(4000);
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.IdCobranca).IsUnicode(false).HasSnakeCaseColumnName().HasMaxLength(4000);
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.Titular).IsUnicode(false).HasSnakeCaseColumnName().HasMaxLength(255);
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.CpfTitular).IsUnicode(false).HasSnakeCaseColumnName().HasMaxLength(14);
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.QuatroUltimosDigitosCartao).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(4);
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>().Property(mensagem => mensagem.IdSeguroCobrancaPropostaIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroCobrancaPropostaCartaoIcatuDominio>()
                .HasOne(spi => spi.SeguroCobrancaPropostaIcatu)
                .WithOne()
                .HasForeignKey<SeguroCobrancaPropostaCartaoIcatuDominio>(spi => spi.IdSeguroCobrancaPropostaIcatu);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroClienteIcatuDominio>());
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.Nome).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.Email).HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.PPE).HasSnakeCaseColumnName().IsUnicode(false).HasMaxLength(1);
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.Nacionalidade).IsUnicode(false).HasMaxLength(100).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.RendaMensal).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.ResidentePais).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.RelacionamentoEletronico).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.Aposentado).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdEstadoCivil).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdGenero).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdProfissaoIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdEnderecoPrincipal).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>().Property(mensagem => mensagem.IdEnderecoCobranca).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(sci => sci.Cliente)
                .WithOne()
                .HasForeignKey<SeguroClienteIcatuDominio>(spi => spi.IdCliente);
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(sci => sci.EstadoCivil)
                .WithMany()
                .HasForeignKey(spi => spi.IdEstadoCivil);
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(sci => sci.Genero)
                .WithMany()
                .HasForeignKey(spi => spi.IdGenero);
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(sci => sci.SeguroProfissaoIcatu)
                .WithMany()
                .HasForeignKey(spi => spi.IdProfissaoIcatu);
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(seguroProposta => seguroProposta.EnderecoPrincipal)
                .WithMany()
                .HasForeignKey(seguroProposta => seguroProposta.IdEnderecoPrincipal);
            modelBuilder.Entity<SeguroClienteIcatuDominio>()
                .HasOne(seguroProposta => seguroProposta.EnderecoCobranca)
                .WithMany()
                .HasForeignKey(seguroProposta => seguroProposta.IdEnderecoCobranca);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroEnderecoClienteDominio>());
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(mensagem => mensagem.Cep).HasSnakeCaseColumnName().HasMaxLength(8).IsUnicode(false);
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(seguroEndereco => seguroEndereco.Bairro).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(seguroEndereco => seguroEndereco.Complemento).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false);
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(seguroEndereco => seguroEndereco.Logradouro).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(seguroEndereco => seguroEndereco.Numero).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroEnderecoClienteDominio>().Property(seguroEndereco => seguroEndereco.IdMunicipio).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroEnderecoClienteDominio>()
                .HasOne(sec => sec.Municipio)
                .WithMany()
                .HasForeignKey(sec => sec.IdMunicipio)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroBeneficiarioDominio>());
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.Nome).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.CPF).HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.ValorPercentual).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroProposta).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroParentesco).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioDominio>()
                .HasOne(sb => sb.SeguroProposta)
                .WithMany()
                .HasForeignKey(sb => sb.IdSeguroProposta);
            modelBuilder.Entity<SeguroBeneficiarioDominio>()
                .HasOne(sec => sec.SeguroCliente)
                .WithMany()
                .HasForeignKey(sec => sec.IdSeguroCliente);
            modelBuilder.Entity<SeguroBeneficiarioDominio>()
                .HasOne(sec => sec.SeguroParentesco)
                .WithMany()
                .HasForeignKey(sec => sec.IdSeguroParentesco);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>());
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.Nome).HasSnakeCaseColumnName().HasMaxLength(255).IsUnicode(false);
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.CPF).HasMaxLength(50).IsUnicode(false);
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.DataNascimento).HasSnakeCaseColumnName().HasColumnType("date");
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.ValorPercentual).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroProposta).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroClienteIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>().Property(seguroBeneficiario => seguroBeneficiario.IdSeguroParentescoIcatu).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>()
                .HasOne(sb => sb.SeguroPropostaIcatu)
                .WithMany()
                .HasForeignKey(sb => sb.IdSeguroProposta);
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>()
                .HasOne(sec => sec.SeguroClienteIcatu)
                .WithMany()
                .HasForeignKey(sec => sec.IdSeguroClienteIcatu);
            modelBuilder.Entity<SeguroBeneficiarioIcatuDominio>()
                .HasOne(sec => sec.SeguroParentescoIcatu)
                .WithMany()
                .HasForeignKey(sec => sec.IdSeguroParentescoIcatu);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<SeguroClienteTelefoneDominio>());
            modelBuilder.Entity<SeguroClienteTelefoneDominio>().Property(seguroEndereco => seguroEndereco.DDD).HasMaxLength(3).IsUnicode(false).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteTelefoneDominio>().Property(seguroEndereco => seguroEndereco.Fone).HasMaxLength(9).IsUnicode(false).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteTelefoneDominio>().Property(seguroEndereco => seguroEndereco.Deletado).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteTelefoneDominio>().Property(seguroEndereco => seguroEndereco.Principal).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteTelefoneDominio>().Property(seguroEndereco => seguroEndereco.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<SeguroClienteTelefoneDominio>()
                .HasOne(sec => sec.Cliente)
                .WithMany(sci => sci.Telefones)
                .HasForeignKey(sec => sec.IdCliente);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<BiometriaClienteDominio>());
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.Score).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.Valido).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.IdBiometriaSituacao).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.DataEnvio).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>().Property(biometria => biometria.DataRetorno).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaClienteDominio>()
                .HasOne(biometria => biometria.Cliente)
                .WithOne(cliente => cliente.BiometriaCliente)
                .HasForeignKey<BiometriaClienteDominio>(biometria => biometria.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BiometriaClienteDominio>()
                .HasOne(biometria => biometria.RegistroBiometriaUnico)
                .WithOne(registro => registro.BiometriaCliente)
                .HasForeignKey<BiometriaClienteDominio>(biometria => biometria.IdRegistroBiometriaUnico)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroBiometriaUnicoDominio>());
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.Codigo).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.DataEnvio).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.DataRetorno).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.Score).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.Liveness).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.FaceMatch).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.PossuiBiometria).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.CodigoSituacaoBiometria).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>().Property(biometria => biometria.CodigoErro).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroBiometriaUnicoDominio>()
                .HasOne(biometria => biometria.Cliente)
                .WithMany()
                .HasForeignKey(biometria => biometria.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<BiometriaSituacaoDominio>());
            modelBuilder.Entity<BiometriaSituacaoDominio>().Property(biometria => biometria.ID).HasSnakeCaseColumnName();
            modelBuilder.Entity<BiometriaSituacaoDominio>().Property(biometria => biometria.Descricao).HasSnakeCaseColumnName();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<RegistroClubeBeneficiosDominio>());
            modelBuilder.Entity<RegistroClubeBeneficiosDominio>().Property(clubeBeneficios => clubeBeneficios.IdCliente).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroClubeBeneficiosDominio>().Property(clubeBeneficios => clubeBeneficios.DataAcesso).HasSnakeCaseColumnName();
            modelBuilder.Entity<RegistroClubeBeneficiosDominio>()
                .HasOne(clubeBeneficios => clubeBeneficios.Cliente)
                .WithOne()
                .HasForeignKey<RegistroClubeBeneficiosDominio>(clubeBeneficios => clubeBeneficios.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<MarinhaCargoDominio>());
            modelBuilder.Entity<MarinhaCargoDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(60).IsUnicode(false).IsRequired();
            modelBuilder.Entity<MarinhaCargoDominio>().Property(m => m.Sigla).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<MarinhaCargoDominio>().Property(m => m.Codigo).HasSnakeCaseColumnName().IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<MarinhaTipoFuncionalDominio>());
            modelBuilder.Entity<MarinhaTipoFuncionalDominio>().Property(m => m.Descricao).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<MarinhaTipoFuncionalDominio>().Property(m => m.Sigla).HasSnakeCaseColumnName().HasMaxLength(1).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<AeronauticaCargoDominio>());
            modelBuilder.Entity<AeronauticaCargoDominio>().Property(a => a.Descricao).HasSnakeCaseColumnName().HasMaxLength(60).IsUnicode(false).IsRequired();
            modelBuilder.Entity<AeronauticaCargoDominio>().Property(a => a.Sigla).HasSnakeCaseColumnName().HasMaxLength(10).IsUnicode(false).IsRequired();
            modelBuilder.Entity<AeronauticaCargoDominio>().Property(a => a.Codigo).HasSnakeCaseColumnName().IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<AeronauticaTipoFuncionalDominio>());
            modelBuilder.Entity<AeronauticaTipoFuncionalDominio>().Property(a => a.Descricao).HasSnakeCaseColumnName().HasMaxLength(100).IsUnicode(false).IsRequired();
            modelBuilder.Entity<AeronauticaTipoFuncionalDominio>().Property(a => a.Sigla).HasSnakeCaseColumnName().HasMaxLength(1).IsUnicode(false).IsRequired();

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<MeioPagamentoSeguroDominio>());
            modelBuilder.Entity<MeioPagamentoSeguroDominio>().Property(clubeBeneficios => clubeBeneficios.IdMeioPagamento).HasSnakeCaseColumnName();
            modelBuilder.Entity<MeioPagamentoSeguroDominio>()
                .HasOne(meioPagamento => meioPagamento.Produto)
                .WithMany()
                .HasForeignKey(meioPagamento => meioPagamento.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);

            setPropriedadesDeEntidadeBase(modelBuilder.Entity<ContaBancariaDominio>());
            modelBuilder.Entity<ContaBancariaDominio>().Property(clubeBeneficios => clubeBeneficios.DigitoVerificadorAgencia).HasSnakeCaseColumnName();
            modelBuilder.Entity<ContaBancariaDominio>().Property(clubeBeneficios => clubeBeneficios.Agencia).IsUnicode(false).HasMaxLength(20).HasSnakeCaseColumnName();
            modelBuilder.Entity<ContaBancariaDominio>().Property(clubeBeneficios => clubeBeneficios.NumeroConta).IsUnicode(false).HasMaxLength(255).HasSnakeCaseColumnName();
            modelBuilder.Entity<ContaBancariaDominio>()
                .HasOne(meioPagamento => meioPagamento.Banco)
                .WithMany()
                .HasForeignKey(meioPagamento => meioPagamento.IdBanco)
                .OnDelete(DeleteBehavior.NoAction);
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

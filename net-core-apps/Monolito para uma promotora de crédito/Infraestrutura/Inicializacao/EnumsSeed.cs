using Dominio;
using Dominio.Enum;
using Dominio.Enum.Notificacoes;
using Dominio.Enum.TemplateEmail;
using Dominio.Enum.TemplateSms;
using Dominio.Enum.TemplateWhatsapp;
using Dominio.Enum.TemplateTorpedoVoz;
using Infraestrutura.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Infraestrutura.Inicializacao
{
    [ExcludeFromCodeCoverage]
    public class EnumsSeed
    {
        public void SeedData(PlataformaClienteContexto contexto)
        {
            if (!contexto.Convenios.Any())
            {
                var opcoes = new ConvenioDominio[] {
                    new ConvenioDominio(Convenio.INSS, "INSS DATAPREV", Convenio.INSS.GetDescription(), "Disponível para Aposentados e pensionistas do INSS. O valor do empréstimo é descontado diretamente do benefício."),
                    new ConvenioDominio(Convenio.SIAPE, "SIAPE", Convenio.SIAPE.GetDescription(), "Disponível para Servidores Federais Civis. O valor do empréstimo é descontado diretamente da folha de pagamento."),
                };

                contexto.Convenios.AddRange(opcoes);
                contexto.SaveChanges();
            }

            if (!contexto.Produtos.Any())
            {
                var opcoes = new ProdutoDominio[] {
                    new ProdutoDominio(Produto.CreditoConsignado, "Crédito Consignado", Produto.CreditoConsignado.GetDescription(), true),
                    new ProdutoDominio(Produto.CartaoCreditoConsignado, "Cartão Crédito Consignado", Produto.CartaoCreditoConsignado.GetDescription(), false),
                };

                contexto.Produtos.AddRange(opcoes);
            }

            var tiposOperacao = EnumExtensions.ValoresEnum<TipoOperacao>();
            if (contexto.TiposOperacao.Count() < tiposOperacao.Count())
            {
                var novosTiposOperacao = tiposOperacao
                    .Select(e => new TipoOperacaoDominio(e, e.ToString(), e.GetDescription()))
                    .Where(e => !contexto.TiposOperacao.Any(t => t.ID == e.ID));

                contexto.TiposOperacao.AddRange(novosTiposOperacao);
            }

            var finalidades = EnumExtensions.ValoresEnum<TemplateEmailFinalidade>();
            if (contexto.TemplateEmailFinalidades.Count() < finalidades.Count())
            {
                var novasFinalidades = finalidades
                    .Select(e => new TemplateEmailFinalidadeDominio(e, e.ToString()))
                    .Where(d => !contexto.TemplateEmailFinalidades.Any(e => e.ID == d.ID));

                contexto.AddRange(novasFinalidades);
            }

            var tiposTemplateEmail = EnumExtensions.ValoresEnum<TemplateEmailTipo>();
            if (contexto.TemplateEmailTipos.Count() < tiposTemplateEmail.Count())
            {
                var novosTipos = tiposTemplateEmail
                    .Select(e => new TemplateEmailTipoDominio(e, e.ToString()))
                    .Where(d => !contexto.TemplateEmailTipos.Any(e => e.ID == d.ID));

                contexto.TemplateEmailTipos.AddRange(novosTipos);
            }

            var tiposTermos = EnumExtensions.ValoresEnum<Dominio.Enum.TipoTermo>();
            if (contexto.TiposTermo.Count() < tiposTermos.Count())
            {
                var novos = tiposTermos
                    .Select(e => new TipoTermoDominio(e, e.GetDescription()))
                    .Where(e => !contexto.TiposTermo.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var notificacaoFinalidades = EnumExtensions.ValoresEnum<NotificacaoFinalidade>();
            if (contexto.NotificacaoFinalidades.Count() < notificacaoFinalidades.Count())
            {
                var novos = notificacaoFinalidades
                    .Select(e => new NotificacaoFinalidadeDominio(e, e.ToString()))
                    .Where(e => !contexto.NotificacaoFinalidades.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var notificacaoSeveridades = EnumExtensions.ValoresEnum<NotificacaoSeveridade>();
            if (contexto.NotificacaoSeveridades.Count() < notificacaoSeveridades.Count())
            {
                var novos = notificacaoSeveridades
                    .Select(e => new NotificacaoSeveridadeDominio(e, e.ToString()))
                    .Where(e => !contexto.NotificacaoSeveridades.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var redesSociais = EnumExtensions.ValoresEnum<RedeSocial>();
            if (contexto.RedesSociais.Count() < redesSociais.Count())
            {
                var novasRedesSociais = redesSociais
                    .Select(e => new RedeSocialDominio(e, e.ToString()))
                    .Where(e => !contexto.RedesSociais.Any(t => t.ID == e.ID));

                contexto.RedesSociais.AddRange(novasRedesSociais);
            }

            var tiposDocumento = EnumExtensions.ValoresEnum<TipoDocumento>();
            if (contexto.TiposDocumento.Count() < tiposDocumento.Count())
            {
                var novos = tiposDocumento
                    .Select(e => new TipoDocumentoDominio(e, e.ToString(), e.GetDescription()))
                    .Where(e => !contexto.TiposDocumento.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var tipoSolicitacaoConfirmacao = EnumExtensions.ValoresEnum<TipoSolicitacaoConfirmacao>();
            if (contexto.TiposSolicitacaoConfirmacao.Count() < tipoSolicitacaoConfirmacao.Count())
            {
                var novos = tipoSolicitacaoConfirmacao
                    .Select(e => new TipoSolicitacaoConfirmacaoDominio(e, e.ToString()))
                    .Where(e => !contexto.TiposSolicitacaoConfirmacao.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var formasRecebimento = EnumExtensions.ValoresEnum<FormaRecebimento>();
            if (contexto.FormasRecebimento.Count() < formasRecebimento.Count())
            {
                var novos = formasRecebimento
                    .Select(e => new FormaRecebimentoDominio(e, e.ToString()))
                    .Where(e => !contexto.FormasRecebimento.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var tiposConta = EnumExtensions.ValoresEnum<TipoConta>();
            if (contexto.TiposConta.Count() < formasRecebimento.Count())
            {
                var novos = tiposConta
                    .Select(e => new TipoContaDominio(e, e.ToString(), e.GetDescription()))
                    .Where(e => !contexto.TiposConta.Any(t => t.ID == e.ID));

                contexto.AddRange(novos);
            }

            var templatesSms = EnumExtensions.ValoresEnum<TemplateSms>();
            if (contexto.TemplatesSms.Count() < templatesSms.Count())
            {
                var novosTipos = templatesSms
                    .Select(e => new TemplateSmsDominio(e, e.ToString()))
                    .Where(d => !contexto.TemplatesSms.Any(e => e.ID == d.ID));

                contexto.TemplatesSms.AddRange(novosTipos);
            }

            var templatesWhatsapp = EnumExtensions.ValoresEnum<TemplateWhatsapp>();
            if (contexto.TemplatesWhatsapp.Count() < templatesWhatsapp.Count())
            {
                var novosTipos = templatesWhatsapp
                    .Select(e => new TemplateWhatsappDominio(e, e.ToString()))
                    .Where(d => !contexto.TemplatesWhatsapp.Any(e => e.ID == d.ID));

                contexto.TemplatesWhatsapp.AddRange(novosTipos);
            }

            var templatesTorpedoVoz = EnumExtensions.ValoresEnum<TemplateTorpedoVoz>();
            if (contexto.TemplatesTorpedoVoz.Count() < templatesTorpedoVoz.Count())
            {
                var novosTipos = templatesTorpedoVoz
                    .Select(e => new TemplateTorpedoVozDominio(e, e.ToString()))
                    .Where(d => !contexto.TemplatesTorpedoVoz.Any(e => e.ID == d.ID));

                contexto.TemplatesTorpedoVoz.AddRange(novosTipos);
            }

            contexto.SaveChanges();
        }

    }
}

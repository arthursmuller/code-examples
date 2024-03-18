using Dominio.Resource;
using FluentValidation;
using System.Linq;

namespace Aplicacao.Model.IntencaoOperacao
{
    public class IntencaoOperacaoCriacaoModelValidacao : AbstractValidator<IntencaoOperacaoCriacaoModel>
    {
        public IntencaoOperacaoCriacaoModelValidacao()
        {
            RuleFor(x => x.IdTipoOperacao).NotEmpty();

            RuleFor(x => x.IdProduto).NotEmpty();

            RuleFor(x => x.Prestacao).NotEmpty();

            RuleFor(x => x.TaxaMes).NotEmpty();

            When(x => x.IdTipoOperacao == (int)Dominio.Enum.TipoOperacao.Refinanciamento, () =>
            {

                RuleFor(x => x.IdRendimentoCliente).NotEmpty();

                RuleFor(x => x.Contratos).NotEmpty();

            });

            When(x => x.IdTipoOperacao == (int)Dominio.Enum.TipoOperacao.Portabilidade, () =>
            {
                RuleFor(x => x.Contratos).NotEmpty();

                When(x => x.Contratos?.Any() ?? false, () =>
                {
                    RuleFor(x => x.Contratos).Must(x => x.Count().Equals(1)).WithMessage(Mensagens.IntencaoOperacao_ApenasUmContratoDeveSerInformado);
                });

                RuleFor(x => x.Portabilidade).NotEmpty();

                RuleFor(x => x.Portabilidade.IdBancoOriginador).NotEmpty();

                RuleFor(x => x.Portabilidade.PrazoRestante).NotEmpty();

                RuleFor(x => x.Portabilidade.PrazoTotal).NotEmpty();

                RuleFor(x => x.Portabilidade.SaldoDevedor).NotEmpty();

                When(DeveValidarPlanoPrazoIntencaoRefinanciamento, () =>
                {
                    RuleFor(x => x.Portabilidade.PlanoRefinanciamento).NotEmpty().Length(0, 4);
                    RuleFor(x => x.Portabilidade.PrazoRefinanciamento).NotEmpty().GreaterThan(0);
                });

                When(DeveValidarPrestacaoIntencaoRefinanciamento, () =>
                {
                    RuleFor(x => x.Portabilidade.ValorPrestacaoRefinanciamento).NotEmpty().GreaterThan(0);
                });

            });
        }

        private bool DeveValidarPlanoPrazoIntencaoRefinanciamento(IntencaoOperacaoCriacaoModel intencao) =>
            intencao.Portabilidade.ValorPrestacaoRefinanciamento > 0;

        private bool DeveValidarPrestacaoIntencaoRefinanciamento(IntencaoOperacaoCriacaoModel intencao) =>
            !string.IsNullOrWhiteSpace(intencao.Portabilidade.PlanoRefinanciamento)
            && intencao.Portabilidade.PrazoRefinanciamento > 0;
    }
}

using FluentValidation;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class ParametroSimulacaoNovoValidacao : AbstractValidator<ParametrosSimulacaoNovoDto>
    {
        public ParametroSimulacaoNovoValidacao()
        {
            When(p => (p.ValorOperacao ?? 0) > 0, () =>
            {
                RuleFor(p => p)
                    .Must(p => (p.Prestacao ?? 0) == 0)
                    .WithMessage("Deve ser informado apenas um dos valores: operação ou prestação.");
            });

            When(p => (p.ValorOperacao ?? 0) == 0, () =>
            {
                RuleFor(p => p)
                    .Must(p => (p.Prestacao ?? 0) > 0)
                    .WithMessage("Deve ser informado um dos valores: operação ou prestação.");
            });
        }
    }
}

using FluentValidation;

namespace Aplicacao.Model.RendimentoCliente
{
    public class RendimentoClienteModelValidacao : AbstractValidator<RendimentoClienteModel>
    {
        public RendimentoClienteModelValidacao()
        {
            RuleFor(x => x.Convenio).NotEmpty();

            RuleFor(x => x.IdConvenioOrgao).GreaterThan(0);

            RuleFor(x => x.IdUf).GreaterThan(0);

            When(x => x.Convenio == Dominio.Enum.Convenio.INSS, () =>
                {
                    RuleFor(x => x.IdInssEspecieBeneficio).GreaterThan(0);
                    RuleFor(x => x.Matricula).NotEmpty().Matches(@"^\d{10}$");
                }
            );

            When(x => x.Convenio == Dominio.Enum.Convenio.SIAPE, () =>
                {
                    RuleFor(x => x.IdSiapeTipoFuncional).GreaterThan(0);
                    RuleFor(x => x.Matricula).NotEmpty().Matches(@"^\d{7,10}$");
                }
            );

            When(x => x.Convenio == Dominio.Enum.Convenio.MARINHA, () =>
                {
                    RuleFor(x => x.IdMarinhaTipoFuncional).NotEmpty();
                }
            );

            When(x => x.Convenio == Dominio.Enum.Convenio.AERONAUTICA, () =>
                {
                    RuleFor(x => x.IdAeronauticaTipoFuncional).NotEmpty();
                }
            );

            RuleFor(x => x.ContaCliente.IdBanco).GreaterThan(0);

            RuleFor(x => x.ContaCliente.IdTipoConta).GreaterThan(0);

            RuleFor(x => x.ContaCliente.Agencia).NotEmpty();

            RuleFor(x => x.ContaCliente.Conta).NotEmpty();
        }
    }
}

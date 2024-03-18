using FluentValidation;

namespace Aplicacao.Model.Cliente
{
    public class ClienteModelValidacao : AbstractValidator<ClienteModel>
    {
        public ClienteModelValidacao()
        {
            RuleFor(x => x.IdGenero).GreaterThan(0);

            RuleFor(x => x.IdEstadoCivil).GreaterThan(0);

            RuleFor(x => x.IdGrauInstrucao).GreaterThan(0);

            RuleFor(x => x.IdCidadeNatal).GreaterThan(0);

            RuleFor(x => x.Nome).NotEmpty();

            RuleFor(x => x.DataNascimento).NotEmpty();
        }
    }
}

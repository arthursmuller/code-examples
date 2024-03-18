using FluentValidation;

namespace Aplicacao.Model.EnderecoCliente
{
    public class EnderecoClienteModelValidacao : AbstractValidator<EnderecoClienteModel>
    {
        public EnderecoClienteModelValidacao()
        {
            RuleFor(x => x.IdMunicipio).GreaterThan(0);

            RuleFor(x => x.IdTipoLogradouro).GreaterThan(0);
        }
    }
}

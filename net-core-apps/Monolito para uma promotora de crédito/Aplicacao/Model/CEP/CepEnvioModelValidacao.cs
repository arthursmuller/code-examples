using FluentValidation;

namespace Aplicacao.CEP
{
    public class CepEnvioModelValidacao : AbstractValidator<CepEnvioModel>
    {
        public CepEnvioModelValidacao()
        {
            RuleFor(x => x.IdUnidadeFederativa).NotEmpty();

            RuleFor(x => x.IdMunicipio).NotEmpty();

            RuleFor(x => x.Bairro).MinimumLength(3);

            RuleFor(x => x.Logradouro).MinimumLength(3);
        }
    }
}

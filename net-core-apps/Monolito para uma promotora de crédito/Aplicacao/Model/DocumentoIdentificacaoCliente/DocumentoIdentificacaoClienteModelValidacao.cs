using FluentValidation;

namespace Aplicacao.Model.DocumentoIdentificacaoCliente
{
    public class DocumentoIdentificacaoClienteModelValidacao : AbstractValidator<DocumentoIdentificacaoClienteModel>
    {
        public DocumentoIdentificacaoClienteModelValidacao()
        {
            RuleFor(x => x.IdTipoDocumento).GreaterThan(0);

            RuleFor(x => x.IdOrgaoEmissor).GreaterThan(0);

            RuleFor(x => x.IdUnidadeFederativa).GreaterThan(0);

            RuleFor(x => x.DataEmissao).NotEmpty();
        }
    }
}

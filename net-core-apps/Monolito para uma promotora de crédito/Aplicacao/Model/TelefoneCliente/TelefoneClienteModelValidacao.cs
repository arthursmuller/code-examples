using B.Mensagens.Interfaces;
using FluentValidation;
using SharedKernel.ValueObjects.v2;

namespace Aplicacao.Model.TelefoneCliente
{
    public class TelefoneClienteModelValidacao : AbstractValidator<TelefoneClienteModel>
    {
        private readonly IBemMensagens _mensagens;

        public TelefoneClienteModelValidacao(IBemMensagens mensagens)
        {
            _mensagens = mensagens;

            RuleFor(x => new { x.DDD, x.Fone }).Must(x => Fone.IsValid(x.DDD, x.Fone, _mensagens));
        }
    }
}

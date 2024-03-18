using Dominio;
using SharedKernel.Enums;
using Telefone = SharedKernel.ValueObjects.v2.Fone;

namespace Aplicacao.Model.TelefoneCliente
{
    public class TelefoneClienteExibicaoModel
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public string DDD { get; set; }

        public string Fone { get; set; }

        public bool Principal { get; set; }

        public bool Confirmado { get; set; }

        public EnumCodigoTipoFone TipoTelefone { get; set; }

        public TelefoneClienteExibicaoModel() { }

        public TelefoneClienteExibicaoModel(TelefoneClienteDominio telefone, bool principal = false)
        {
            if (telefone != null)
            {
                Id = telefone.ID;
                IdCliente = telefone.IdCliente;
                DDD = telefone.DDD;
                Fone = telefone.Fone;
                Principal = principal;
                Confirmado = telefone.Confirmado;
                TipoTelefone = Telefone.CalcularCodigoTipoFone(Fone);
            }
        }
    }
}

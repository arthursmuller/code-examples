using Aplicacao.Model.TipoVinculoInstitucional;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TipoVinculoInstitucionalServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly PlataformaClienteContexto _contexto;

        public TipoVinculoInstitucionalServico(IBemMensagens mensagens, PlataformaClienteContexto contexto)
        {
            _mensagens = mensagens;
            _contexto = contexto;
        }

        public async Task<IEnumerable<TipoVinculoInstitucionalModel>> ListarTipoVinculoInstitucional()
        {
            var tiposVinculoInstitucional = await _contexto.TiposVinculoInsticional
                .AsNoTracking()
                .OrderBy(t => t.Nome)
                .ToListAsync();

            if (tiposVinculoInstitucional == null)
            {
                _mensagens.AdicionarErro(Mensagens.TipoVinculoInstitucional_NenhumEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return tiposVinculoInstitucional.Select(i =>
                new TipoVinculoInstitucionalModel
                {
                    Id = i.ID,
                    Nome = i.Nome
                });
        }
    }
}

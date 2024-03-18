using Aplicacao.Model.TipoOperacao;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TipoOperacaoServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly PlataformaClienteContexto _contexto;

        public TipoOperacaoServico(IBemMensagens mensagens, PlataformaClienteContexto contexto)
        {
            _mensagens = mensagens;
            _contexto = contexto;
        }

        public async Task<TipoOperacaoModel> BuscarTipoOperacao(int id)
        {
            var tipoOperacao = await _contexto.TiposOperacao.AsNoTracking().FirstOrDefaultAsync(c => (int)c.ID == id);

            if (tipoOperacao == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.TipoOperacao_IdNaoEncontrado), EnumMensagemTipo.banco);

                return null;
            }

            return ConverterParaModel(tipoOperacao);
        }

        public async Task<IEnumerable<TipoOperacaoModel>> ListarTiposOperacao()
        {
            var tiposOperacao = await _contexto.TiposOperacao
                .AsNoTracking()
                .OrderBy(t => t.Nome)
                .ToListAsync();

            if (tiposOperacao == null)
            {
                _mensagens.AdicionarErro(Mensagens.TipoOperacao_NenhumEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return converterProdutosParaModel(tiposOperacao);
        }

        public static TipoOperacaoModel ConverterParaModel(TipoOperacaoDominio tipoOperacao)
        {
            return new TipoOperacaoModel
            {
                ID = (int)tipoOperacao.ID,
                Nome = tipoOperacao.Nome,
                Sigla = tipoOperacao.Sigla
            };
        }

        private static IEnumerable<TipoOperacaoModel> converterProdutosParaModel(List<TipoOperacaoDominio> tiposOperacao)
        {
            return tiposOperacao.Select(x => ConverterParaModel(x));
        }
    }
}

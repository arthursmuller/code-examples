using Aplicacao.Model.Banco;
using Aplicacao.Model.TipoConta;
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
    public class BancarioServico : ServicoBase
    {
        public BancarioServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto) : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<TipoContaModel>> ListarTiposConta()
        {
            var tiposConta = await _contexto.TiposConta
                .AsNoTracking()
                .OrderBy(t => t.Nome)
                .ToListAsync();

            if (tiposConta == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Bancario_TipoContaNaoEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return tiposConta.Select(i
                => new TipoContaModel
                {
                    Id = (int)i.ID,
                    Nome = i.Nome,
                    Sigla = i.Sigla
                });
        }

        public async Task<TipoContaModel> ObterTipoContaPorSigla(string sigla)
        {
            var tiposConta = await _contexto.TiposConta
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Sigla.Equals(sigla));

            if (tiposConta == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Bancario_TipoContaNaoEncontrado, EnumMensagemTipo.banco);

                return new TipoContaModel();
            }

            return new TipoContaModel
            {
                Id = (int)tiposConta.ID,
                Nome = tiposConta.Nome,
                Sigla = tiposConta.Sigla
            };
        }

        public async Task<IEnumerable<BancoModel>> ListarBancos()
        {
            var bancos = await _contexto.Bancos
                .AsNoTracking()
                .OrderBy(b => b.Nome)
                .ToListAsync();

            if (bancos == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Bancario_BancoNaoEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return bancos.Select(i
                => new BancoModel
                {
                    Id = i.ID,
                    Nome = i.Nome,
                    Codigo = i.Codigo
                });
        }

        public async Task<BancoModel> ObterBancoPorCodigo(string codigo)
        {
            var banco = await _contexto.Bancos
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Codigo.Equals(codigo));

            if (banco == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Bancario_BancoNaoEncontrado, EnumMensagemTipo.banco);

                return new BancoModel();
            }

            return new BancoModel
            {
                Id = banco.ID,
                Nome = banco.Nome,
                Codigo = banco.Codigo
            };
        }
    }
}

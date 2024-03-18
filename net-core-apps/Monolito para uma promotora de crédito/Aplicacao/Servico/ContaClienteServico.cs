using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacao.Model.ContaCliente;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Servico
{
    public class ContaClienteServico : ServicoBase, IContaClienteServico
    {
        public ContaClienteServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto
        ) : base(mensagens, usuarioLogin, contexto) {}

        public async Task<IEnumerable<ContaClienteExibicaoModel>> ListarContasAutenticado()
        {
            var cliente = await _contexto.Clientes
                .Include(c => c.Contas.Where(conta => !conta.Deletado))
                .FirstOrDefaultAsync(c => c.IdUsuario == _usuarioLogin.IdUsuario);

            return cliente.Contas.Select(conta => new ContaClienteExibicaoModel(conta));
        }

        public async Task<bool> ExcluirContaAutenticado(int idConta)
        {
            var conta = await _contexto.ContasCliente
                .Include(conta => conta.Cliente)
                .Include(conta => conta.Rendimentos.Where(r => !r.Deletado))
                .Include(conta => conta.RendimentosRecebimento.Where(r => !r.Deletado))
                .FirstOrDefaultAsync(c => c.ID == idConta);

            if (conta is null || conta.Cliente.IdUsuario != _usuarioLogin.IdUsuario) {
                _mensagens.AdicionarErro(Mensagens.Conta_NaoLocalizada, EnumMensagemTipo.banco);
                return false;
            }

            if (conta.Rendimentos.Any() || conta.RendimentosRecebimento.Any()) {
                _mensagens.AdicionarErro(Mensagens.Conta_EmUso, EnumMensagemTipo.banco);
                return false;
            }

            conta.SetDeletado();

            await SaveChangesAsync();

            return true;
        }

        public async Task<ContaClienteDominio> CriarConta(ContaClienteModel requisicao, int idCliente) 
        {
            var novaConta = new ContaClienteDominio(
                idCliente,
                requisicao.IdBanco,
                (TipoConta)requisicao.IdTipoConta,
                requisicao.Agencia,
                requisicao.Conta,
                requisicao.IdFormaRecebimento.HasValue ? (FormaRecebimento)requisicao.IdFormaRecebimento.Value : FormaRecebimento.TED
            );

            await _contexto.AddAsync(novaConta);
            await SaveChangesAsync();

            return novaConta;
        }

        public async Task<bool> VerificarContaCliente(int idConta, int idCliente)
        {
            var contaPertenceAoCliente = await _contexto.Clientes
                .Include(c => c.Contas)
                .Where(c => c.ID == idCliente)
                .AsNoTracking()
                .AnyAsync(c => c.Contas.Any(conta => conta.ID == idConta));

            if (!contaPertenceAoCliente)
            {
                _mensagens.AdicionarErro(Mensagens.Conta_NaoLocalizada, EnumMensagemTipo.banco);
            }

            return contaPertenceAoCliente;
        }
    }
}

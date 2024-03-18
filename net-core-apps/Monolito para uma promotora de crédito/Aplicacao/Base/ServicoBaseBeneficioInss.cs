using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Dto;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;
using SharedKernel.ValueObjects.v2;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ServicoBaseBeneficioInss : ServicoBaseSuporteAutenticacao
    {
        public ServicoBaseBeneficioInss(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, IProviderAutenticacao providerAutenticacao,
            ConfiguracaoProviders configuracaoProviders) : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders) { }

        protected async Task<ConsultaBeneficioInssClienteDominio> ObterConsultaBeneficio(int idConsultaBeneficio)
        {
            return await _contexto.ConsultaBeneficiosInssCliente
                            .Where(c => c.ID.Equals(idConsultaBeneficio) && c.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
        }

        protected async Task<TelefoneClienteDominio> ObterTelefoneCelular(int idTelefone, int idCliente)
        {
            var telefone = await _contexto.TelefonesCliente
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(t => t.ID.Equals(idTelefone) && t.IdCliente == idCliente);

            if (telefone == null) 
            {
                _mensagens.AdicionarErro(Mensagens.Telefone_NaoLocalizado, B.Mensagens.EnumMensagemTipo.banco);
                return null;
            }
            else if (Fone.CalcularCodigoTipoFone(telefone.Fone) != EnumCodigoTipoFone.Celular)
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_TelefoneDeveSerCelular, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            return telefone;
        }
    }
}

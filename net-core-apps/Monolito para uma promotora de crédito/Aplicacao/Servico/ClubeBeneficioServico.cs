using Aplicacao.Interfaces;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Kaledo;
using Infraestrutura.Providers.Kaledo.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ClubeBeneficioServico : ServicoBase, IClubeBeneficioServico
    {
        private readonly IProviderKaledo _provider;

        public ClubeBeneficioServico(PlataformaClienteContexto contexto,
            IUsuarioLogin usuarioLogin,
            IBemMensagens mensagens,
            IProviderKaledo provider) : base(mensagens, usuarioLogin, contexto)
            => _provider = provider;

        public async Task<string> CriarAutenticarUsuario()
        {
            var usuarioAutenticado = await ObterDadosUsuarioAutenticado();

            var cliente = await _contexto.Clientes
                            .Where(cliente => cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            if (!cliente.OperacaoAtiva.HasValue || !cliente.OperacaoAtiva.Value)
            {
                _mensagens.AdicionarErro(Mensagens.Cliente_ClubeBeneficioOperacaoInativa, EnumMensagemTipo.formulario);
                return null;
            }

            var resultado = await _provider.CriarAutenticarUsuario(new KaledoCriarAutenticarUsuarioDTO { document = usuarioAutenticado.CPF });


            if (resultado == null || !resultado.Success) {
                _mensagens.AdicionarErro(Mensagens.ProviderKaledo_NaoHouveSucessoNoRetornoDoProvedorCriarAuntenticarUsuario, EnumMensagemTipo.formulario);
                return null;
            }

            RegistroClubeBeneficiosDominio registroClubeBeneficios = new RegistroClubeBeneficiosDominio(cliente.ID);

            return resultado.Data;
        }

    }
}

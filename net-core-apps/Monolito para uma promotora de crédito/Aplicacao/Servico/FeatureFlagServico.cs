using Aplicacao.Model.FeatureFlags;
using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class FeatureFlagServico : ServicoBase
    {
        public FeatureFlagServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto
        ) : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<FeatureFlagModel>> ConsultarChaves()
        {
            var flags = await _contexto.FeatureFlags
                .AsNoTracking()
                .ToListAsync();

            return flags.Select(flag => new FeatureFlagModel(flag.Chave, flag.Habilitado));
        }

        public async Task<FeatureFlagModel> AdicionarOuAtualizar(FeatureFlagModel requisicao)
        {
            var flag = await _contexto
                .FeatureFlags
                .FirstOrDefaultAsync(f => f.Chave == requisicao.Chave.ToUpper());

            if (flag == null)
            {
                flag = new FeatureFlagDominio(requisicao.Chave, requisicao.Habilitado);
                await _contexto.AddAsync(flag);
            } else {
                flag.atualizarHabilitado(requisicao.Habilitado);
            }

            await _contexto.SaveChangesAsync();

            return new FeatureFlagModel(flag.Chave, flag.Habilitado);
        }
    }
}

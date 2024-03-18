using B.Comunicacao.Interfaces;
using B.Mensagens;
using B.Mensagens.Interfaces;
using B.Models;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infraestrutura.Consulta
{
    public class BeneficioInssMensagemDeParaQuery
    {
        private readonly PlataformaClienteContexto _contexto;
        private readonly IBemMensagens _mensagens;

        public BeneficioInssMensagemDeParaQuery(PlataformaClienteContexto contexto, IBemMensagens mensagens)
        {
            _contexto = contexto;
            _mensagens = mensagens;
        }

        public async Task ObterMensagemTratada(IConectaResponse response)
        {
            var erros = buscarListaDeErros(response);
            if (erros is null)
                return;

            var codigosErro = erros.Select(e => extrairCodigoMensagem(e.Mensagem)).Distinct();

            var mensagens = await _contexto
                                    .BeneficioInssMensagensDePara
                                    .AsNoTracking()
                                    .Where(mensagem =>
                                        codigosErro.Contains(mensagem.CodigoOriginal)
                                        && !string.IsNullOrWhiteSpace(mensagem.MensagemTratada))
                                    .ToListAsync();

            adicionarMensagemTratada(mensagens);
        }

        private IEnumerable<IMensagemBase> buscarListaDeErros(IConectaResponse response) =>
            response is null 
            ? null  
            : JsonConvert.DeserializeObject<RetornoApiDto<object>>(response.Content)?.Erros;
            
        private string extrairCodigoMensagem(string mensagem) => 
            Regex.Match(mensagem, @"[^\s-]*")?.Value;

        private void adicionarMensagemTratada(List<BeneficioInssMensagemDeParaDominio> mensagens) =>
            mensagens.ForEach(m => {
                _mensagens.AdicionarErro(m.MensagemTratada, EnumMensagemTipo.negocio); });

    }
}

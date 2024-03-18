using B.Comunicacao;
using B.Comunicacao.Interfaces;
using B.Mensagens.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infraestrutura.Providers.Unico.Autenticacao;
using Infraestrutura.Providers.Unico.DTO;
using System.Diagnostics.CodeAnalysis;
using Infraestrutura.Providers.Dto;
using Microsoft.Extensions.Logging;
using Infraestrutura.Providers.IcatuApi;

namespace Infraestrutura.Providers.Unico
{
	[ExcludeFromCodeCoverage]
	public class UnicoProvider : ProviderBase, IUnicoProvider
	{
		private readonly IBemMensagens _mensagens;
		private readonly IConecta _conecta;
		private readonly UnicoConfiguracao _unicoConfiguracao;
		private readonly UnicoAutenticacaoProvider _unicoAutenticacaoProvider;

		public UnicoProvider(IClienteConecta clienteConecta, IConecta conecta, IBemMensagens mensagens, ConfiguracaoProviders configuracaoProviders, ILogger<ProviderIcatu> logger, UnicoConfiguracao configuracao)
            : base(clienteConecta, conecta, mensagens, configuracaoProviders, logger) 
		{
			_conecta = conecta;
			_unicoConfiguracao = configuracao;
			_unicoAutenticacaoProvider = new UnicoAutenticacaoProvider(_conecta, _unicoConfiguracao, clienteConecta);
		}


		public async Task<UnicoResultadoBuscaProcessoDto> BuscarProcesso(string codigoProcesso)
		{
			var token = await _unicoAutenticacaoProvider.ObterTokenAcesso();
			var conectaRequisicao = _conecta
				.Get()
				.AddHeader("APIKEY", _unicoConfiguracao.ChaveApi)
				.AddHeader("Authorization", token)
				.AddNomeApi("Unico")
				.AddUrlApi(_unicoConfiguracao.UrlBaseApi)
				.AddUrlMetodo($"{_unicoConfiguracao.UrlProcessos}/{codigoProcesso}");

			var resposta = await _clienteConecta.Executar(conectaRequisicao);
			if (resposta.StatusCode == HttpStatusCode.OK)
				return JsonConvert.DeserializeObject<UnicoResultadoBuscaProcessoDto>(resposta.Content);

			var retornoErro = JsonConvert.DeserializeObject<UnicoResultadoCriarProcessoDto>(resposta.Content);
			_mensagens.AdicionarErro(retornoErro.Error.Description);
			return null;
		}

		public async Task<UnicoResultadoCriarProcessoDto> CriarProcesso(UnicoRequisicaoCriarProcessoDto dto)
		{
			var token = await _unicoAutenticacaoProvider.ObterTokenAcesso();
			var conectaRequisicao = _conecta
				.Post()
				.AddHeader("APIKEY", _unicoConfiguracao.ChaveApi)
				.AddHeader("Authorization", token)
				.AddBody(dto)
				.AddNomeApi("Unico")
				.AddUrlApi(_unicoConfiguracao.UrlBaseApi)
				.AddUrlMetodo(_unicoConfiguracao.UrlProcessos);

			var resposta = await _clienteConecta.Executar(conectaRequisicao);
			return JsonConvert.DeserializeObject<UnicoResultadoCriarProcessoDto>(resposta.Content);
		}
	}
}

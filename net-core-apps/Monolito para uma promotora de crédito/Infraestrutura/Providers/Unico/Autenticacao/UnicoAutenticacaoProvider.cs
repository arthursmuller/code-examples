using B.Comunicacao;
using B.Comunicacao.Interfaces;
using Infraestrutura.Providers.Unico.DTO;
using Infraestrutura.Providers.Unico.Utils;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Dominio.Resource;
using System.Text.RegularExpressions;

namespace Infraestrutura.Providers.Unico.Autenticacao
{

	[ExcludeFromCodeCoverage]
	public class UnicoAutenticacaoProvider
	{
		private const int TAMANHO_CHAVE_RSA = 2048;
		private readonly IConecta _conecta;
		private readonly UnicoConfiguracao _unicoConfigucacao;
		protected readonly IClienteConecta _clienteConecta;

        public UnicoAutenticacaoProvider(IConecta conecta
                                        , UnicoConfiguracao configuracao, IClienteConecta clienteConecta)
        {
            _conecta = conecta;
            _unicoConfigucacao = configuracao;
            _clienteConecta = clienteConecta;
        }

        public static long UnixEpoch => DateTimeOffset.Now.ToUnixTimeSeconds();

		public async Task<string> ObterTokenAcesso(bool recriarCacheToken = false)
		{
			var tokenAcessoValidadeEmSegundos = _unicoConfigucacao.TokenAcessoValidadeEmSegundos;
			var criadoEmUnixTimestamp = UnixEpoch;
			var expiraEmUnixTimestamp = criadoEmUnixTimestamp + tokenAcessoValidadeEmSegundos;

			var tokenConta = await ObterTokenConta(criadoEmUnixTimestamp, expiraEmUnixTimestamp);
			var token = await ObterTokenAcesso(tokenConta);

			return token;
		}

		private async Task<string> ObterTokenAcesso(string tokenConta)
		{
			var parametros = new Dictionary<string, string>
			{
				{ "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" },
				{ "assertion", tokenConta }
			};

			var conectaRequisicao = _conecta
			   .Post()
			   .AddBody(parametros)
			   .AddNomeApi("Unico")
			   .AddUrlApi(_unicoConfigucacao.AuthUrlBaseApi)
			   .AddUrlMetodo("oauth2/token");

			var resposta = await _clienteConecta.Executar(conectaRequisicao);

			if (resposta.StatusCode == HttpStatusCode.OK)
			{
				var retorno = JsonConvert.DeserializeObject<UnicoResultadoAutenticarDto>(resposta.Content);
				return retorno.access_token;
			}

			throw new WebException(Mensagens.ProviderUnico_NaoHouveSucessoNoRetornoDoProvedorObterToken);
		}

		private async Task<string> ObterTokenConta(long criadoEmUnixTimestamp, long expiraEmUnixTimestamp)
		{
			string chavePrivada = Regex.Unescape(_unicoConfigucacao.ChavePrivada);

			var conteudoPayload = new StringBuilder();
			conteudoPayload.Append($"\"aud\":\"{_unicoConfigucacao.AuthUrlBaseApi}\",");
			conteudoPayload.Append($"\"exp\":{expiraEmUnixTimestamp},");
			conteudoPayload.Append($"\"iat\":{criadoEmUnixTimestamp},");
			conteudoPayload.Append($"\"iss\":\"{_unicoConfigucacao.ServiceAccount}@{_unicoConfigucacao.Tenant}.iam.acesso.io\",");
			conteudoPayload.Append($"\"scope\":\"*\"");

			var payloadJwt = $"{{{conteudoPayload}}}";

			RSAParameters rsaParams;

			using (var tr = new StringReader(chavePrivada))
			{
				var rpckParams = new PemReader(tr).ReadObject();
				rsaParams = RsaPrivateKeyUtils.ToRSAParameters((RsaPrivateCrtKeyParameters)rpckParams);
			}
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(TAMANHO_CHAVE_RSA))
			{
				rsa.ImportParameters(rsaParams);
				return Jose.JWT.Encode(payloadJwt, rsa, Jose.JwsAlgorithm.RS256);
			}
		}
	}
}

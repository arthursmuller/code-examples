using LoginSocialApple.Dto;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LoginSocialApple
{
    [ExcludeFromCodeCoverage]
    public class ProvedorApple : IProvedorApple
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConfiguracaoLoginSocialApple _configuracaoLoginApple;
        private readonly string _validIssuer = "https://appleid.apple.com";
        private readonly string _applePublicKeys = "https://appleid.apple.com/auth/keys";
        
        public ProvedorApple(IHttpClientFactory httpClientFactory, ConfiguracaoLoginSocialApple configuracaoLoginApple)
        {
            _httpClientFactory = httpClientFactory;
            _configuracaoLoginApple = configuracaoLoginApple;
        }

        public async Task<ValidacaoTokenRetornoDto> ValidarToken(string token)
        {
            var httpClient = _httpClientFactory.CreateClient("Apple");

             //Read the token and get it's claims using System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims;
            SecurityKey publicKey; SecurityToken validatedToken;

            //Get the expiration of the token and convert its value from unix time seconds to DateTime object
            var expirationClaim = claims.FirstOrDefault(x => x.Type == "exp").Value;
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim)).DateTime;

            if (expirationTime < DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException("Não houve sucesso na validação do token de login do Apple. O Token informado expirou. Realize novo login.");
            }

            //Request Apple's JWKS used for verifying the tokens.
            var applePublicKeys = httpClient.GetAsync(_applePublicKeys);
            var keyset = new JsonWebKeySet(applePublicKeys.Result.Content.ReadAsStringAsync().Result);

            //Since there is more than one JSON Web Key we select the one that has been used for our token.
            //This is achieved by filtering on the "Kid" value from the header of the token
            publicKey = keyset.Keys.FirstOrDefault(x => x.Kid == jwtSecurityToken.Header.Kid);
        
            //Create new TokenValidationParameters object which we pass to ValidateToken method of JwtSecurityTokenHandler.
            //The handler uses this object to validate the token and will throw an exception if any of the specified parameters is invalid.
            
            
            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _validIssuer,
                IssuerSigningKey = publicKey,
                ValidAudience = _configuracaoLoginApple.idCliente
            };


            try{
                var t = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch(Exception ex){
                throw new ArgumentException($"Não houve sucesso na validação do token de login do Apple. Retorno: Erro na validação do Token");
            }

            var jwtToken = new JwtSecurityToken(token);

            ValidacaoTokenRetornoDto retornoToken = new ValidacaoTokenRetornoDto();
            retornoToken.Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email").Value;
            return retornoToken;
            
        }

        public async Task<ValidacaoTokenRetornoDto> ValidarToken(string codigoAutorizacao, string redirectUrl)
        {

            var chave = GeraChaveClienteApple();
        
            //Create a List of KeyValuePairs that will hold the form-data parameters.
            var requestBody = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", _configuracaoLoginApple.idCliente),
                new KeyValuePair<string, string>("client_secret", chave),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl),
                new KeyValuePair<string, string>("code", codigoAutorizacao)
            };

            var conteudo = new FormUrlEncodedContent(requestBody);
            conteudo.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var requisicao =  new HttpRequestMessage(HttpMethod.Post, "https://appleid.apple.com/auth/token") { Content = conteudo };

            var httpClient = _httpClientFactory.CreateClient("AppleAutenticacao");
            
            var resposta = await httpClient.SendAsync(requisicao);

            if(resposta.StatusCode != HttpStatusCode.OK){
                throw new ArgumentException($"Não houve sucesso na validação do token de login da Apple. Retorno: {resposta.StatusCode.ToString()} - {await resposta.Content.ReadAsStringAsync()}");
            }

            var dadosApple = await resposta.Content.ReadAsAsync<AutenticacaoAppleRetornoDto>();
            var jwtToken = new JwtSecurityToken(dadosApple.id_token);

            ValidacaoTokenRetornoDto retornoToken = new ValidacaoTokenRetornoDto();
            retornoToken.Email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email").Value;
            return retornoToken;
        }

        protected string GeraChaveClienteApple()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
           
            //Create new ECDsaCng object with the imported key.
            var ecDsaCng = ECDsa.Create();
            ecDsaCng.ImportPkcs8PrivateKey(Convert.FromBase64String(_configuracaoLoginApple.chavePrivada), out var _);

            var signingCredentials = new SigningCredentials(
              new ECDsaSecurityKey(ecDsaCng), SecurityAlgorithms.EcdsaSha256);

            var now = DateTime.UtcNow;
                
            //Create new list with the required claims.
            var claims = new List<Claim>
            {
                new Claim("iss", _configuracaoLoginApple.idTime),
                new Claim("iat", EpochTime.GetIntDate(now).ToString(), ClaimValueTypes.Integer64),
                new Claim("exp", EpochTime.GetIntDate(now.AddMinutes(5)).ToString(), ClaimValueTypes.Integer64),
                new Claim("aud", _validIssuer),
                new Claim("sub", _configuracaoLoginApple.idCliente)
            };
                
            //Create the JSON Web Token object.
            var token = new JwtSecurityToken(
                issuer: _configuracaoLoginApple.idTime,
                claims: claims,
                expires: now.AddMinutes(5),
                signingCredentials: signingCredentials);

            token.Header.Add("kid", _configuracaoLoginApple.idChave);
            token.Header.Remove("typ");

            //Return the JSON Web Token as a string.
            return tokenHandler.WriteToken(token);
        }
    }
}

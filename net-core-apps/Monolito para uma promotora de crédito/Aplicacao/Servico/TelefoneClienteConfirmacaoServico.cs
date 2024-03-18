using Aplicacao.Model.TelefoneClienteConfirmacao;
using B.Configuracao;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Enum.TemplateWhatsapp;
using Dominio.Enum.TemplateTorpedoVoz;
using Dominio.Enum.TemplateSms;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Fila.TorpedoVoz;
using Infraestrutura.Fila.Sms;
using Infraestrutura.Fila.Whatsapp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TelefoneClienteConfirmacaoServico : ServicoBase
    {
        private readonly ISmsServico _smsServico;
        private readonly ITorpedoVozServico _torpedoVozServico;
        private readonly IWhatsappServico _whatsappServico;
        private readonly Configuracao _configuracao;
        private readonly ILogger<TelefoneClienteConfirmacaoServico> _logger;
        private readonly PlataformaClienteContexto _contexto;


        public TelefoneClienteConfirmacaoServico(IBemMensagens mensagens
                                               , IUsuarioLogin usuarioLogin
                                               , PlataformaClienteContexto contexto
                                               , ISmsServico smsServico
                                               , IWhatsappServico whatsappServico
                                               , ITorpedoVozServico torpedoVozServico
                                               , Configuracao configuracao
                                               , ILogger<TelefoneClienteConfirmacaoServico> logger) : base(mensagens, usuarioLogin, contexto)
        {
            _contexto = contexto;
            _smsServico = smsServico;
            _whatsappServico = whatsappServico;
            _torpedoVozServico = torpedoVozServico;
            _configuracao = configuracao;
            _logger = logger;
        }

        public async Task<TelefoneClienteSolicitacaoConfirmacaoModel> SolicitarConfirmacaoDePropriedade(int idTelefoneCliente, TelefoneClienteSolicitacaoConfirmacaoEnvioModel solicitacao)
        {
            var telefoneCliente = await obterTelefoneCliente(idTelefoneCliente);
            if (telefoneCliente is null)
                return null;

            if (!validaSeSolicitacaoViavel(telefoneCliente, solicitacao))
                return null;

            telefoneCliente.AlternarConfirmado(confirmado: false);

            var token = gerarToken();
            var novaSolicitacao = new TelefoneClienteSolicitacaoConfirmacaoDominio(solicitacao.TipoSolicitacaoConfirmacao, idTelefoneCliente, token);

            await _contexto.AddAsync(novaSolicitacao);
            await _contexto.SaveChangesAsync();

            await processarEnvio(novaSolicitacao);

            return new TelefoneClienteSolicitacaoConfirmacaoModel { Id = novaSolicitacao.ID, SolicitacaoEnviada = novaSolicitacao.Enviada };
        }

        public async Task<TelefoneClienteSolicitacaoConfirmacaoModel> ReenviarSolicitacaoDeConfirmacaoDePropriedade(int idTelefoneCliente, TelefoneClienteSolicitacaoConfirmacaoEnvioModel solicitacaoReenvio)
        {
            var solicitacao = await _contexto.TelefoneClienteSolicitacoesConfirmacao
                                    .Include(t => t.TelefoneCliente)
                                    .Where(t =>
                                                t.TelefoneCliente.ID.Equals(idTelefoneCliente)
                                                && t.IdTipoSolicitacaoConfirmacao.Equals(solicitacaoReenvio.TipoSolicitacaoConfirmacao)
                                                && t.TelefoneCliente.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario)
                                    )
                                    .OrderBy(s => s.ID)
                                    .LastOrDefaultAsync();

            if (solicitacao is null)
                return await SolicitarConfirmacaoDePropriedade(idTelefoneCliente, solicitacaoReenvio);

            if (solicitacao.TelefoneCliente.Confirmado)
                _mensagens.AdicionarAlerta(Mensagens.TelefoneConfirmacao_JaConfirmado, EnumMensagemTipo.formulario);
            else
                await reenviarToken(solicitacao);

            return new TelefoneClienteSolicitacaoConfirmacaoModel { Id = solicitacao.ID, SolicitacaoEnviada = solicitacao.Enviada };
        }

        public async Task<bool> ConfirmarPropriedadeTelefone(int idTelefoneCliente, string token)
        {
            var solicitacao = await _contexto.TelefoneClienteSolicitacoesConfirmacao
                                    .Include(t => t.TelefoneCliente)
                                    .Where(t =>
                                                t.TelefoneCliente.ID.Equals(idTelefoneCliente)
                                                && t.TelefoneCliente.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario)
                                    )
                                    .OrderBy(s => s.ID)
                                    .LastOrDefaultAsync();

            if (solicitacao is null)
            {
                _mensagens.AdicionarErro(Mensagens.Telefone_NaoLocalizado, EnumMensagemTipo.banco);
                return false;
            }
            else if (solicitacao.Token != token)
            {
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_TokenInvalido, EnumMensagemTipo.banco);
                return false;
            }

            solicitacao.TelefoneCliente.AlternarConfirmado(confirmado: true);
            await _contexto.SaveChangesAsync();

            return true;
        }

        private async Task reenviarToken(TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao)
        {
            if (solicitacao.DataEnvioSolicitacao?.Date != (DateTime.Now.Date))
            {
                var token = gerarToken();
                solicitacao.SetToken(token);
                solicitacao.ReiniciarQuantidadeEnviosEfetuados();
            }

            await processarEnvio(solicitacao, ehReenvio: true);
        }

        private async Task processarEnvio(TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao, bool ehReenvio = false)
        {
            var solicitacaoEnviada = await enviarSolicitacao(solicitacao.TelefoneCliente, solicitacao, ehReenvio);

            solicitacao.AtualizarDadosEnvioSolicitacao(solicitacaoEnviada, _mensagens.BuscarErros()?.LastOrDefault()?.Mensagem);
            await _contexto.SaveChangesAsync();
        }


        private bool permiteEnvioQuantidadeLimiteEnvioDiario(TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao, int qtdLimite){
            if (solicitacao.QuantidadeEnviosEfetuados >= qtdLimite)
            {
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_FoiAtingidoLimiteDiarioReenviosToken, EnumMensagemTipo.negocio);
                return false;
            }
            return true;
        }

        private bool permiteEnvioIntervaloEnvio(TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao, int intervaloReenvio){
            if (solicitacao.DataEnvioSolicitacao.HasValue)
            {
                var intervaloMinimoParaReenvioEmSegundos = intervaloReenvio;
                
                var segundosAguardarParaReenvio = DateTime.Now.Subtract(solicitacao.DataEnvioSolicitacao.Value).TotalSeconds;
                if (segundosAguardarParaReenvio < intervaloMinimoParaReenvioEmSegundos)
                {
                    _mensagens.AdicionarErro(string.Format(Mensagens.TelefoneConfirmacao_AguardeXSegundosParaSolicitarReenvio, segundosAguardarParaReenvio), EnumMensagemTipo.negocio);
                    return false;
                }
            }
            return true;
        }

        private async Task<bool> enviarSolicitacao(TelefoneClienteDominio telefoneCliente, TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao, bool ehReenvio)
        {
            switch (solicitacao.IdTipoSolicitacaoConfirmacao)
            {
                case TipoSolicitacaoConfirmacao.Sms:
                    return await enviarSolicitacaoSms(telefoneCliente, solicitacao, ehReenvio);
                case TipoSolicitacaoConfirmacao.WhatsApp:
                    return await enviarSolicitacaoWhatsapp(telefoneCliente, solicitacao, ehReenvio);
                case TipoSolicitacaoConfirmacao.Telefonema:
                    return await enviarSolicitacaoTorpedoVoz(telefoneCliente, solicitacao, ehReenvio);
                default:
                    _mensagens.AdicionarErro(Mensagens.TipoSolicitacaoConfirmacao_NaoImplementado, EnumMensagemTipo.negocio);
                    return false;
            }
        }

        private async Task<bool> enviarSolicitacaoSms(TelefoneClienteDominio telefoneCliente, TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao, bool ehReenvio)
        {
            if (ehReenvio && !validarReenvio(solicitacao))
                return false;


            var templateMensagem = _contexto.TemplatesSms.Where( x => x.ID == TemplateSms.TokenConfirmacaoTelefone).First();
            var mensagemTratada = templateMensagem.Conteudo.Replace("#TOKEN#", solicitacao.Token);


            // TODO: Criar uma parte do código, para encaminhar o vinculo.
            try
            {
                await _smsServico.RegistrarSms( TemplateSms.TokenConfirmacaoTelefone
                                              , solicitacao.ID
                                              , new Fone(telefoneCliente.DDD, telefoneCliente.Fone)
                                              , mensagemTratada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorSms, EnumMensagemTipo.negocio);

                return false;
            }

            return true;
        }

        private async Task<bool> enviarSolicitacaoWhatsapp(TelefoneClienteDominio telefoneCliente
                                                         , TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao
                                                         , bool ehReenvio){
            

            
            if (ehReenvio && !validarReenvio(solicitacao))
                return false;

            var templateMensagem = _contexto.TemplatesWhatsapp.Where( x => x.ID == TemplateWhatsapp.TokenConfirmacaoTelefone).First();

            var valoresMensagem = new Dictionary<string,string>()
            {   { "TOKEN", solicitacao.Token }   };

            try
            {
                await _whatsappServico.RegistrarWhatsapp( TemplateWhatsapp.TokenConfirmacaoTelefone
                                                        , solicitacao.ID
                                                        , new Guid(templateMensagem.GUID)
                                                        , new Fone(telefoneCliente.DDD, telefoneCliente.Fone)
                                                        , valoresMensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorWhatsapp, EnumMensagemTipo.negocio);

                return false;
            }

            return true;
        }
        
        private async Task<bool> enviarSolicitacaoTorpedoVoz(TelefoneClienteDominio telefoneCliente
                                                            , TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao
                                                            , bool ehReenvio){

            if (ehReenvio && !validarReenvio(solicitacao))
                return false;

            var templateMensagem = _contexto.TemplatesTorpedoVoz.Where( x => x.ID == TemplateTorpedoVoz.TokenConfirmacaoTelefone).First();
            var mensagemTratada = templateMensagem.Conteudo.Replace("#TOKEN#", solicitacao.Token);

            try
            {
                await _torpedoVozServico.RegistrarTorpedoVoz( TemplateTorpedoVoz.TokenConfirmacaoTelefone
                                                            , solicitacao.ID
                                                            , new Fone(telefoneCliente.DDD, telefoneCliente.Fone)
                                                            , mensagemTratada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorTorpedoVoz, EnumMensagemTipo.negocio);

                return false;
            }

            return true;
        }
                
        private bool validarReenvio(TelefoneClienteSolicitacaoConfirmacaoDominio solicitacao)
        {
            int quantidadeMaximaReenviosDia = 0;
            int intervaloReenvio            = 999999;

            switch (solicitacao.IdTipoSolicitacaoConfirmacao)
            {
                case TipoSolicitacaoConfirmacao.Sms:
                    quantidadeMaximaReenviosDia = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-sms-quantidademaximaenviosdia"));
                    intervaloReenvio            = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-sms-intervalominimoreenviosegundos"));
                    break;
                case TipoSolicitacaoConfirmacao.WhatsApp:
                    quantidadeMaximaReenviosDia = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-whatsapp-quantidademaximaenviosdia"));
                    intervaloReenvio            = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-whatsapp-intervalominimoreenviosegundos"));
                    break;
                case TipoSolicitacaoConfirmacao.Telefonema:
                    quantidadeMaximaReenviosDia = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-torpedovoz-quantidademaximaenviosdia"));
                    intervaloReenvio            = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-torpedovoz-intervalominimoreenviosegundos"));
                    break;
            }

            if(!permiteEnvioQuantidadeLimiteEnvioDiario(solicitacao, quantidadeMaximaReenviosDia))
                return false;
            
            if(!permiteEnvioIntervaloEnvio(solicitacao, intervaloReenvio))
                return false;

            return true;
        }
        private async Task<TelefoneClienteDominio> obterTelefoneCliente(int idTelefone)
        {
            var telefoneCliente = await _contexto.TelefonesCliente
                                        .FirstOrDefaultAsync(t => t.ID.Equals(idTelefone) && t.Cliente.Usuario.ID.Equals(_usuarioLogin.IdUsuario));

            if (telefoneCliente is null)
                _mensagens.AdicionarErro(Mensagens.Telefone_NaoLocalizado, EnumMensagemTipo.formulario);

            return telefoneCliente;
        }

        private bool validaSeSolicitacaoViavel(TelefoneClienteDominio telefoneCliente, TelefoneClienteSolicitacaoConfirmacaoEnvioModel solicitacao)
        {
            if (solicitacao.TipoSolicitacaoConfirmacao == TipoSolicitacaoConfirmacao.Sms && (Fone.CalcularCodigoTipoFone(telefoneCliente.Fone) != SharedKernel.Enums.EnumCodigoTipoFone.Celular))
            {
                _mensagens.AdicionarErro(Mensagens.TelefoneConfirmacao_ConfirmacaoPorTelefonemaSomenteParaTelefoneCelular, EnumMensagemTipo.negocio);
                return false;
            }

            return true;
        }

        private string gerarToken()
        {
            var quantidadeDigitos = Convert.ToInt32(_configuracao.BuscarParametro("configuracao-tokenconfirmacaotelefone-quantidadedigitos"));

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                Byte[] bytes = new Byte[quantidadeDigitos];
                rng.GetBytes(bytes);
                var token = BitConverter.ToInt32(bytes, 0).ToString();
                return token.Substring(token.Length - quantidadeDigitos);
            }
        }
    }
}

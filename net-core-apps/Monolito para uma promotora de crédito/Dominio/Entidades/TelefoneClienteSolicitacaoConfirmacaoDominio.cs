using Dominio.Enum;
using System;

namespace Dominio
{
    public class TelefoneClienteSolicitacaoConfirmacaoDominio : EntidadeBase
    {
        public TipoSolicitacaoConfirmacao IdTipoSolicitacaoConfirmacao { get; private set; }
        public int IdTelefoneCliente { get; private set; }
        public string Token { get; private set; }
        public bool Enviada { get; private set; }
        public DateTime? DataEnvioSolicitacao { get; private set; }
        public string MensagemErro { get; private set; }
        public int QuantidadeEnviosEfetuados { get; private set; }

        public TipoSolicitacaoConfirmacaoDominio TipoSolicitacaoConfirmacao { get; private set; }
        public TelefoneClienteDominio TelefoneCliente { get; private set; }

        public TelefoneClienteSolicitacaoConfirmacaoDominio(TipoSolicitacaoConfirmacao idTipoSolicitacaoConfirmacao, int idTelefoneCliente, string token)
        {
            IdTipoSolicitacaoConfirmacao = idTipoSolicitacaoConfirmacao;
            IdTelefoneCliente = idTelefoneCliente;
            Token = token;
            QuantidadeEnviosEfetuados = 0;
        }

        public void AtualizarDadosEnvioSolicitacao(bool enviada, string mensagemErro)
        {
            Enviada = enviada;
            MensagemErro = mensagemErro;

            if (enviada)
            {
                DataEnvioSolicitacao = DateTime.Now;
                QuantidadeEnviosEfetuados++;
            }

            setDataAtualizacao();
        }

        public void SetToken(string token)
        {
            Token = token;
            setDataAtualizacao();
        }

        public void ReiniciarQuantidadeEnviosEfetuados()
        {
            QuantidadeEnviosEfetuados = 0;
            setDataAtualizacao();
        }
    }
}

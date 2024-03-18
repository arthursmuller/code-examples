using System;

namespace Dominio.Entidades
{
    public class SmsMensagemDominio: EntidadeBase
    {
        public string CodigoReferenciaMensagem { get; private set; }

        public string NumeroTelefone { get; private set; }

        public string Mensagem { get; private set; }

        public string Operadora { get; private set; }
        
        public bool Processado { get; private set; }

        public DateTime DataInsercao { get; private set; }

        public DateTime? DataEnvio { get; private set; }

        public DateTime? DataRecebimento { get; private set; }

        public int IdSmsFornecedor { get; private set; }

        public int? IdSituacaoEnvio { get; private set; }

        public int? IdSituacaoEnvioDetalhes { get; private set; }

        public SituacaoEnvioDominio SituacaoEnvio { get; private set; }

        public SituacaoEnvioDetalhesDominio SituacaoEnvioDetalhes { get; private set; }

        public SmsFornecedorDominio SmsFornecedor { get; private set; }

        public SmsMensagemDominio(string codigoReferenciaMensagem, string numeroTelefone, string mensagem, DateTime dataInsercao, int idSmsFornecedor, string operadora = null)
        {
            CodigoReferenciaMensagem = codigoReferenciaMensagem;
            NumeroTelefone = numeroTelefone;
            Mensagem = mensagem;
            DataInsercao = dataInsercao;
            IdSmsFornecedor = idSmsFornecedor;
            Operadora = operadora;
        }

        public void AtualizarSituacaoEnvio(int idSituacaoEnvio, int? idSituacaoEnvioDetalhes, bool? processado = null)
        {
            IdSituacaoEnvio = idSituacaoEnvio;
            IdSituacaoEnvioDetalhes = idSituacaoEnvioDetalhes ?? IdSituacaoEnvioDetalhes;
            Processado = processado ?? Processado;

            DataAtualizacao = DateTime.Now;
        }

        public void RegistrarEnvio(int codigoSituacaoEnvio, int codigoSituacaoEnvioDetalhes)
        {
            AtualizarSituacaoEnvio(codigoSituacaoEnvio, codigoSituacaoEnvioDetalhes, false);
            DataEnvio = DateTime.Now;
        }

        public void RegistrarRecebimento(int codigoSituacaoEnvio, int codigoSituacaoEnvioDetalhes)
        {
            AtualizarSituacaoEnvio(codigoSituacaoEnvio, codigoSituacaoEnvioDetalhes, true);
            DataRecebimento = DateTime.Now;
        }

        public void AtualizarOperadora(string operadora)
        {
            Operadora = operadora;
        }
    }
}

using System;

namespace Dominio
{
    public class ConsultaBeneficioInssClienteDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }

        public int? IdPaperlessDocumento { get; private set; }

        public int? IdAnexoArquivoTermo { get; private set; }

        public string ChaveAutorizacao { get; private set; }

        public DateTime DataGeracaoArquivoTermo { get; private set; }

        public ClienteDominio Cliente { get; private set; }

        public AnexoDominio AnexoArquivoTermo { get; private set; }

        public ConsultaBeneficioInssClienteDominio(int idCliente, int idPaperlessDocumento)
        {
            IdCliente = idCliente;
            IdPaperlessDocumento = idPaperlessDocumento;
            DataGeracaoArquivoTermo = DateTime.Now;
        }

        public ConsultaBeneficioInssClienteDominio(int idCliente, string chaveAutorizacao)
        {
            IdCliente = idCliente;
            ChaveAutorizacao = chaveAutorizacao;
            DataGeracaoArquivoTermo = DateTime.Now;
        }

        public void SetAnexoArquivoTermo(int idAnexoArquivoTermo)
        {
            IdAnexoArquivoTermo = idAnexoArquivoTermo;
            setDataAtualizacao();
        }

        public void SetChaveAutorizacao(string chaveAutorizacao)
        {
            ChaveAutorizacao = chaveAutorizacao;
            setDataAtualizacao();
        }
    }
}

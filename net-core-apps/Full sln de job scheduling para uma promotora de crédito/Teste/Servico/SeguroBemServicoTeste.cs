using Aplicacao.Servico;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Teste.Servico
{
    public class SeguroBemServicoTeste : ServicoBase
    {
        private readonly SeguroBemServico _seguroBemServico;

        public SeguroBemServicoTeste() : base()
        {
            _seguroBemServico = new SeguroBemServico(
                _mensageria, 
                _contexto, 
                new Mock<IHttpClientFactory>().Object
            );
        }


        [Fact]
        public async void GravarDados_Quando_Nao_Ha_Dados()
        {
           
        }

        [Fact]
        public async void GravarDados_Novos_Quando_Api_Trouxer_Novos_Dados()
        {

        }

        [Fact]
        public async void SeguroParentesco_Bem_E_Icatu_Devem_EstarIguais()
        {

        }
    }
}

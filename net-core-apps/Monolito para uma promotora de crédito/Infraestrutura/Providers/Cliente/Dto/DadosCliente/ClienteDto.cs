using System.Collections.Generic;

namespace Infraestrutura.Providers.Cliente.Dto
{
    public class ClienteDto
    {
        public ClienteDadosBasicosDto DadosBasicos { get; set; }

        public DocumentoIdentificacaoDto DocumentoIdentificacao { get; set; }

        public EnderecoClienteDto EnderecoResidencial { get; set; }

        public EnderecoClienteDto EnderecoCorrespondencia { get; set; }

        public ClienteContatosDto Contato { get; set; }

        public ConjugeDto Conjuge { get; set; }

        public BancoDto ContaBancaria1 { get; set; }

        public BancoDto ContaBancaria2 { get; set; }

        public IEnumerable<ClienteRendimentoDto> Rendimentos { get; set; }
    }
}

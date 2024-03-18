using Aplicacao.Model.Banco;
using Aplicacao.Model.Cliente;
using Aplicacao.Model.EstadoCivil;
using Aplicacao.Model.Genero;
using Aplicacao.Model.GrauInstrucao;
using Aplicacao.Model.OrgaoEmissorIdentificacao;
using Aplicacao.Model.UnidadeFederativa;
using Dominio;
using Infraestrutura.Providers.Cliente.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IClienteServico
    {
        Task<ClienteExibicaoModel> ObterClienteAutenticado();

        Task<ClienteExibicaoCompletaModel> ObterClientePorCpf(string cpf);

        Task<ClienteExibicaoModel> AtualizarCliente(ClienteModel clienteAtualizacao, DateTime? dataImportacao = null);

        Task<IEnumerable<GrauInstrucaoModel>> ListarGrausEscolaridade();

        Task<IEnumerable<EstadoCivilModel>> ListarEstadosCivil();

        Task<IEnumerable<OrgaoEmissorIdentificacaoModel>> ListarOrgaosEmissoresIdentificacao(string termo);

        Task<IEnumerable<GeneroModel>> ListarGeneros();

        Task<OrgaoEmissorIdentificacaoModel> ObterOrgaoEmissor(string orgaoEmissor);

        Task<GeneroModel> ObterGeneroPorSigla(string sigla);

        Task<GrauInstrucaoModel> ObterGrauEscolaridadePorDescricao(string descricao);

        Task<EstadoCivilModel> ObterEstadoCivilPorSigla(string sigla);

        Task ImportarDadosBasicos(ClienteDto dadosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas);
        Task<bool> AdicionarContaBancaria(ContaBancariaModel model);
        Task<bool> AtualizarContaBancaria(ContaBancariaModel model);
    }
}
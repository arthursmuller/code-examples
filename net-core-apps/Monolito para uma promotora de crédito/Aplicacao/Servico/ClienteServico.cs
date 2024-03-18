using Aplicacao.Model.Banco;
using Aplicacao.Model.Cliente;
using Aplicacao.Model.EstadoCivil;
using Aplicacao.Model.Genero;
using Aplicacao.Model.GrauInstrucao;
using Aplicacao.Model.OrgaoEmissorIdentificacao;
using Aplicacao.Model.UnidadeFederativa;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Cliente.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ClienteServico : ServicoBase, IClienteServico
    {
        private readonly ILocalizacaoServico _localizacaoServico;

        public ClienteServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, ILocalizacaoServico localizacaoServico) : base(mensagens, usuarioLogin, contexto)
            => _localizacaoServico = localizacaoServico;

        public async Task<ClienteExibicaoModel> ObterClienteAutenticado()
        {
            var cliente = await _contexto.Clientes
                            .Include(c => c.Genero)
                            .Include(c => c.EstadoCivil)
                            .Include(c => c.Conjuge)
                            .Include(c => c.GrauInstrucao)
                            .Include(c => c.CidadeNatal)
                                .ThenInclude(cidadeNatal => cidadeNatal.UF)
                            .Include(c => c.Loja)
                                .ThenInclude(loja => loja.Telefones)
                            .Include(i => i.Loja)
                            .ThenInclude(l => l.Municipio)
                                    .ThenInclude(m => m.UF)
                            .Include(i => i.Loja)
                                .ThenInclude(l => l.TipoLogradouro)
                            .Include(c => c.Usuario)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.IdUsuario.Equals(_usuarioLogin.IdUsuario));

            if (cliente == null)
            {
                adicionarMensagemClienteNaoEncontrado();
                return null;
            }

            return new ClienteExibicaoModel(cliente);
        }

        public async Task<ClienteExibicaoCompletaModel> ObterClientePorCpf(string cpf)
        {
            var cliente = await _contexto.Clientes
                                    .Include(c => c.Genero)
                                    .Include(c => c.EstadoCivil)
                                    .Include(c => c.GrauInstrucao)
                                    .Include(c => c.CidadeNatal)
                                        .ThenInclude(cidadeNatal => cidadeNatal.UF)
                                    .Include(c => c.TelefonePrincipal)
                                    .Include(c => c.TelefoneSecundario)
                                    .Include(c => c.DocumentosIdentificacao)
                                        .ThenInclude(d => d.TipoDocumento)
                                    .Include(c => c.DocumentosIdentificacao)
                                        .ThenInclude(d => d.OrgaoEmissor)
                                    .Include(c => c.DocumentosIdentificacao)
                                        .ThenInclude(d => d.Uf)
                                    .Include(c => c.Enderecos)
                                        .ThenInclude(e => e.Municipio.UF)
                                    .Include(c => c.Enderecos)
                                        .ThenInclude(e => e.TipoLogradouro)
                                    .Include(c => c.Usuario)
                                    
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.Convenio)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ConvenioOrgao)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.Uf)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaCliente)
                                            .ThenInclude(r => r.Banco)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaCliente)
                                            .ThenInclude(r => r.TipoConta)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaCliente)
                                            .ThenInclude(r => r.FormaRecebimento)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaClienteRecebimento)
                                            .ThenInclude(r => r.Banco)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaClienteRecebimento)
                                            .ThenInclude(r => r.TipoConta)
                                    .Include(c => c.Rendimentos)
                                        .ThenInclude(r => r.ContaClienteRecebimento)
                                            .ThenInclude(r => r.FormaRecebimento)
                                    .Include(c => (c.Rendimentos as RendimentoClienteInssDominio).EspecieBeneficio)
                                    .Include(c => (c.Rendimentos as RendimentoClienteSiapeDominio).TipoFuncional)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Usuario.CPF.Equals(cpf));

            if (cliente == null)
            {
                adicionarMensagemClienteNaoEncontrado();
                return null;
            }

            return new ClienteExibicaoCompletaModel(cliente);
        }

        public async Task<ClienteExibicaoModel> AtualizarCliente(ClienteModel clienteAtualizacao, DateTime? dataImportacao = null)
        {
            var cliente = await _contexto.Clientes
                                .FirstOrDefaultAsync(c => c.IdUsuario.Equals(_usuarioLogin.IdUsuario));

            if (cliente == null)
            {
                adicionarMensagemClienteNaoEncontrado();
                return null;
            }

            cliente.SetPropriedadesAtualizadas(
                clienteAtualizacao.IdGenero,
                clienteAtualizacao.IdEstadoCivil,
                clienteAtualizacao.IdGrauInstrucao,
                clienteAtualizacao.IdCidadeNatal,
                clienteAtualizacao.Nome,
                clienteAtualizacao.DataNascimento,
                clienteAtualizacao.Filiacao1,
                clienteAtualizacao.Filiacao2,
                clienteAtualizacao.DeficienteVisual,
                dataImportacao,
                clienteAtualizacao.IdLoja,
                clienteAtualizacao.IdProfissao
            );

            await _contexto.SaveChangesAsync();

            return await ObterClienteAutenticado();
        }

        public async Task<bool> AdicionarContaBancaria(ContaBancariaModel model)
        {
            var conta = new ContaBancariaDominio(model.Agencia, model.NumeroConta, model.DigitoVerificadorAgencia, model.IdBanco);

            await _contexto.ContasBancarias.AddAsync(conta);
            await SaveChangesAsync();

            var cliente = await _contexto.Clientes
                                .FirstOrDefaultAsync(c => c.IdUsuario.Equals(_usuarioLogin.IdUsuario));

            cliente.SetContaBancaria(conta.ID);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarContaBancaria(ContaBancariaModel model)
        {
            var cliente = await _contexto.Clientes
                                .Include(c => c.ContaBancaria)
                                .FirstOrDefaultAsync(c => c.IdUsuario.Equals(_usuarioLogin.IdUsuario));

            cliente.ContaBancaria.SetPropriedadesAtualizadas(model.Agencia, model.NumeroConta, model.DigitoVerificadorAgencia, model.IdBanco);

            await SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GrauInstrucaoModel>> ListarGrausEscolaridade()
        {
            var opcoes = await _contexto.GrausInstrucao
                .AsNoTracking()
                .OrderBy(g => g.Descricao)
                .ToListAsync();

            return opcoes.Select(i
                => new GrauInstrucaoModel
                {
                    Id = i.ID,
                    Descricao = i.Descricao,
                });
        }

        public async Task<IEnumerable<EstadoCivilModel>> ListarEstadosCivil()
        {
            var opcoes = await _contexto.EstadosCivil
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            return opcoes.Select(i
                => new EstadoCivilModel
                {
                    Id = i.ID,
                    Descricao = i.Descricao,
                    Sigla = i.Sigla,
                });
        }

        public async Task<IEnumerable<OrgaoEmissorIdentificacaoModel>> ListarOrgaosEmissoresIdentificacao(string termo)
        {
            var orgaosEmissoresQuery = _contexto.OrgaosEmissoresIdentificacao
                .AsNoTracking();

            if (!String.IsNullOrWhiteSpace(termo))
            {
                orgaosEmissoresQuery = orgaosEmissoresQuery.Where(orgaoEmissor =>
                    orgaoEmissor.Codigo.Contains(termo)
                    || orgaoEmissor.Descricao.Contains(termo));
            }

            var orgaos = await orgaosEmissoresQuery
                .OrderBy(o => o.Descricao)
                .ToListAsync();

            if (orgaos == null || !orgaos.Any())
            {
                _mensagens.AdicionarAlerta(Mensagens.Documento_OrgaoEmissorNaoEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return orgaos.Select(i
                => new OrgaoEmissorIdentificacaoModel
                {
                    Id = i.ID,
                    Codigo = i.Codigo,
                    Descricao = i.Descricao
                });
        }

        public async Task<IEnumerable<GeneroModel>> ListarGeneros()
        {
            var opcoes = await _contexto.Generos
                .AsNoTracking()
                .OrderBy(g => g.Descricao)
                .ToListAsync();

            return opcoes.Select(i
                => new GeneroModel
                (
                    i.ID,
                    i.Sigla,
                    i.Descricao
                ));
        }

        public async Task<OrgaoEmissorIdentificacaoModel> ObterOrgaoEmissor(string orgaoEmissor)
        {
            var orgaos = await ListarOrgaosEmissoresIdentificacao(orgaoEmissor);

            var quantidadeOrgaos = orgaos?.Count() ?? 0;

            if (quantidadeOrgaos == 1)
                return orgaos.First();

            if (quantidadeOrgaos > 1)
                _mensagens.AdicionarAlerta(Mensagens.Documento_OrgaoEmissorNaoFoiPossivelLocalizarApenasUm, EnumMensagemTipo.formulario);

            if (quantidadeOrgaos == 0)
                _mensagens.AdicionarAlerta(Mensagens.Documento_OrgaoEmissorNaoEncontrado, EnumMensagemTipo.banco);

            return new OrgaoEmissorIdentificacaoModel();
        }

        public async Task<GeneroModel> ObterGeneroPorSigla(string sigla)
        {
            var genero = await _contexto.Generos.AsNoTracking().Where(g => g.Sigla.Equals(sigla)).FirstOrDefaultAsync();

            if (genero == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Cliente_GeneroNaoEncontrado, EnumMensagemTipo.banco);

                return new GeneroModel();
            }

            return new GeneroModel
            (
                genero.ID,
                genero.Descricao,
                genero.Sigla
            );
        }

        public async Task<GrauInstrucaoModel> ObterGrauEscolaridadePorDescricao(string descricao)
        {
            var grauEscolaridade = await _contexto.GrausInstrucao.AsNoTracking().FirstOrDefaultAsync(g => g.Descricao.Equals(descricao));

            if (grauEscolaridade == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Cliente_GrauInstrucaoNaoEncontrado, EnumMensagemTipo.banco);

                return new GrauInstrucaoModel();
            }

            return new GrauInstrucaoModel
            {
                Id = grauEscolaridade.ID,
                Descricao = grauEscolaridade.Descricao
            };
        }

        public async Task<EstadoCivilModel> ObterEstadoCivilPorSigla(string sigla)
        {
            var estadoCivil = await _contexto.EstadosCivil.AsNoTracking().Where(ec => ec.Sigla.Equals(sigla)).FirstOrDefaultAsync();

            if (estadoCivil == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Cliente_EstadoCivilNaoEncontrado, EnumMensagemTipo.banco);

                return new EstadoCivilModel();
            }

            return new EstadoCivilModel
            {
                Id = estadoCivil.ID,
                Descricao = estadoCivil.Descricao,
                Sigla = estadoCivil.Sigla
            };
        }

        private void adicionarMensagemClienteNaoEncontrado()
            => _mensagens.AdicionarErro(Mensagens.Cliente_NaoEncontrado, EnumMensagemTipo.negocio);

        #region Importação

        public async Task ImportarDadosBasicos(ClienteDto dadosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var dadosBasicos = await converterDadosBasicos(dadosCliente, unidadesFederativas);
            if (new ClienteModelValidacao().Validate(dadosBasicos).IsValid)
                await AtualizarCliente(dadosBasicos, DateTime.Now);
        }

        private async Task<ClienteModel> converterDadosBasicos(ClienteDto dadosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var clienteModel = new ClienteModel();
            clienteModel.DeficienteVisual = dadosCliente.DadosBasicos.DeficienteVisual;
            clienteModel.Filiacao1 = dadosCliente.DadosBasicos.Mae;
            clienteModel.Filiacao2 = dadosCliente.DadosBasicos.Pai;
            clienteModel.Nome = dadosCliente.DadosBasicos.Nome;

            var estadoCivil = await ObterEstadoCivilPorSigla(dadosCliente.DadosBasicos.CodigoEstadoCivil);
            clienteModel.IdEstadoCivil = estadoCivil.Id;

            var idUf = unidadesFederativas.FirstOrDefault(uf => uf.Sigla.Equals(dadosCliente.DadosBasicos.UFNascimento))?.Id ?? 0;
            var cidadeNatal = await _localizacaoServico.ObterMunicipio(idUf, dadosCliente.DadosBasicos.CidadeNascimento);

            if (cidadeNatal != null)
                clienteModel.IdCidadeNatal = cidadeNatal.Id;

            var genero = await ObterGeneroPorSigla(dadosCliente.DadosBasicos.Sexo);
            clienteModel.IdGenero = genero.Id;

            var grauInstrucao = await ObterGrauEscolaridadePorDescricao(dadosCliente.DadosBasicos.DescricaoGrauInstrucao);
            clienteModel.IdGrauInstrucao = grauInstrucao.Id;

            if (dadosCliente.DadosBasicos.Nascimento.HasValue)
                clienteModel.DataNascimento = dadosCliente.DadosBasicos.Nascimento.Value;

            return clienteModel;
        }

        #endregion
    }
}

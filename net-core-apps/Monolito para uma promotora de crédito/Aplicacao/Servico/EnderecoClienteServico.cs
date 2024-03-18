using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Model.UnidadeFederativa;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Cliente.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class EnderecoClienteServico : ServicoBase
    {
        private readonly ILocalizacaoServico _localizacaoServico;

        public EnderecoClienteServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, ILocalizacaoServico localizacaoServico) : base(mensagens, usuarioLogin, contexto)
            => _localizacaoServico = localizacaoServico;

        public async Task<IEnumerable<EnderecoClienteExibicaoModel>> BuscarEnderecosPorCliente()
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var enderecos = await obterConsultaBase()
                                .Where(e => e.IdCliente.Equals(usuario.Cliente.ID) && !e.Deletado)
                                .ToListAsync();

            return enderecos.Select(e => new EnderecoClienteExibicaoModel(e));
        }

        public async Task<EnderecoClienteExibicaoModel> GravarEndereco(EnderecoClienteModel enderecoGravacao)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            await tratarEnderecoPrincipal(enderecoGravacao, usuario.Cliente.ID);

            EnderecoClienteDominio endereco;

            if (enderecoGravacao.Id != null)
            {
                endereco = usuario.Cliente.Enderecos.FirstOrDefault(endereco => endereco.ID == enderecoGravacao.Id);

                if (endereco == null)
                {
                    _mensagens.AdicionarErro(Mensagens.Endereco_NaoEncontrado, EnumMensagemTipo.formulario);
                    return null;
                }

                endereco.SetAtualizarEndereco(
                                enderecoGravacao.Titulo,
                                enderecoGravacao.IdMunicipio,
                                enderecoGravacao.Bairro,
                                enderecoGravacao.IdTipoLogradouro,
                                enderecoGravacao.Logradouro,
                                enderecoGravacao.Numero,
                                enderecoGravacao.Complemento,
                                enderecoGravacao.Cep,
                                enderecoGravacao.Principal);
            }
            else
            {
                endereco = new EnderecoClienteDominio(
                                usuario.Cliente.ID,
                                enderecoGravacao.Titulo,
                                enderecoGravacao.IdMunicipio,
                                enderecoGravacao.Bairro,
                                enderecoGravacao.IdTipoLogradouro,
                                enderecoGravacao.Logradouro,
                                enderecoGravacao.Numero,
                                enderecoGravacao.Complemento,
                                enderecoGravacao.Cep,
                                enderecoGravacao.Principal
                            );

                await _contexto.AddAsync(endereco);
            }

            await _contexto.SaveChangesAsync();

            if (usuario.Cliente.IdEnderecoPrincipal == null || enderecoGravacao.Principal)
                usuario.Cliente.SetEnderecoPrincipal(endereco.ID);
            else
                usuario.Cliente.SetEnderecoSecundario(endereco.ID);

            await _contexto.SaveChangesAsync();

            return await obterEndereco(endereco.ID, usuario.Cliente.ID);
        }

        public async Task<bool> DeletarEndereco(int idendereco)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return false;

            var endereco = await _contexto.EnderecosCliente.FirstOrDefaultAsync(d => d.ID.Equals(idendereco) && d.IdCliente.Equals(usuario.Cliente.ID));

            if (!validarSePermiteRemover(endereco))
                return false;

            endereco.AlternarDeletado(true);

            usuario.Cliente.SetEnderecoSecundario(default);

            await SaveChangesAsync();

            return true;
        }

        private bool validarSePermiteRemover(EnderecoClienteDominio endereco)
        {
            if (endereco == null)
            {
                _mensagens.AdicionarErro(Mensagens.Endereco_NaoEncontrado, EnumMensagemTipo.negocio);
                return false;
            }
            else if (endereco.Cliente.IdEnderecoPrincipal == endereco.ID)
            {
                _mensagens.AdicionarErro(Mensagens.Endereco_IndicadoComoPrincipalNaoPodeSerRemovido, EnumMensagemTipo.negocio);
                return false;
            }

            return true;
        }

        public async Task<EnderecoClienteExibicaoModel> AtualizarEndereco(int idEndereco, EnderecoClienteModel enderecoAtualizacao)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var endereco = await _contexto.EnderecosCliente.FirstOrDefaultAsync(e => e.ID.Equals(idEndereco) && e.IdCliente.Equals(usuario.Cliente.ID));

            if (endereco == null)
            {
                _mensagens.AdicionarErro(Mensagens.Endereco_NaoEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            await tratarEnderecoPrincipal(enderecoAtualizacao, usuario.Cliente.ID);

            endereco.SetAtualizarEndereco(
                                enderecoAtualizacao.Titulo,
                                enderecoAtualizacao.IdMunicipio,
                                enderecoAtualizacao.Bairro,
                                enderecoAtualizacao.IdTipoLogradouro,
                                enderecoAtualizacao.Logradouro,
                                enderecoAtualizacao.Numero,
                                enderecoAtualizacao.Complemento,
                                enderecoAtualizacao.Cep,
                                enderecoAtualizacao.Principal
                            );

            await _contexto.SaveChangesAsync();

            return await obterEndereco(endereco.ID, usuario.Cliente.ID);
        }

        public async Task<EnderecoClienteExibicaoModel> ObterEnderecoPrincipal()
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var enderecoPrincipal = await obterConsultaBase()
                                            .FirstOrDefaultAsync(e => e.IdCliente.Equals(usuario.Cliente.ID) && !e.Deletado && e.Principal);
            if (enderecoPrincipal == null)
            {
                _mensagens.AdicionarErro(Mensagens.Endereco_PrincipalNaoLocalizado);
                return null;
            }

            return new EnderecoClienteExibicaoModel(enderecoPrincipal);
        }

        #region Importação

        public async Task ImportarEnderecos(ClienteDto dadosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var enderecos = await converterEnderecos(dadosCliente, unidadesFederativas);
            foreach (var endereco in enderecos)
            {
                if (new EnderecoClienteModelValidacao().Validate(endereco).IsValid)
                    await GravarEndereco(endereco);
            }
        }

        private async Task<IEnumerable<EnderecoClienteModel>> converterEnderecos(ClienteDto dadosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            List<EnderecoClienteModel> enderecos = new List<EnderecoClienteModel>();
            if (dadosCliente.EnderecoResidencial != null)
                enderecos.Add(await obterEnderecoConvertido(dadosCliente.EnderecoResidencial, "Endereço Residencial", unidadesFederativas));

            if (dadosCliente.EnderecoCorrespondencia != null)
                enderecos.Add(await obterEnderecoConvertido(dadosCliente.EnderecoCorrespondencia, "Endereço Correspondência", unidadesFederativas));

            return enderecos;
        }

        private async Task<EnderecoClienteModel> obterEnderecoConvertido(EnderecoClienteDto enderecoCliente, string titulo, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var endereco = new EnderecoClienteModel();
            endereco.Bairro = enderecoCliente.Bairro;
            endereco.Cep = enderecoCliente.Cep;
            endereco.Complemento = enderecoCliente.Complemento;
            endereco.Logradouro = enderecoCliente.Logradouro;
            endereco.Numero = enderecoCliente.Numero;
            endereco.Titulo = titulo;

            var idUf = unidadesFederativas.FirstOrDefault(uf => uf.Sigla.Equals(enderecoCliente.Uf))?.Id ?? 0;

            var municipio = await _localizacaoServico.ObterMunicipio(idUf, enderecoCliente.Cidade);
            endereco.IdMunicipio = municipio.Id;

            var tipoLogradouro = await _localizacaoServico.ObterTipoLogradouroPorCodigo(enderecoCliente.DescricaoTipoLogradouro);
            endereco.IdTipoLogradouro = tipoLogradouro.Id;

            return endereco;
        }

        #endregion

        private IQueryable<EnderecoClienteDominio> obterConsultaBase()
        {
            var consulta = _contexto.EnderecosCliente
                                .Include(e => e.Municipio)
                                    .ThenInclude(m => m.UF)
                                .Include(e => e.TipoLogradouro)
                                .AsNoTracking();

            return consulta;
        }

        private async Task tratarEnderecoPrincipal(EnderecoClienteModel enderecoCliente, int idCliente)
        {
            var enderecoPrincipalAtualCliente = await _contexto.EnderecosCliente.FirstOrDefaultAsync(e => e.IdCliente.Equals(idCliente) && e.Principal);

            if (enderecoPrincipalAtualCliente == null)
                enderecoCliente.Principal = true;
            else if (enderecoCliente.Principal)
                enderecoPrincipalAtualCliente.SetPrincipal(false);
        }

        private async Task<EnderecoClienteExibicaoModel> obterEndereco(int idEndereco, int idCliente)
        {
            var endereco = await obterConsultaBase()
                                .FirstOrDefaultAsync(d => d.ID.Equals(idEndereco) && !d.Deletado && d.IdCliente.Equals(idCliente));

            if (endereco == null)
                return null;

            return new EnderecoClienteExibicaoModel(endereco);
        }
    }
}

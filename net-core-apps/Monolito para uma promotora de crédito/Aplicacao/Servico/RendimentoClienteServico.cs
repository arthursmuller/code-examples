using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Model.UnidadeFederativa;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Cliente.Dto;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class RendimentoClienteServico : ServicoBase, IRendimentoClienteServico
    {
        private readonly ConvenioServico _convenioServico;
        private readonly BancarioServico _bancarioServico;
        private readonly IConsignadoServico _consignadoServico;
        private readonly IContaClienteServico _contaClienteServico;

        public RendimentoClienteServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto,
            ConvenioServico convenioServico,
            BancarioServico bancarioServico,
            IConsignadoServico consignadoServico,
            IContaClienteServico contaClienteServico) : base(mensagens, usuarioLogin, contexto)
        {
            _convenioServico = convenioServico;
            _bancarioServico = bancarioServico;
            _consignadoServico = consignadoServico;
            _contaClienteServico = contaClienteServico;
        }

        public async Task<IEnumerable<RendimentoClienteExibicaoModel>> BuscarRendimentosPorCliente()
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var rendimentos = await obterConsultaBase()
                                    .Where(r => r.IdCliente.Equals(usuario.Cliente.ID) && !r.Deletado)
                                    .AsNoTracking()
                                    .OrderBy(r => r.Matricula)
                                    .ToListAsync();

            if (!rendimentos.Any())
                return null;

            return rendimentos.Select(r => new RendimentoClienteExibicaoModel(r));
        }

        public async Task<bool> VerificarSeRendimentoPertenceAoUsuario(int idRendimento, int? idUsuario = null)
        {
            var usuario = !idUsuario.HasValue ? 
                await ObterDadosUsuarioAutenticado() : 
                await _contexto.Usuarios
                    .Include(u => u.Cliente)
                    .FirstOrDefaultAsync(u => u.ID.Equals(idUsuario));

            if (_mensagens.PossuiErros)
                return false;

            return await _contexto.RendimentoCliente.AsNoTracking()
                .AnyAsync(r => r.ID.Equals(idRendimento) && r.IdCliente.Equals(usuario.Cliente.ID) && !r.Deletado);
        }

        public async Task<RendimentoClienteExibicaoModel> GravarRendimento(RendimentoClienteModel rendimento)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            int idConta = await criarContaOuAtualizar(-1, rendimento.ContaCliente, usuario.Cliente.ID);
            int idContaRecebimento = rendimento.ContaClienteRecebimento is null
                ? idConta
                : await criarContaOuAtualizar(-1, rendimento.ContaClienteRecebimento, usuario.Cliente.ID);

            if (_mensagens.PossuiErros)
                return null;

            RendimentoClienteDominio rendimentoNovo;

            switch (rendimento.Convenio)
            {
                case Convenio.INSS:
                    rendimentoNovo = await obterRendimentoInss(rendimento, usuario, idConta, idContaRecebimento);
                    break;
                case Convenio.SIAPE:
                    rendimentoNovo = obterRentimentoSiape(rendimento, usuario, idConta, idContaRecebimento);
                    break;
                 case Convenio.MARINHA:
                    rendimentoNovo = await obterRendimentoMarinha(rendimento, usuario, idConta, idContaRecebimento);
                     break;
                case Convenio.AERONAUTICA:
                    rendimentoNovo = await obterRendimentoAeronautica(rendimento, usuario, idConta, idContaRecebimento);
                     break;
                default:
                    _mensagens.AdicionarErro(Mensagens.Rendimento_ConvenioInvalido, EnumMensagemTipo.formulario); 
                    rendimentoNovo = null;
                    break;
            }

            if (_mensagens.PossuiErros)
                return null;

            await _contexto.AddAsync(rendimentoNovo);

            await _contexto.SaveChangesAsync();

            return await obterRendimento(rendimentoNovo.ID, usuario.Cliente.ID);
        }

        public async Task<RendimentoClienteExibicaoModel> AtualizarRendimento(int idRendimentoCliente, RendimentoClienteModel rendimentoAtualizar)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var rendimento = await _contexto.RendimentoCliente.FirstOrDefaultAsync(r => r.ID.Equals(idRendimentoCliente));

            if (rendimento == null)
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_NaoLocalizado, EnumMensagemTipo.banco);
                return null;
            }

            int idConta = await criarContaOuAtualizar(rendimento.IdContaCliente, rendimentoAtualizar.ContaCliente, usuario.Cliente.ID);
            int idContaRecebimento = rendimentoAtualizar.ContaClienteRecebimento is null 
                ? idConta
                : await criarContaOuAtualizar(rendimento.IdContaClienteRecebimento, rendimentoAtualizar.ContaClienteRecebimento, usuario.Cliente.ID);

            if (_mensagens.PossuiErros)
                return null;

            rendimento.SetContas(idConta, idContaRecebimento);

            switch (rendimento.Convenio.ID)
            {
                case Convenio.INSS:
                    (rendimento as RendimentoClienteInssDominio).SetPropriedadesAtualizadas(
                           rendimentoAtualizar.IdUf,
                           rendimentoAtualizar.ValorRendimento,
                           rendimentoAtualizar.Matricula,
                           rendimentoAtualizar.IdConvenioOrgao,
                           rendimentoAtualizar.IdInssEspecieBeneficio ?? 0,
                           rendimentoAtualizar.DataInscricaoBeneficio,
                           rendimentoAtualizar.MargemDisponivel,
                           rendimentoAtualizar.MargemDisponivelCartao
                       );
                    break;
                case Convenio.SIAPE:
                    (rendimento as RendimentoClienteSiapeDominio).SetPropriedadesAtualizadas(
                        rendimentoAtualizar.IdUf,
                        rendimentoAtualizar.ValorRendimento,
                        rendimentoAtualizar.Matricula,
                        rendimentoAtualizar.IdConvenioOrgao,
                        rendimentoAtualizar.IdSiapeTipoFuncional ?? 0,
                        rendimentoAtualizar.MatriculaInstituidor,
                        rendimentoAtualizar.PossuiRepresentacaoPorProcurador,
                        rendimentoAtualizar.DataAdmissao,
                        rendimentoAtualizar.NomeInstituidor,
                        rendimentoAtualizar.MargemDisponivel,
                        rendimentoAtualizar.MargemDisponivelCartao
                    );
                    break;
                case Convenio.MARINHA:
                    (rendimento as RendimentoClienteMarinhaDominio).SetPropriedadesAtualizadas(
                        rendimentoAtualizar.IdUf,
                           rendimentoAtualizar.ValorRendimento,
                           rendimentoAtualizar.Matricula,
                           rendimentoAtualizar.IdConvenioOrgao,
                           rendimentoAtualizar.IdMarinhaTipoFuncional ?? 0,
                           rendimentoAtualizar.IdMarinhaCargo,
                           rendimentoAtualizar.MargemDisponivel,
                           rendimentoAtualizar.MargemDisponivelCartao
                    );
                    break;
                case Convenio.AERONAUTICA:
                    (rendimento as RendimentoClienteAeronauticaDominio).SetPropriedadesAtualizadas(
                        rendimentoAtualizar.IdUf,
                           rendimentoAtualizar.ValorRendimento,
                           rendimentoAtualizar.Matricula,
                           rendimentoAtualizar.IdConvenioOrgao,
                           rendimentoAtualizar.IdAeronauticaTipoFuncional ?? 0,
                           rendimentoAtualizar.IdAeronauticaCargo,
                           rendimentoAtualizar.MargemDisponivel,
                           rendimentoAtualizar.MargemDisponivelCartao
                    );
                    break;
                default:
                    break;
            }

            await _contexto.SaveChangesAsync();

            return await obterRendimento(idRendimentoCliente, usuario.Cliente.ID);
        }

        private async Task<int> criarContaOuAtualizar(int idContaAtual, ContaClienteModel requisicao, int idCliente)
        {
            int idConta = requisicao.IdContaCliente.GetValueOrDefault();
            if (!requisicao.IdContaCliente.HasValue)
            {
                var novaConta = await _contaClienteServico.CriarConta(requisicao, idCliente);
                
                idConta = novaConta.ID;
            } else {
                if (idContaAtual != requisicao.IdContaCliente.Value)
                {
                    await _contaClienteServico.VerificarContaCliente(idContaAtual, idCliente);

                    if (_mensagens.PossuiErros)
                        return -1;
                }

                var conta = await _contexto.ContasCliente.FindAsync(requisicao.IdContaCliente.Value);

                conta.SetPropriedadesAtualizadas(
                    requisicao.IdBanco,
                    (TipoConta)requisicao.IdTipoConta,
                    requisicao.Agencia,
                    requisicao.Conta
                );

                await _contexto.SaveChangesAsync();
            }

            return idConta;
        }

        public async Task<bool> RemoverRendimento(int idRendimentoCliente)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return false;

            var rendimento = await _contexto.RendimentoCliente.FirstOrDefaultAsync(r => r.ID.Equals(idRendimentoCliente) && r.IdCliente.Equals(usuario.Cliente.ID));

            if (rendimento == null)
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_NaoLocalizado, EnumMensagemTipo.banco);
                return false;
            }

            rendimento.AlternarAtivo(false);

            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<RendimentoSiapeConsultaMargemModel> ConsultarMargem(int? idRendimentoCliente)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            if (idRendimentoCliente.HasValue)
            {
                var rendimento = await _contexto.RendimentoCliente
                    .Include(r => r.ConvenioOrgao)
                    .FirstOrDefaultAsync(r => r.ID.Equals(idRendimentoCliente) && r.IdCliente.Equals(usuario.Cliente.ID));

                if (rendimento == null)
                {
                    _mensagens.AdicionarErro(Mensagens.Rendimento_NaoLocalizado, EnumMensagemTipo.banco);
                    return null;
                }

                if (!(rendimento is RendimentoClienteSiapeDominio))
                {
                    _mensagens.AdicionarErro(Mensagens.Rendimento_TipoInvalido, EnumMensagemTipo.banco);
                    return null;
                }

                if (!(rendimento as RendimentoClienteSiapeDominio).DataLiberacaoConsultaMargem.HasValue)
                {
                    (rendimento as RendimentoClienteSiapeDominio).RegistrarAceiteConsultaMargem();
                    await _contexto.SaveChangesAsync();
                }

                return await _consignadoServico.ConsultarMargemSiape(rendimento.ConvenioOrgao.Codigo, rendimento.Matricula);
            }

            return await _consignadoServico.ConsultarMargemSiape(null, null);
        }

        #region Importação

        public async Task ImportarRendimentos(IEnumerable<ClienteRendimentoDto> rendimentosCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var rendimentos = await converterRendimentos(rendimentosCliente, unidadesFederativas);
            foreach (var rendimento in rendimentos)
            {
                if (new RendimentoClienteModelValidacao().Validate(rendimento).IsValid)
                    await GravarRendimento(rendimento);
            }
        }

        private async Task<IEnumerable<RendimentoClienteModel>> converterRendimentos(IEnumerable<ClienteRendimentoDto> rendimentos, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            List<RendimentoClienteModel> rendimentosModel = new List<RendimentoClienteModel>();
            foreach (var rendimento in rendimentos)
            {
                var rendimentoModel = new RendimentoClienteModel();
                rendimentoModel.Convenio = rendimento.Conveniada.GetEnumByDescription<Convenio>();
                rendimentoModel.DataAdmissao = rendimento.Admissao.Value;
                rendimentoModel.DataInscricaoBeneficio = rendimento.Admissao.Value;
                rendimentoModel.Matricula = rendimento.Matricula;
                rendimentoModel.MatriculaInstituidor = rendimento.MatriculaInstituidorSIAPE;
                rendimentoModel.PossuiRepresentacaoPorProcurador = false;
                rendimentoModel.ValorRendimento = rendimento.Salario ?? 0;
                rendimentoModel.IdSiapeTipoFuncional = rendimento.FuncaoSIAPE ?? 0;
                rendimentoModel.IdUf = unidadesFederativas.FirstOrDefault(uf => uf.Sigla.Equals(rendimento.UF))?.Id ?? 0;

                var orgao = await _convenioServico.ObterOrgaoPorCodigo(rendimento.Orgao);
                rendimentoModel.IdConvenioOrgao = orgao.Id;

                var banco = await _bancarioServico.ObterBancoPorCodigo(rendimento.Banco);

                var especieBeneficio = await _convenioServico.ObterInssEspecieBeneficioPorCodigo(rendimento.EspecieINSS);
                rendimentoModel.IdInssEspecieBeneficio = especieBeneficio.Id;

                var tipoConta = await _bancarioServico.ObterTipoContaPorSigla(rendimento.TipoConta);


                var conta = new ContaClienteModel()
                {
                    IdBanco = banco.Id,
                    IdTipoConta = tipoConta.Id,
                    Agencia = rendimento.Agencia,
                    Conta = rendimento.Conta,
                };

                rendimentoModel.ContaCliente = conta;
                rendimentoModel.ContaClienteRecebimento = conta;

                rendimentosModel.Add(rendimentoModel);
            }

            return rendimentosModel;
        }

        #endregion

        private IQueryable<RendimentoClienteDominio> obterConsultaBase()
        {
            var consulta = _contexto.RendimentoCliente
                .Include(r => r.Convenio)
                .Include(r => r.ConvenioOrgao)
                .Include(r => r.Uf)
                .Include(r => r.ContaCliente)
                    .ThenInclude(c => c.Banco)
                .Include(r => r.ContaCliente)
                    .ThenInclude(c => c.TipoConta)
                .Include(r => r.ContaCliente)
                    .ThenInclude(c => c.FormaRecebimento)
                .Include(r => r.ContaClienteRecebimento)
                    .ThenInclude(c => c.Banco)
                .Include(r => r.ContaClienteRecebimento)
                    .ThenInclude(c => c.TipoConta)
                .Include(r => r.ContaClienteRecebimento)
                    .ThenInclude(c => c.FormaRecebimento)
                .Include(r => (r as RendimentoClienteInssDominio).EspecieBeneficio)
                .Include(r => (r as RendimentoClienteSiapeDominio).TipoFuncional)
                .AsNoTracking();

            return consulta;
        }

        private async Task<RendimentoClienteExibicaoModel> obterRendimento(int idRendimentoCliente, int idCliente)
        {
            var rendimento = await obterConsultaBase()
                .FirstOrDefaultAsync(r => r.ID == idRendimentoCliente && r.IdCliente == idCliente);

            if (rendimento == null)
                return null;

            return new RendimentoClienteExibicaoModel(rendimento);
        }

        private static RendimentoClienteSiapeDominio obterRentimentoSiape(RendimentoClienteModel rendimento, UsuarioDominio usuario, int idConta, int idContaRecebimento)
            => new RendimentoClienteSiapeDominio(
                                idConta,
                                idContaRecebimento,
                                usuario.Cliente.ID,
                                rendimento.Convenio,
                                rendimento.IdConvenioOrgao.GetValueOrDefault(),
                                rendimento.IdUf,
                                rendimento.ValorRendimento,
                                rendimento.Matricula,
                                rendimento.IdSiapeTipoFuncional ?? 0,
                                rendimento.MatriculaInstituidor,
                                rendimento.PossuiRepresentacaoPorProcurador,
                                rendimento.DataAdmissao,
                                rendimento.NomeInstituidor
                            );

        private async Task<RendimentoClienteDominio> obterRendimentoInss(RendimentoClienteModel rendimento, UsuarioDominio usuario, int idConta, int idContaRecebimento)
        {
            var orgaoInss = await _contexto.ConvenioOrgaos.FirstOrDefaultAsync(c => c.IdConvenio == Convenio.INSS);

            return new RendimentoClienteInssDominio(
                idConta,
                idContaRecebimento,
                usuario.Cliente.ID,
                rendimento.Convenio,
                orgaoInss.ID,
                rendimento.IdUf,
                rendimento.ValorRendimento,
                rendimento.Matricula,
                rendimento.IdInssEspecieBeneficio ?? 0,
                rendimento.DataInscricaoBeneficio
            );
        }

        private async Task<RendimentoClienteDominio> obterRendimentoMarinha(RendimentoClienteModel rendimento, UsuarioDominio usuario, int idConta, int idContaRecebimento)
        {

            if (rendimento.IdMarinhaTipoFuncional == null || rendimento.IdMarinhaTipoFuncional == 0)
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_TipoMarinha_TipoFuncionalNaoInformado, EnumMensagemTipo.formulario);
                return null;
            }

            var tipoFuncional = await _contexto.MarinhaTiposFuncionais.FirstOrDefaultAsync(c => c.ID == rendimento.IdMarinhaTipoFuncional);

            if(tipoFuncional.Sigla == "S" && (rendimento.IdMarinhaCargo == null || rendimento.IdMarinhaCargo == 0))
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_TipoMarinha_CargoNaoInformado, EnumMensagemTipo.formulario);
                return null;
            }

            return new RendimentoClienteMarinhaDominio(
                                idConta,
                                idContaRecebimento,
                                usuario.Cliente.ID,
                                rendimento.Convenio,
                                rendimento.IdConvenioOrgao.GetValueOrDefault(),
                                rendimento.IdUf,
                                rendimento.ValorRendimento,
                                rendimento.Matricula,
                                rendimento.IdMarinhaTipoFuncional ?? 0,
                                rendimento.IdMarinhaCargo ?? 0);
        }

        private async Task<RendimentoClienteDominio> obterRendimentoAeronautica(RendimentoClienteModel rendimento, UsuarioDominio usuario, int idConta, int idContaRecebimento)
        {
            if (rendimento.IdAeronauticaTipoFuncional == null || rendimento.IdAeronauticaTipoFuncional == 0)
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_TipoAeronautica_TipoFuncionalNaoInformado, EnumMensagemTipo.formulario);
                return null;
            }
            var tipoFuncional = await _contexto.AeronauticaTiposFuncionais.FirstOrDefaultAsync(c => c.ID == rendimento.IdAeronauticaTipoFuncional);

            if (tipoFuncional.Sigla == "S" && (rendimento.IdAeronauticaCargo == null || rendimento.IdAeronauticaCargo == 0))
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_TipoAeronautica_CargoNaoInformado, EnumMensagemTipo.formulario);
                return null;
            }

            return new RendimentoClienteAeronauticaDominio(
                                idConta,
                                idContaRecebimento,
                                usuario.Cliente.ID,
                                rendimento.Convenio,
                                rendimento.IdConvenioOrgao.GetValueOrDefault(),
                                rendimento.IdUf,
                                rendimento.ValorRendimento,
                                rendimento.Matricula,
                                rendimento.IdAeronauticaTipoFuncional ?? 0,
                                rendimento.IdAeronauticaCargo ?? 0);
        }

    }
}

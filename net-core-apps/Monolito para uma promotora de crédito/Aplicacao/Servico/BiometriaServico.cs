using Aplicacao.Model.Anexo;
using Aplicacao.Model.Biometria;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers;
using Infraestrutura.Providers.Unico;
using Infraestrutura.Providers.Unico.DTO;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class BiometriaServico : ServicoBase, IBiometriaServico
    {

        private readonly IUnicoProvider _unicoProvider;
        private readonly IProviderAzure _azureProvider;

        public BiometriaServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto,
            IUnicoProvider unicoProvider,
            IProviderAzure providerAzure
        ) : base(mensagens, usuarioLogin, contexto)
        {
            _unicoProvider = unicoProvider;
            _azureProvider = providerAzure;
        }

        public async Task<bool> ExecutarBiometria()
        {
            var usuarioAutenticado = await ObterDadosUsuarioAutenticado();

            var biometria = await _contexto.BiometriaClientes
                            .Include(biometria => biometria.Cliente.Usuario)
                            .Where(biometria => biometria.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            if (biometria != null)
            {
                _mensagens.AdicionarErro(Mensagens.Biometria_JaCadastrada, EnumMensagemTipo.formulario);
                return false;
            }

            // Verificar se existe anexo para a biometria
            var anexo = await _contexto.Anexos
                        .Include(anexo => anexo.Cliente.Usuario)
                        .Include(anexo => anexo.TipoDocumento)
                        .Where( anexo => anexo.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario) )
                        .Where( anexo => anexo.IdTipoDocumento == TipoDocumento.SelfieBiometria )
                        .AsNoTracking()
                        .FirstOrDefaultAsync();


            if (anexo is null)
            {
                _mensagens.AdicionarErro(Mensagens.Anexo_TipoDocumentoSefileBiometriaNaoLocalizado, EnumMensagemTipo.formulario);
                return false;
            }

            var imagemBase64 = await _azureProvider.ObterBase64Azure(anexo.Link);

            if (string.IsNullOrEmpty(imagemBase64))
            {
                _mensagens.AdicionarErro(Mensagens.Selfie_NaoEncontrada, EnumMensagemTipo.formulario);
                return false;
            }

            UnicoRequisicaoCriarProcessoDto req = new UnicoRequisicaoCriarProcessoDto()
            {
                subject = new UnicoRequisicaoCriarProcessoSubjectDto()
                {
                    Code = usuarioAutenticado.CPF,
                    Name = usuarioAutenticado.Cliente.Nome
                },
                imagebase64 = imagemBase64,
                onlySelfie = true
            };

            var retorno = await _unicoProvider.CriarProcesso(req);

            RegistroBiometriaUnicoDominio registroBiometria = new RegistroBiometriaUnicoDominio(usuarioAutenticado.Cliente.ID);
            await _contexto.AddAsync(registroBiometria);
            await _contexto.SaveChangesAsync();

            if (retorno.Error != null)
            {
                registroBiometria.RegistraCodigoErro(retorno.Error.Code);
                await _contexto.SaveChangesAsync();
                if (retorno.Error.Code < 600)
                {
                    _mensagens.AdicionarErro(Mensagens.ProvedorUnico_SelfieNaoIdentificado, EnumMensagemTipo.formulario);
                    return false;
                }
                _mensagens.AdicionarErro(Mensagens.ProvedorUnico_ErroAutenticarSefie, EnumMensagemTipo.formulario);
                return false;
            }

            registroBiometria.RegistraBiometriaCodigo(retorno.Id);
            await _contexto.SaveChangesAsync();

            biometria = new BiometriaClienteDominio(usuarioAutenticado.Cliente.ID
                                                   , registroBiometria.ID);
            await _contexto.AddAsync(biometria);
            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<BiometriaConsultaModel> ObterSituacaoBiometria()
        {
            var biometria = await _contexto.BiometriaClientes
                            .Include(biometria => biometria.Cliente.Usuario)
                            .Include(c => c.RegistroBiometriaUnico)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario));

            if (biometria is null)
            {
                _mensagens.AdicionarErro(Mensagens.Biometria_NaoEncontrada, EnumMensagemTipo.formulario);
                return null;
            }

            if (biometria.IdBiometriaSituacao == BiometriaSituacao.Concluido)
                return montaRetornoSucessoBiometria(biometria);

            var consultaSituacaoUnico = await _unicoProvider.BuscarProcesso(biometria.RegistroBiometriaUnico.Codigo);

            if (consultaSituacaoUnico is null)
            {
                _mensagens.AdicionarErro(Mensagens.ProviderUnico_NaoHouveSucessoNoRetornoDoProvedorConsultaAutenticacaoSelfie, EnumMensagemTipo.formulario);
                return montaRetornoSucessoBiometria(biometria);
            }

            if (consultaSituacaoUnico.Status != ((int)SituacaoRetornoUnico.Concluido))
            {

                var situacao = (int)BiometriaSituacao.Pendente;
                if (consultaSituacaoUnico.Status == (int)SituacaoRetornoUnico.Erro ||
                    consultaSituacaoUnico.Status == (int)SituacaoRetornoUnico.Cancelado)
                    situacao = (int)BiometriaSituacao.Falha;

                biometria.RegistroBiometriaUnico
                         .AtualizaScoreESituacaoBiometria(consultaSituacaoUnico.Score
                                                         , situacao);

                _mensagens.AdicionarErro(string.Format(Mensagens.Biometria_SelfieAuntenticadaSituacao, ((BiometriaSituacao)situacao).GetDescription())
                                        , EnumMensagemTipo.formulario);

                return montaRetornoSucessoBiometria(biometria);
            }

            var registroBiometria = biometria.RegistroBiometriaUnico;

            registroBiometria.AtualizaBiometria(
                consultaSituacaoUnico.Score
               , consultaSituacaoUnico.Liveness
               , consultaSituacaoUnico.FaceMatch
               , consultaSituacaoUnico.HasBiometry
               , consultaSituacaoUnico.Status
            );
            _contexto.Update(registroBiometria);

            biometria.AtualizaBiometriaCliente(consultaSituacaoUnico.Score);
            _contexto.Update(biometria);

            await _contexto.SaveChangesAsync();

            return montaRetornoSucessoBiometria(biometria);
        }

        private BiometriaConsultaModel montaRetornoSucessoBiometria(BiometriaClienteDominio biometria)
        {
            return new BiometriaConsultaModel()
            {
                IdBiometriaSituacao = (int)biometria.IdBiometriaSituacao,
                Mensagem = null
            };
        }

        public async Task<bool> ProcessarRetornoWebhookUnico(BiometriaWebhookRetornoUnicoModel retorno)
        {
            var registro = await _contexto.RegistrosBiometriaCliente
                                          .Include(x => x.BiometriaCliente)
                                          .FirstOrDefaultAsync(x => x.Codigo == retorno.data.id);

            if (retorno.data.status != ((int)SituacaoRetornoUnico.Concluido))
            {
                registro.AtualizaScoreESituacaoBiometria(retorno.data.score, retorno.data.status);
                _contexto.Update(registro);
                await _contexto.SaveChangesAsync();
                return true;
            }

            var consultaSituacaoUnico = await _unicoProvider.BuscarProcesso(retorno.data.id);

            registro.AtualizaBiometria(
                consultaSituacaoUnico.Score
               , consultaSituacaoUnico.Liveness
               , consultaSituacaoUnico.FaceMatch
               , consultaSituacaoUnico.HasBiometry
               , consultaSituacaoUnico.Status
            );
            _contexto.Update(registro);

            registro.BiometriaCliente.AtualizaBiometriaCliente(consultaSituacaoUnico.Score);
            _contexto.Update(registro.BiometriaCliente);

            await _contexto.SaveChangesAsync();

            return true;
        }
    }
}

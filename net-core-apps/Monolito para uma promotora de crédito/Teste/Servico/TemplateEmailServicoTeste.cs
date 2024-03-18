using Aplicacao.Servico;
using Dominio;
using Dominio.Enum.TemplateEmail;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class TemplateEmailServicoTeste: ServicoTesteBase
    {
        private readonly ITemplateEmailServico _templateEmailServico;

        public TemplateEmailServicoTeste(): base()
        {
            var templateBuilder = new TemplateBuilderServico(_mensagens);
            _templateEmailServico = new TemplateEmailServico(_contexto, _mensagens, templateBuilder);
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoNaoEncontrado_DeveRetornarErro()
        {
            await criarTemplatesDefaults();

            string resultado = null;
            try {
                resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha);
            }
            catch {}
            finally {
                Assert.True(_mensagens.PossuiErros);
                Assert.True(resultado == null);
            }
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoEncontradoEspecifico_DeveRetornarTemplateSemPartesDefaults()
        {
            await criarTemplatesDefaults();

            var templates = new TemplateEmailDominio[] {
                new TemplateEmailDominio("<div>Header especifico</div>", TemplateEmailTipo.Header, TemplateEmailFinalidade.RecuperacaoSenha),
                new TemplateEmailDominio("<div>Footer especifico</div>", TemplateEmailTipo.Footer, TemplateEmailFinalidade.RecuperacaoSenha),
                new TemplateEmailDominio("<div>Content</div>", TemplateEmailTipo.Content, TemplateEmailFinalidade.RecuperacaoSenha),
            };

            await _contexto.AddRangeAsync(templates);
            await _contexto.SaveChangesAsync();

            string resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado != null);
            Assert.DoesNotContain("default", resultado);
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoEncontradoESemPartesEspecificas_DeveRetornarTemplateComPartesDefaults()
        {
            await criarTemplatesDefaults();

            TemplateEmailDominio content = new TemplateEmailDominio("<div>Content</div>", TemplateEmailTipo.Content, TemplateEmailFinalidade.RecuperacaoSenha);
            await _contexto.TemplatesEmail.AddAsync(content);
            await _contexto.SaveChangesAsync();

            string resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado != null);
            Assert.Contains("default", resultado);
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoPossuiTags_DeveRetornarSubstituindoTags()
        {
            await criarTemplatesDefaults();

            TemplateEmailDominio content = new TemplateEmailDominio("<div>[[test]] [[test2]]</div>", TemplateEmailTipo.Content, TemplateEmailFinalidade.RecuperacaoSenha);
            await _contexto.TemplatesEmail.AddAsync(content);
            await _contexto.SaveChangesAsync();

            var chavesLayout = new Dictionary<string, object> (StringComparer.OrdinalIgnoreCase)
            {
                ["test"] = "unit",
                ["test2"] = "unit2",
            };

            string resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha, chavesLayout);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado != null);
            Assert.DoesNotContain("[[", resultado);
            Assert.DoesNotContain("test", resultado);
            Assert.Contains("unit", resultado);
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoPossuiTagsSemPassar_DeveRetornarRemoveTags()
        {
            await criarTemplatesDefaults();

            TemplateEmailDominio content = new TemplateEmailDominio("<div>[[test]] [[removeTest]]</div>", TemplateEmailTipo.Content, TemplateEmailFinalidade.RecuperacaoSenha);
            await _contexto.TemplatesEmail.AddAsync(content);
            await _contexto.SaveChangesAsync();

            var chavesLayout = new Dictionary<string, object> (StringComparer.OrdinalIgnoreCase)
            {
                ["test"] = "unit",
            };

            string resultado = null;
            try {
                resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha, chavesLayout);
            }
            catch {}
            finally {
                Assert.False(_mensagens.PossuiErros);
                Assert.True(resultado != null);
                Assert.DoesNotContain("[[", resultado);
                Assert.DoesNotContain("removeTest", resultado);
            }
        }

        [Fact]
        public async Task RequisitarTemplate_QuandoPossuiTagMultiNivel_DeveRetornarSubstituindoTags()
        {
            var usuarioTeste = new UsuarioDominio("User level name", "test@test.com", false, new CPF("1112223344"), "1234", new ClienteDominio("Client level name"));

            await criarTemplatesDefaults();

            TemplateEmailDominio content = new TemplateEmailDominio("<div>[[usuario.Nome]] [[usuario.cliente.Nome]] [[usuario.PropInexistente]]</div>", TemplateEmailTipo.Content, TemplateEmailFinalidade.RecuperacaoSenha);
            await _contexto.TemplatesEmail.AddAsync(content);
            await _contexto.SaveChangesAsync();

            var chavesLayout = new Dictionary<string, object> (StringComparer.OrdinalIgnoreCase)
            {
                ["usuario"] = usuarioTeste,
            };

            string resultado = null;
            try {
                resultado = await _templateEmailServico.GerarTemplate(TemplateEmailFinalidade.RecuperacaoSenha, chavesLayout);
            }
            catch {}
            finally {
                Assert.False(_mensagens.PossuiErros);
                Assert.True(resultado != null);
                Assert.DoesNotContain("[[", resultado);
                Assert.DoesNotContain("usuario.Nome", resultado);
                Assert.Contains("User level name", resultado);
                Assert.Contains("Client level name", resultado);
                Assert.DoesNotContain("PropInexistente", resultado);
            }
        }

        private async Task criarTemplatesDefaults()
        {
            TemplateEmailDominio header = new TemplateEmailDominio("<div>Header default</div>", TemplateEmailTipo.Header, TemplateEmailFinalidade.Default);
            TemplateEmailDominio footer = new TemplateEmailDominio("<div>Footer default</div>", TemplateEmailTipo.Footer, TemplateEmailFinalidade.Default);

            await _contexto.TemplatesEmail.AddAsync(header);
            await _contexto.TemplatesEmail.AddAsync(footer);

            await _contexto.SaveChangesAsync();
        } 
    }
}

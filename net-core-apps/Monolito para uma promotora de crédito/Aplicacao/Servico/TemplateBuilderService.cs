using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aplicacao.Servico
{
    public class TemplateBuilderServico
    {
        private readonly IBemMensagens _mensagens;

        public TemplateBuilderServico(IBemMensagens mensagens)
        {
            _mensagens = mensagens;
        }

        public string SubstituirChaves(string template, Dictionary<string, object> chaves)
        {
            MatchCollection tags = new Regex("\\[\\[(.*?)\\]\\]").Matches(template);

            var novoTemplate = template;

            foreach (Match tag in tags)
            {
                string valor = "";

                if (tag.Value.Contains("."))
                {
                    var tagParts = tag.Groups[1].Value.Split(".", 2);

                    object valorDicionario = null;
                    chaves.TryGetValue(tagParts[0], out valorDicionario);

                    if (valorDicionario != null)
                    {
                        valor = localizarValorNoObjeto(tagParts[1], valorDicionario);
                    }
                }
                else
                {
                    object valorDicionario = null;
                    chaves.TryGetValue(tag.Groups[1].Value, out valorDicionario);

                    if (valorDicionario != null && (valorDicionario is string || !valorDicionario.GetType().IsClass))
                    {
                        valor = (string)valorDicionario;
                    }
                }

                novoTemplate = novoTemplate.Replace(tag.Value, valor);
            }

            return novoTemplate;
        }

        private string localizarValorNoObjeto(string busca, object modelo)
        {
            var tagParts = busca.Split(".", 2);

            try
            {
                var nomePropriedade = $"{char.ToUpper(tagParts[0].First())}{tagParts[0].Substring(1)}";
                var valorPropriedade = modelo.GetType().GetProperty(nomePropriedade).GetValue(modelo, null);

                if (tagParts.Length > 1)
                {
                    return localizarValorNoObjeto(tagParts[1], valorPropriedade);
                }

                return valorPropriedade.ToString();
            }
            catch
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.TemplateBuilder_PropriedadeInexistente, modelo.GetType(), tagParts[0]), EnumMensagemTipo.exception);
                return "";
            }
        }
    }
}

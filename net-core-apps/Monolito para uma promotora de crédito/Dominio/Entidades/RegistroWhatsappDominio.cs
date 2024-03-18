using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Enum.TemplateWhatsapp;

namespace Dominio
{
    public class RegistroWhatsappDominio : EntidadeBase
    {
        public string CodigoReferenciaWhatsapp { get => String.Format("API{0:0000000000}", ID); }
        public TemplateWhatsapp IdTemplateWhatsapp {get; set;}
        public TemplateWhatsappDominio TemplateWhatsapp {get; private set;}
        
        public string NumeroTelefone {get; set;}
        public string ParametrosMensagem {get; set;}

        public int? IdUsuario { get; private set; }
        public UsuarioDominio Usuario { get; private set; }
        public int? CodigoOrigem { get; set; }


        public RegistroWhatsappDominio( TemplateWhatsapp idTemplateWhatsapp
                                      , string numeroTelefone
                                      , string parametrosMensagem
                                      , int? idUsuario
                                      , int? codigoOrigem ){
            this.IdTemplateWhatsapp = idTemplateWhatsapp;
            this.NumeroTelefone     = numeroTelefone;
            this.ParametrosMensagem = parametrosMensagem;
            this.IdUsuario          = idUsuario;
            this.CodigoOrigem       = codigoOrigem;
        }

        public RegistroWhatsappDominio( TemplateWhatsapp idTemplateWhatsapp
                                      , string numeroTelefone
                                      , Dictionary<string, string> mensagem
                                      , int? idUsuario
                                      , int? codigoOrigem)
                                      : this( idTemplateWhatsapp,
                                              numeroTelefone,
                                              "",
                                              idUsuario,
                                              codigoOrigem)
        {
            this.ParametrosMensagem = converteDicionarioParaString(mensagem);
        }

        private string converteDicionarioParaString (Dictionary<string, string> dictionary) {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }

    }
}

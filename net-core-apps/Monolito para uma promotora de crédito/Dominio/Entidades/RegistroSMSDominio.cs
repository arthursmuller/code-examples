using System;
using Dominio.Enum.TemplateSms;

namespace Dominio
{
    public class RegistroSmsDominio : EntidadeBase
    {
        public string CodigoReferenciaSms { get => String.Format("API{0:0000000000}", ID); }

        public TemplateSms IdTemplateSms { get; private set; }
        public TemplateSmsDominio TemplateSMS { get; private set; }


        public string NumeroTelefone { get; set; }
        
        public string Mensagem { get; set; }

        public int? IdUsuario { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public int? CodigoOrigem { get; set; }

        public RegistroSmsDominio( TemplateSms idTemplateSms
                                 , string numeroTelefone
                                 , string mensagem
                                 , int? idUsuario
                                 , int? codigoOrigem )
        {
            this.IdTemplateSms  = idTemplateSms;
            this.NumeroTelefone = numeroTelefone;
            this.Mensagem       = mensagem;
            this.IdUsuario      = idUsuario;
            this.CodigoOrigem   = codigoOrigem;
        }

    }
}
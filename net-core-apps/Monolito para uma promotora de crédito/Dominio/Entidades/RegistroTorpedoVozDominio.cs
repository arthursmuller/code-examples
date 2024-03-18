using System;
using Dominio.Enum.TemplateTorpedoVoz;

namespace Dominio
{
    public class RegistroTorpedoVozDominio : EntidadeBase
    {
        public string CodigoReferenciaTorpedoVoz { get => String.Format("API{0:0000000000}", ID); }
        public string NumeroTelefone { get; private set; }
        public string Mensagem { get; private set; }
        public TemplateTorpedoVoz IdTemplateTorpedoVoz { get; private set; }
        public TemplateTorpedoVozDominio TemplateTorpedoVoz { get; private set; }
        public int? IdUsuario { get; private set; }
        public UsuarioDominio Usuario { get; private set; }
        public int? CodigoOrigem { get; set; }

        public RegistroTorpedoVozDominio( TemplateTorpedoVoz idTemplateTorpedoVoz
                                        , string numeroTelefone
                                        , string mensagem
                                        , int? idUsuario
                                        , int? codigoOrigem)
        {
            this.NumeroTelefone       = numeroTelefone;
            this.Mensagem             = mensagem;
            this.IdTemplateTorpedoVoz = idTemplateTorpedoVoz;
            this.IdUsuario            = idUsuario;
            this.CodigoOrigem         = codigoOrigem;
        }

    }
}

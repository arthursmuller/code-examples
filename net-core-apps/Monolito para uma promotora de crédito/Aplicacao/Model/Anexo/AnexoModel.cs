using System;

namespace Aplicacao
{
    public class AnexoModel
    {
        public int Id { get; set; }

        public string LinkAnexo { get; set; }

        public TipoDocumentoModel TipoDocumento { get; set; }

        public UsuarioModel Usuario { get; set; }

        public int IdUsuario { get; set; }

        public string AnexoBase64 { get; set; }

        public string Extensao { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}

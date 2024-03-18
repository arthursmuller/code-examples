namespace Dominio
{
    public class TipoRegimeCasamentoBemDominio : EntidadeBase
    {
        public string Descricao { get; set; }
        public int? IdTipoRegimeCasamento { get; private set; }
        public TipoRegimeCasamentoDominio TipoRegimeCasamento { get; private set; }
    }
}

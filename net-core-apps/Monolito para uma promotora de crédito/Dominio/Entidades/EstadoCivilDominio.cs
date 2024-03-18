namespace Dominio
{
    public class EstadoCivilDominio : EntidadeBase
    {
        public string Descricao { get; private set; }

        public string Sigla { get; private set; }

        public EstadoCivilDominio(string descricao, string sigla)
        {
            Descricao = descricao;
            Sigla = sigla;
        }
    }
}

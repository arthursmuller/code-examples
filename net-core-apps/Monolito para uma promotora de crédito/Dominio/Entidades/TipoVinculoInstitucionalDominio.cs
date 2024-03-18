namespace Dominio
{
    public class TipoVinculoInstitucionalDominio : EntidadeBase
    {
        public string Nome { get; private set; }

        public TipoVinculoInstitucionalDominio(string nome)
        {
            Nome = nome;
        }
    }
}

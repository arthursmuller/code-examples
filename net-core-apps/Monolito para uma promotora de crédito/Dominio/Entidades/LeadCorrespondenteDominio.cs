namespace Dominio
{
    public class LeadCorrespondenteDominio : EntidadeBase
    {
        public string CNPJ { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public int IdMunicipio { get; private set; }
        public string Atividades { get; private set; }

        public MunicipioDominio Municipio { get; private set; }

        public LeadCorrespondenteDominio() { }

        public LeadCorrespondenteDominio(
            string cnpj,
            string nome,
            string telefone,
            string email,
            int idMunicipio,
            string atividades
        )
        {
            CNPJ = cnpj.ToString();
            Nome = nome;
            Telefone = telefone;
            Email = email?.ToLower();
            IdMunicipio = idMunicipio;
            Atividades = atividades;
        }
    }
}
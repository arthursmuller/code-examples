using Dominio.Enum;

namespace Dominio
{
    public class ConvenioDominio : EntidadeBase
    {
        public new Convenio ID { get; private set; }

        public string Nome { get; private set; }

        public string Codigo { get; private set; }

        public string Descricao { get; private set; }

        public ConvenioDominio() { }

        public ConvenioDominio(Convenio id, string nome, string codigo, string descricao)
        {
            ID = id;
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}

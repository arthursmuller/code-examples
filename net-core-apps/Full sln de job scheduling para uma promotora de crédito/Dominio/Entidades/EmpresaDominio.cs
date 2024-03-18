using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class EmpresaDominio: EntidadeBase
    {
        public string Nome { get; private set; }

        public IEnumerable<EmailFornecedorDominio> EmailFornecedores { get; private set; }
        public IEnumerable<SmsFornecedorDominio> SmsFornecedores { get; private set; }
        public IEnumerable<WhatsappFornecedorDominio> WhatsappFornecedores { get; private set; }
        public IEnumerable<TorpedoVozFornecedorDominio> TorpedoVozFornecedores { get; private set; }

        public EmpresaDominio(string nome)
        {
            Nome = nome;
        }
    }
}

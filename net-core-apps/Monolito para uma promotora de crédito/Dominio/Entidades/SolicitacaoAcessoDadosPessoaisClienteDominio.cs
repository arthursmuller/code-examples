using System;

namespace Dominio
{
    public class SolicitacaoAcessoDadosPessoaisClienteDominio : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }

        private DateTime _dataNascimento;
        public DateTime DataNascimento { get => _dataNascimento; private set => _dataNascimento = value.Date; }

        public string NomeMae { get; private set; }

        private string _email;
        public string Email { get => _email; private set => _email = value?.ToLower(); }

        public string TelefoneCompleto { get; private set; }
        public string Motivo { get; private set; }
        public bool NotificacaoEnviada { get; private set; }

        public SolicitacaoAcessoDadosPessoaisClienteDominio(string nome, string sobrenome, DateTime dataNascimento, string nomeMae, string motivo)
            => atribuirDadosBasicos(nome, sobrenome, dataNascimento, nomeMae, motivo);

        public void InformarAtualizarDadosContato(string email, string ddd, string telefone)
        {
            Email = email;
            TelefoneCompleto = $"{ddd}{telefone}";

            setDataAtualizacao();
        }

        public void AtualizarDadosBasicos(string nome, string sobrenome, DateTime dataNascimento, string nomeMae, string motivo)
        {
            atribuirDadosBasicos(nome, sobrenome, dataNascimento, nomeMae, motivo);
            setDataAtualizacao();
        }

        public void AlternarSituacaoEnvioNotificacao(bool notificacaoEnviada)
        {
            NotificacaoEnviada = notificacaoEnviada;

            setDataAtualizacao();
        }

        private void atribuirDadosBasicos(string nome, string sobrenome, DateTime dataNascimento, string nomeMae, string motivo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            NomeMae = nomeMae;
            Motivo = motivo;
        }
    }
}

using Dominio.Enum;
using SharedKernel.ValueObjects.v2;
using System;

namespace Dominio
{
    public class BiometriaClienteDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }
        public ClienteDominio Cliente { get; private set; }
        public DateTime DataEnvio { get; private set; } = DateTime.Now;
        public int Score { get; private set; }
        public bool Valido { get; private set; }
        public BiometriaSituacao IdBiometriaSituacao { get; private set; }
        public DateTime DataRetorno { get; private set; }

        public int IdRegistroBiometriaUnico { get; private set; }
        public RegistroBiometriaUnicoDominio RegistroBiometriaUnico { get; private set; }


        public BiometriaClienteDominio(int idCliente, int idRegistroBiometriaUnico)
        {
            IdCliente = idCliente;
            IdRegistroBiometriaUnico = idRegistroBiometriaUnico;
            Valido = false;
            IdBiometriaSituacao = BiometriaSituacao.Pendente;
        }

        public void AutalizaParaPendenteBiometriaSituacao()
        {
            IdBiometriaSituacao = BiometriaSituacao.Pendente;
            DataAtualizacao = DateTime.Now;
        }

        public void AtualizaBiometriaCliente(int score)
        {

            IdBiometriaSituacao = BiometriaSituacao.Falha;
            Score = score;

            if (score > 10)
            {
                Valido = true;
                IdBiometriaSituacao = BiometriaSituacao.Concluido;
            }

            DataRetorno = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }

    }
}

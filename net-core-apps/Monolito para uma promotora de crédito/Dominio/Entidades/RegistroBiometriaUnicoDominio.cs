using System;
using Dominio.Enum.TemplateSms;

namespace Dominio
{
    public class RegistroBiometriaUnicoDominio : EntidadeBase
    {

        public int IdCliente { get; private set; }
        public ClienteDominio Cliente { get; set; }
        public string Codigo { get; private set; }
        public DateTime DataEnvio { get; private set; } = DateTime.Now;
        public DateTime DataRetorno { get; private set; }
        public int Score { get; private set; }
        public bool Liveness { get; private set; }
        public bool FaceMatch { get; private set; }
        public bool PossuiBiometria { get; private set; }
        public int CodigoSituacaoBiometria { get; private set; }
        public int? CodigoErro { get; private set; }

        public BiometriaClienteDominio BiometriaCliente { get; private set; }

        public RegistroBiometriaUnicoDominio(int idCliente)
        {
            IdCliente = idCliente;
            Codigo = null;
            Liveness = false;
            FaceMatch = false;
            PossuiBiometria = false;
            CodigoSituacaoBiometria = 0;
        }

        public void RegistraBiometriaCodigo(string codigo)
        {
            Codigo = codigo;
            DataAtualizacao = DateTime.Now;
        }

        public void RegistraCodigoErro(int? codigoErro)
        {
            CodigoErro = codigoErro;
            DataAtualizacao = DateTime.Now;
        }


        public void AtualizaScoreESituacaoBiometria(int score
                                                   , int situacaoBiometria)
        {
            this.Score = score;
            this.CodigoSituacaoBiometria = situacaoBiometria;
            this.DataAtualizacao = DateTime.Now;
            this.DataRetorno = DateTime.Now;
        }

        public void AtualizaBiometria(int score
                                     , bool liveness
                                     , bool faceMatch
                                     , bool possuiBiometria
                                     , int situacaoBiometria)
        {
            this.Score = score;
            this.CodigoSituacaoBiometria = situacaoBiometria;
            this.Liveness = liveness;
            this.FaceMatch = faceMatch;
            this.PossuiBiometria = possuiBiometria;
            this.DataAtualizacao = DateTime.Now;
            this.DataRetorno = DateTime.Now;
        }

    }
}
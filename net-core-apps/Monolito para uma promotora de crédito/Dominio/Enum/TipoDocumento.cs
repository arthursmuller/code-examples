using System.ComponentModel;

namespace Dominio.Enum
{
    public enum TipoDocumento
    {
        [Description("IP")]
        IdentidadeProfissional = 1,

        [Description("CDR")]
        ComprovanteDeResidencia = 2,

        [Description("HOL")]
        Holerite = 3,

        [Description("PAS.EST")]
        PassaporteEstrangeiro = 4,

        [Description("FOTO")]
        FotoPessoal = 5,

        [Description("CR")]
        CertificadoReservista = 6,

        [Description("CNH")]
        CarteiraNacionalDeHabilitacao = 7,

        [Description("PAS.BRA")]
        PassaporteBrasileiro = 8,

        [Description("CIEP")]
        CedItentidadeEstrangeiro = 9,

        [Description("RIC")]
        RegistroIdentidadeCivil = 10,

        [Description("IM")]
        IdentidadeMilitar = 11,

        [Description("CI")]
        CarteiraIdentidade = 12,

        [Description("CTPS")]
        CarteiraDeTrabalho = 13,

        [Description("FOR/09")]
        TermoAutorizacaoBeneficiario = 14,

        [Description("FOTO-PEFIL")]
        FotoPerfil = 15,

        [Description("SELFIE-BIOMETRIA")]
        SelfieBiometria = 16,

        [Description("SELFIE")]
        Selfie = 17
    }
}

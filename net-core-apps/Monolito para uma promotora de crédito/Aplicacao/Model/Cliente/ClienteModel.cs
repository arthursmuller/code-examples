using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Cliente
{
    public class ClienteModel
    {
        [Required]
        public int IdGenero { get; set; }

        [Required]
        public int IdEstadoCivil { get; set; }

        [Required]
        public int IdGrauInstrucao { get; set; }

        [Required]
        public int IdCidadeNatal { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        public string Filiacao1 { get; set; }

        public string Filiacao2 { get; set; }

        [Required]
        public bool DeficienteVisual { get; set; }

        public int? IdLoja { get; set; }
        public int? IdProfissao { get; set; }
    }
}

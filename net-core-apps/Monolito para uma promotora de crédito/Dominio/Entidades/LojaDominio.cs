using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace Dominio
{
    public class LojaDominio : EnderecoBaseDominio
    {
        public string Nome { get; private set; }
        public Point Geolocalizacao { get; private set; }
        public string MensagemApresentacao { get; private set; }

        public IEnumerable<IntencaoOperacaoDominio> IntencoesOperacao { get; private set; }
        public IEnumerable<ClienteDominio> Clientes { get; private set; }
        public IEnumerable<LeadDominio> Leads { get; private set; }
        public List<TelefoneLojaDominio> Telefones { get; private set; }

        public LojaDominio() : base() { }

        public LojaDominio(
            string nome,
            int idMunicipio,
            string bairro,
            int idTipoLogradouro,
            string logradouro,
            int? numero,
            string complemento,
            string cep,
            string mensagemApresentacao,
            List<TelefoneLojaDominio> telefones,
            Point geolocalizacao) : base(idMunicipio, bairro, idTipoLogradouro, logradouro, numero, complemento, cep)
        {
            Nome = nome;
            MensagemApresentacao = mensagemApresentacao;
            Telefones = telefones;
            Geolocalizacao = geolocalizacao;
        }
    }
}

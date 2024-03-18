using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class LeadServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly IProviderMaxMind _providerMaxMind;
        private readonly PlataformaClienteContexto _contexto;
        private readonly LojaServico _lojaServico;

        public LeadServico(IBemMensagens mensagens, PlataformaClienteContexto contexto, IProviderMaxMind providerMaxMind, LojaServico lojaServico)
        {
            _mensagens = mensagens;
            _contexto = contexto;
            _providerMaxMind = providerMaxMind;
            _lojaServico = lojaServico;
        }

        public async Task<LeadModel> BuscarLead(int id)
        {
            var lead = await _contexto.Leads
                .Include(i => i.Convenio)
                .Include(i => i.Loja)
                    .ThenInclude(l => l.Telefones)
                .Include(i => i.Loja)
                    .ThenInclude(l => l.Municipio)
                        .ThenInclude(m => m.UF)
                .Include(i => i.Loja)
                    .ThenInclude(l => l.TipoLogradouro)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (lead == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Lead_IdNaoEncontrado, id), EnumMensagemTipo.banco);

                return null;
            }

            return ConverterParaModel(lead);
        }

        public async Task<IEnumerable<LeadModel>> ListarLeads()
        {
            var leads = await _contexto.Leads
                .Include(i => i.Convenio)
                .Include(i => i.Loja)
                    .ThenInclude(l => l.Municipio)
                        .ThenInclude(m => m.UF)
                .Include(i => i.Loja)
                    .ThenInclude(l => l.TipoLogradouro)
                .AsNoTracking()
                .ToListAsync();

            if (leads == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Lead_NenhumaEncontrada, EnumMensagemTipo.banco);

                return null;
            }

            var leadsModel = new List<LeadModel>();
            leads.ForEach(l => leadsModel.Add(ConverterParaModel(l)));

            return leadsModel;
        }

        public async Task<LeadNovaModel> GravarLead(LeadCriacaoModel lead)
        {
            if (verificarSeGeolocalizacaoNaoInformada(lead.Latitude, lead.Longitude))
            {
                (lead.Latitude, lead.Longitude) = _providerMaxMind.ObterLatitudeLongitude();
            }

            string linkContatoWhatsAppLoja = null;
            if (lead.DesejaContatoWhatsApp)
                (lead.IdLoja, linkContatoWhatsAppLoja) = await _lojaServico.ObterLinkContatoWhatsApp(lead.IdLoja, lead.Latitude ?? 0, lead.Longitude ?? 0);

            var cpf = validarCpf(lead.CPF);
            if (_mensagens.PossuiErros)
                return null;

            var novaLead = new LeadDominio
                (
                    cpf,
                    lead.Nome,
                    lead.Telefone,
                    lead.Celular,
                    lead.Email,
                    (Produto?)lead.IdProduto,
                    null,
                    lead.Latitude.Value,
                    lead.Longitude.Value,
                    lead.OrigemRequisicaoPalavraChave,
                    lead.OrigemRequisicaoMidia,
                    lead.OrigemRequisicaoConteudo,
                    lead.OrigemRequisicaoTermo,
                    lead.OrigemRequisicaoCampanha,
                    lead.IdLoja,
                    lead.DesejaContatoWhatsApp,
                    linkContatoWhatsAppLoja,
                    lead.Quitacao
                );

            if (lead.IdConvenio.HasValue)
            {
                novaLead.SetConvenio((Convenio)lead.IdConvenio);
            }

            await _contexto.AddAsync(novaLead);
            await _contexto.SaveChangesAsync();

            return new LeadNovaModel { Id = novaLead.ID, Latitude = novaLead.Latitude, Longitude = novaLead.Longitude, LinkContatoWhatsAppLoja = novaLead.LinkContatoWhatsAppLoja };
        }

        public async Task<LeadAtualizadaModel> AtualizarLead(LeadAtualizacaoModel leadAtualizado, int id)
        {
            var lead = await _contexto.Leads.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (lead == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Lead_IdNaoEncontrado, id), EnumMensagemTipo.banco);

                return null;
            }

            (int?, string) retContatoWhatsApp = (null, null);
            if (leadAtualizado.DesejaContatoWhatsApp && leadAtualizado.IdLoja != lead.IdLoja)
                retContatoWhatsApp = await _lojaServico.ObterLinkContatoWhatsApp(leadAtualizado.IdLoja, lead.Latitude, lead.Longitude);

            var cpf = validarCpf(leadAtualizado.CPF);
            if (_mensagens.PossuiErros)
                return null;

            lead.SetPropriedadesAtualizadas
            (
                cpf,
                leadAtualizado.Nome,
                leadAtualizado.Telefone,
                leadAtualizado.Celular,
                leadAtualizado.Email,
                (Produto?)leadAtualizado.IdProduto,
                null,
                leadAtualizado.IdLoja,
                leadAtualizado.DesejaContatoWhatsApp,
                retContatoWhatsApp.Item1 != null ? retContatoWhatsApp.Item2 : lead.LinkContatoWhatsAppLoja
            );

            if (leadAtualizado.IdConvenio.HasValue)
            {
                lead.SetConvenio((Convenio)leadAtualizado.IdConvenio);
            }

            _contexto.Update(lead);

            await _contexto.SaveChangesAsync();

            return new LeadAtualizadaModel { Id = lead.ID, Latitude = lead.Latitude, Longitude = lead.Longitude, LinkContatoWhatsAppLoja = lead.LinkContatoWhatsAppLoja };
        }

        public async Task<bool> AtualizarLojaLead(int id, int idLoja)
        {
            var lead = await _contexto.Leads.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (lead == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Lead_IdNaoEncontrado, id), EnumMensagemTipo.banco);

                return false;
            }

            (int?, string) retContatoWhatsApp = (null, null);
            if (lead.DesejaContatoWhatsApp && lead.IdLoja != idLoja)
                retContatoWhatsApp = await _lojaServico.ObterLinkContatoWhatsApp(idLoja, lead.Latitude, lead.Longitude);

            lead.SetInformacoesLoja(idLoja, retContatoWhatsApp.Item1 != null ? retContatoWhatsApp.Item2 : lead.LinkContatoWhatsAppLoja);

            _contexto.Update(lead);

            await _contexto.SaveChangesAsync();

            return true;
        }

        public static LeadModel ConverterParaModel(LeadDominio lead)
        {
            return new LeadModel
            {
                Id = lead.ID,
                Nome = lead.Nome,
                CPF = lead.CPF,
                Telefone = lead.Telefone,
                Celular = lead.Celular,
                Email = lead.Email,
                Convenio = lead.Convenio != null ? ConvenioServico.ConverterParaModel(lead.Convenio) : null,
                Latitude = lead.Latitude,
                Longitude = lead.Longitude,
                OrigemRequisicaoPalavraChave = lead.OrigemRequisicaoPalavraChave,
                OrigemRequisicaoMidia = lead.OrigemRequisicaoMidia,
                OrigemRequisicaoConteudo = lead.OrigemRequisicaoConteudo,
                OrigemRequisicaoTermo = lead.OrigemRequisicaoTermo,
                OrigemRequisicaoCampanha = lead.OrigemRequisicaoCampanha,
                Loja = lead.Loja != null ? new LojaModel(lead.Loja) : null,
                DesejaContatoWhatsApp = lead.DesejaContatoWhatsApp,
                LinkContatoWhatsAppLoja = lead.LinkContatoWhatsAppLoja,
                DataAtualizacao = lead.DataAtualizacao
            };
        }

        private CPF validarCpf(string cpfLead)
        {
            var cpf = new CPF(cpfLead);
            if (string.IsNullOrWhiteSpace(cpfLead))
                return cpf;

            cpf.IsValid(_mensagens);
            return cpf;
        }

        private bool verificarSeGeolocalizacaoNaoInformada(double? latitude, double? longitude)
        {
            return latitude == null || longitude == null || latitude == 0 || longitude == 0;
        }
    }
}
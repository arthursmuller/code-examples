using B.Configuracao;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class LeadCorrespondenteServico
    {
        private readonly PlataformaClienteContexto _contexto;
        private readonly Configuracao _configuracao;
        private readonly IEmailServico _emailServico;

        public LeadCorrespondenteServico(PlataformaClienteContexto contexto,
            IEmailServico emailServico, Configuracao configuracao)
        {
            _contexto = contexto;
            _configuracao = configuracao;
            _emailServico = emailServico;
        }

        public async Task<int?> GravarLead(LeadCorrespondenteCriacaoModel lead)
        {
            var novaLead = new LeadCorrespondenteDominio
                (
                    lead.CNPJ,
                    lead.Nome,
                    lead.Telefone,
                    lead.Email,
                    lead.IdMunicipio,
                    lead.Atividades
                );

            await _contexto.AddAsync(novaLead);
            await _contexto.SaveChangesAsync();

            var leadConsulta = await _contexto
                .LeadsCorrespondente
                .AsNoTracking()
                .Include(l => l.Municipio)
                    .ThenInclude(m => m.UF)
                .FirstOrDefaultAsync(l => l.ID == novaLead.ID);

            await enviarEmailSuporte(leadConsulta);

            return novaLead.ID;
        }

        private async Task<bool> enviarEmailSuporte(LeadCorrespondenteDominio lead)
        {
            var emailSuporteComercial = _configuracao.BuscarParametro("email-suporte-comercial");

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["lead"] = lead,
            };

            var resultado = await _emailServico.RegistrarEmail(
                TemplateEmailFinalidade.LeadCorrespondente,
                $"Lead Correspondente ##{lead.ID} - {lead.Nome}",
                new string[] { emailSuporteComercial },
                chavesLayout
            );

            return resultado;
        }
    }
}
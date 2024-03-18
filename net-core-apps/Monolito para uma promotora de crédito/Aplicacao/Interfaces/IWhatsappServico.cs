using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Enum.TemplateWhatsapp;
using SharedKernel.ValueObjects.v2;

namespace Aplicacao.Servico
{
    public interface IWhatsappServico
    {
        Task<bool> RegistrarWhatsapp(  TemplateWhatsapp template
                              , int? codigoOrigem
                              , Guid guid
                              , Fone fone
                              , Dictionary<string, string> valoresMensagem );
    }
}
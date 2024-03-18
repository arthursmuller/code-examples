using System.ComponentModel.DataAnnotations;
using Dominio.Resource;

namespace Aplicacao.CustomValidation
{
    public class CNPJ : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {  
            var resultado = BrazilianUtils.Cnpj.IsValid((string)value, false);

            return resultado
                ? ValidationResult.Success
                : new ValidationResult(Mensagens.CNPJ_Invalido);
        }
    }
}
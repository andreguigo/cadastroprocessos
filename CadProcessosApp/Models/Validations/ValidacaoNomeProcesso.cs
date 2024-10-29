using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CadProcessosApp.Models.Validations
{
    public class ValidacaoNomeProcesso : ValidationAttribute
    {
        protected override ValidationResult IsValid(object valor, ValidationContext contextoValidacao)
        {
            var nomeProcesso = valor as string;
            
            if (string.IsNullOrWhiteSpace(nomeProcesso))
                return new ValidationResult("O Nome do Processo é obrigatório");
            
            return ValidationResult.Success!;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CadProcessosApp.Models.Validations
{
    public class ValidacaoNpu : ValidationAttribute
    {
        protected override ValidationResult IsValid(object valor, ValidationContext contextoValidacao)
        {
            var npu = valor as string;
            var regex = new Regex(@"^\d{7}-\d{2}\.\d{4}\.\d{1}\.\d{2}\.\d{4}$");

            if (npu == null || !regex.IsMatch(npu))
                return new ValidationResult("O NPU deve seguir o formato 1111111-11.1111.1.11.1111 e conter só números");
            
            return ValidationResult.Success!;
        }
    }
}
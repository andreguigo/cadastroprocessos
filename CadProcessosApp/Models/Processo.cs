using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using CadProcessosApp.Models.Validations;

namespace CadProcessosApp.Models
{
    public class Processo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ValidacaoNomeProcesso] 
        public string? NomeProcesso { get; set; }
        
        [ValidacaoNpu] 
        [NotMapped]
        public string? NPU { get; set; }
        
        [Column("NPU")]
        public string? NPURaw { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataVisualizacao { get; set; }
        public string? UF { get; set; }
        public string? Municipio { get; set; }

        public void ConverterNPUParaRaw()
        {
            NPURaw = Regex.Replace(NPU!, @"\D", "");
        }

        public void ConverterRawParaNPU()
        {
            string npuString = Regex.Replace(NPURaw!, @"\D", "");
            NPU = $"{npuString.Substring(0, 7)}-{npuString.Substring(7, 2)}.{npuString.Substring(9, 4)}.{npuString.Substring(13, 1)}.{npuString.Substring(14, 2)}.{npuString.Substring(16, 4)}";
        }
    }
}
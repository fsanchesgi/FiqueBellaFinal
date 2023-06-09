using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Contabilidade
    {
        public int ContabilidadeId { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o valor da entrada ou saída")]
        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal Valor { get; set; }

        [Display(Name = "Tipo de entrada ou saída")]
        public int TipoId { get; set; }
        public virtual Tipo Tipo { get; set; }

        [Display(Name = "Entrada ou saída de valores")]
        public int EntradaSaidaId { get; set; }
        public virtual EntradaSaida EntradaSaida { get; set; }
    }
}

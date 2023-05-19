using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [StringLength(40, ErrorMessage = "O tamanho máximo é 40 caracteres.")]
        [Required(ErrorMessage = "Informe o nome da cliente.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Informe o endereço da cliente.")]
        [Display(Name = "Endereço")]
        public string Endereco1 { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Display(Name = "Complemento")]
        public string? Endereco2 { get; set; }

        [StringLength(40, ErrorMessage = "O tamanho máximo é 40 caracteres.")]
        [Required(ErrorMessage = "Informe o bairro da cliente.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho máximo é 50 caracteres.")]
        [Required(ErrorMessage = "Informe a cidade da cliente.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o telefone da cliente")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
    }
}

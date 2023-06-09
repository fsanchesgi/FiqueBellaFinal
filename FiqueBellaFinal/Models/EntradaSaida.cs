using System.ComponentModel.DataAnnotations;

namespace FiqueBellaFinal.Models
{
    public class EntradaSaida
    {
        public int EntradaSaidaId { get; set; }

        [StringLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição.")]
        public string Descricao { get; set; }

        public List<Contabilidade> Contabilidade { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Tipo
    {
        public int TipoId { get; set; }

        [StringLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição.")]
        public string TipoDesc { get; set; }

        public List<Contabilidade> Contabilidade { get; set; }
    }
}

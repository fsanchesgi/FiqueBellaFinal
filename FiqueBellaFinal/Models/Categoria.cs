using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Display(Name = "Nome da categoria")]
        public string CategoriaNome { get; set; }
        [Display(Name = "Descrição da categoria")]
        public string Descricao { get; set; }

        public List<Procedimento> Procedimento { get; set; }
    }
}

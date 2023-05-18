using FiqueBellaFinal.Models;
using System.IO;

namespace FiqueBellaFinal.ViewModels
{
    public class ProcedimentoListViewModel
    {
        public IEnumerable<Procedimento> Procedimentos { get; set; }
        public string CategoriaAtual { get; set; }
    }
}

using FiqueBellaFinal.Models;
using System.IO;
using System.Collections.Generic;

namespace FiqueBellaFinal.ViewModels
{
    public class ProcedimentoListViewModel
    {
        public IEnumerable<Procedimento> Procedimentos { get; set; } = new List<Procedimento>();
        public string CategoriaAtual { get; set; } = string.Empty;
    }
}

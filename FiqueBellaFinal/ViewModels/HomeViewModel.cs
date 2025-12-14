using FiqueBellaFinal.Models;
using System.Collections.Generic;
using System.IO;

namespace FiqueBellaFinal.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Procedimento> ProcedimentosPreferidos { get; set; } = new List<Procedimento>();
        public IEnumerable<Procedimento> ProcedimentosEmPromocao { get; set; } = new List<Procedimento>();
    }
}

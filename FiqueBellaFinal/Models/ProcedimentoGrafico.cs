using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiqueBellaFinal.Models
{
    [Keyless]
    public class ProcedimentoGrafico
    {
        public string ProcedimentoNome { get; set; }

        public int ProcedimentoQuantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProcedimentoValorTotal { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiqueBellaFinal.Models
{
    [Keyless] // ← ESSENCIAL
    public class ProcedimentoGrafico
    {
        // mantenha TODAS as propriedades que já existem
        // não remova nada

        // exemplo:
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}

using FiqueBellaFinal.Models;
using FiqueBellaFinal.Data;
using FiqueBellaFinal.Services;

namespace FiqueBellaFinal.Areas.Admin.Services
{
    public class GraficoServices
    {
        private readonly AppDbContext _context;

        public GraficoServices(AppDbContext context)
        {
            _context = context;
        }

        public List<ProcedimentoGrafico> GetProcedimento(int dias = 360) 
        {
            var data = DateTime.Now.AddDays(-dias);

            var procedimentos = (from ag in _context.Agendas
                                join p in _context.Procedimentos on ag.ProcedimentoId equals p.ProcedimentoId
                                where ag.Dia >= data
                                group ag by new { ag.ProcedimentoId, p.Nome }
                                into a
                                select new
                                {
                                    ProcedimentoNome = a.Key.Nome,
                                    ProcedimentoQuantidade = a.Count(),
                                    ProcedimentoValorTotal = a.Sum(x => x.Procedimento.Preco)
                                });

            var lista = new List<ProcedimentoGrafico>();

            foreach(var item in procedimentos)
            {
                var procedimento = new ProcedimentoGrafico();
                procedimento.ProcedimentoNome = item.ProcedimentoNome;
                procedimento.ProcedimentoQuantidade = item.ProcedimentoQuantidade;
                procedimento.ProcedimentoValorTotal = item.ProcedimentoValorTotal;
                lista.Add(procedimento);
            }

            return lista;
        }

    }
}

using FiqueBellaFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FiqueBellaFinal.Areas.Admin.Services
{
    public class RelatorioContabilidadeServices
    {
        private readonly AppDbContext _appDbContext;

        public RelatorioContabilidadeServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Contabilidade>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _appDbContext.Contabilidades select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= maxDate.Value);
            }

            return await resultado
                            .Include(p => p.Tipo)
                            .Include(p => p.EntradaSaida)
                            .OrderByDescending(x => x.Data)
                            .ToListAsync();
        }
    }
}

using FiqueBellaFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using FiqueBellaFinal.Data;

namespace FiqueBellaFinal.Areas.Admin.Services
{
    public class RelatorioServices
    {
        private readonly AppDbContext _appDbContext;

        public RelatorioServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Agenda>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _appDbContext.Agendas select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Dia >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.Dia <= maxDate.Value);
            }

            return await resultado
                            .Include(p => p.Procedimento)
                            .OrderByDescending(x => x.Dia)
                            .ToListAsync();
        }
    }
}

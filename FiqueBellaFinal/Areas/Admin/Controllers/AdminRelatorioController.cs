using Microsoft.AspNetCore.Mvc;                    // Para Controller e IActionResult
using Microsoft.AspNetCore.Authorization;          // Para [Authorize]
using FiqueBellaFinal.Data;                         // Para AppDbContext
using FiqueBellaFinal.Services;                     // Para serviços (GraficoServices, RelatorioServices, etc.)
using Microsoft.Extensions.Options;                // Para IOptions<> (ex.: configuração de imagens)


namespace FiqueBellaFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminRelatorioController : Controller
    {
        private readonly RelatorioServices _relatorioServices;

        public AdminRelatorioController(RelatorioServices relatorioServices)
        {
            _relatorioServices = relatorioServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Relatorio(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioServices.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}

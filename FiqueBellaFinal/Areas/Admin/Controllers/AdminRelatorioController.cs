using Microsoft.AspNetCore.Mvc;                  // Para Controller, IActionResult, etc.
using Microsoft.AspNetCore.Authorization;        // Para [Authorize]
using FiqueBellaFinal.Data;                       // Para AppDbContext
using FiqueBellaFinal.Services;                   // Para RelatorioServices, GraficoServices, etc.
using Microsoft.Extensions.Options;              // Para IOptions<>
using FiqueBellaFinal.Models;                     // Para modelos se usados
using FiqueBellaFinal.Repositories.Interfaces;   // Para repositórios se usados



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

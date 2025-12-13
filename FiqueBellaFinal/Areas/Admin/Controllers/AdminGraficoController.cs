using Microsoft.AspNetCore.Mvc;                    // Para Controller e IActionResult
using Microsoft.AspNetCore.Authorization;          // Para [Authorize]
using FiqueBellaFinal.Data;                         // Para AppDbContext
using FiqueBellaFinal.Services;                     // Para serviços (GraficoServices, RelatorioServices, etc.)
using Microsoft.Extensions.Options;                // Para IOptions<> (ex.: configuração de imagens)

namespace FiqueBellaFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminGraficoController : Controller
    {        
        private readonly GraficoServices _graficoServices;
        
        public AdminGraficoController(GraficoServices graficoServices)
        {
            _graficoServices = graficoServices;
        }

        public JsonResult AgendaProcedimento (int dias)
        {
            var procedimentosTotais = _graficoServices.GetProcedimento(dias);
            return Json(procedimentosTotais);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Mensal()
        {
            return View();
        }

    }
}

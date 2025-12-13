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

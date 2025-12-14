using Microsoft.AspNetCore.Mvc;                 // IActionResult, Controller
using Microsoft.AspNetCore.Authorization;       // [Authorize]
using FiqueBellaFinal.Data;                      // AppDbContext
using FiqueBellaFinal.Models;                    // Se usar Models
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.Extensions.Options;             // IOptions<>
using Microsoft.EntityFrameworkCore;

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

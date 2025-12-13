using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;  // para [Authorize]
using FiqueBellaFinal.Models;               // se usar modelos
using FiqueBellaFinal.Repositories.Interfaces; // se usar repositórios
using Microsoft.Extensions.Options;        // se usar IOptions<>


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

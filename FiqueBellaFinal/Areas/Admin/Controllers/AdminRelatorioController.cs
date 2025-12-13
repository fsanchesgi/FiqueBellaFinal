using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.EntityFrameworkCore;

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

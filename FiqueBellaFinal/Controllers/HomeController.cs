using Microsoft.AspNetCore.Mvc;                    // Para Controller e IActionResult
using FiqueBellaFinal.Data;                         // Se usar AppDbContext (se houver)
using FiqueBellaFinal.Models;                       // Para modelos
using FiqueBellaFinal.Repositories.Interfaces;     // Para repositórios
using FiqueBellaFinal.Areas.Admin.Services;
using ReflectionIT.Mvc.Paging;


namespace FiqueBellaFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProcedimentoRepository _procedimentoRepository;

        public HomeController(IProcedimentoRepository procedimentoRepository)
        {
            _procedimentoRepository = procedimentoRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                ProcedimentosPreferidos = _procedimentoRepository.ProcedimentosPreferidos,
                ProcedimentosEmPromocao = _procedimentoRepository.ProcedimentosEmPromocao
            };
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult QuemSomos()
        {
            return View();
        }
    }
}

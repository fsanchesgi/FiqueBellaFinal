using Microsoft.AspNetCore.Mvc;
using FiqueBellaFinal.ViewModels;
using FiqueBellaFinal.Repositories.Interfaces;
using System.Diagnostics;
using FiqueBellaFinal.Models; // <--- garante que ErrorViewModel seja reconhecido

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
            try
            {
                var homeViewModel = new HomeViewModel
                {
                    ProcedimentosPreferidos = _procedimentoRepository.ProcedimentosPreferidos,
                    ProcedimentosEmPromocao = _procedimentoRepository.ProcedimentosEmPromocao
                };

                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar HomeController: " + ex.Message);

                return View(new HomeViewModel
                {
                    ProcedimentosPreferidos = Enumerable.Empty<Models.Procedimento>(),
                    ProcedimentosEmPromocao = Enumerable.Empty<Models.Procedimento>()
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult QuemSomos()
        {
            return View();
        }
    }
}

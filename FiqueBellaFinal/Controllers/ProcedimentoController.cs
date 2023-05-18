using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FiqueBellaFinal.Controllers
{
    public class ProcedimentoController : Controller
    {
        private readonly IProcedimentoRepository _procedimentoRepository;

        public ProcedimentoController(IProcedimentoRepository procedimentoRepository)
        {
            _procedimentoRepository = procedimentoRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Procedimento> procedimentos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                procedimentos = _procedimentoRepository.Procedimentos.OrderBy(l => l.ProcedimentoId);
                categoriaAtual = "Todos os procedimentos";
            }
            else
            {
                procedimentos = _procedimentoRepository.Procedimentos.Where(l => l.Categoria.CategoriaNome.Equals(categoria)).OrderBy(l => l.Nome);
                categoriaAtual = categoria;
            }

            var procedimentosListViewModel = new ProcedimentoListViewModel
            {
                Procedimentos = procedimentos,
                CategoriaAtual = categoriaAtual
            };

            return View(procedimentosListViewModel);
        }
        public ViewResult Search(string searchString)
        {
            IEnumerable<Procedimento> procedimentos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                procedimentos = _procedimentoRepository.Procedimentos.OrderBy(p => p.ProcedimentoId);
                categoriaAtual = "Todos os pratos";
            }
            else
            {
                procedimentos = _procedimentoRepository.Procedimentos
                    .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));
                if (procedimentos.Any())
                    categoriaAtual = "Procedimentos";
                else
                    categoriaAtual = "Nenhum procedimento foi encontrado";
            }
            return View("~/Views/Procedimento/List.cshtml", new ProcedimentoListViewModel
            {
                Procedimentos = procedimentos,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}

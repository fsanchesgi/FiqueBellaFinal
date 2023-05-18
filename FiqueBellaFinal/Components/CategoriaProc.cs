using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiqueBellaFinal.Components
{
    public class CategoriaProc : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaProc(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(categorias);
        }
    }
}

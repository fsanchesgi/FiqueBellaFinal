using FiqueBellaFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FiqueBellaFinal.Controllers
{
    public class GaleriaController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GaleriaController(IWebHostEnvironment webHostEnvironment,
                                 IOptions<ConfigurationImagens> myConfig)
        {
            _myConfig = myConfig.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_webHostEnvironment.WebRootPath,
                                        _myConfig.NomePastaImagensAtendimentos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesAtendimento = _myConfig.NomePastaImagensAtendimentos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo na galeria!";
            }

            model.Files = files;

            return View(model);
        }
    }
}

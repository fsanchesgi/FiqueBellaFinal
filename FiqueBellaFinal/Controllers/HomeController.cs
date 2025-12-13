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

    public IActionResult QuemSomos()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

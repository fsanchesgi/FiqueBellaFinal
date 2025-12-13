using Microsoft.AspNetCore.Mvc;

namespace FiqueBellaFinal.Controllers
{
    [Route("/")]
    public class HealthController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("SITE ONLINE - RAILWAY OK");
        }
    }
}

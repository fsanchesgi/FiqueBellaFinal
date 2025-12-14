using Microsoft.AspNetCore.Mvc;

namespace FiqueBellaFinal.Controllers
{
    // 🔴 ANTES: [Route("/")]
    // ✅ AGORA: endpoint exclusivo para health check
    [Route("health")]
    public class HealthController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("SITE ONLINE - RAILWAY OK");
        }
    }
}

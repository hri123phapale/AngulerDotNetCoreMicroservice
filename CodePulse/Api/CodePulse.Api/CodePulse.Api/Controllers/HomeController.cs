using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

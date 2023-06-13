using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

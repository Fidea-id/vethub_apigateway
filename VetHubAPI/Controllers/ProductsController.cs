using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

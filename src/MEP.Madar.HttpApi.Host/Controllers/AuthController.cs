using Microsoft.AspNetCore.Mvc;

namespace MEP.Madar.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

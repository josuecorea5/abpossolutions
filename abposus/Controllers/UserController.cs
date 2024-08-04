using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

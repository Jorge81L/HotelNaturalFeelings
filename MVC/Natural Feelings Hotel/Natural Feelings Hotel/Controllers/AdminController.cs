using Microsoft.AspNetCore.Mvc;

namespace Natural_Feelings_Hotel.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

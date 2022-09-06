using Microsoft.AspNetCore.Mvc;

namespace Natural_Feelings_Hotel.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

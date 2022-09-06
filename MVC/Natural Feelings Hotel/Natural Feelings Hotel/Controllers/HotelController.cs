using Microsoft.AspNetCore.Mvc;

namespace Natural_Feelings_Hotel.Controllers
{
    public class HotelController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        public HotelController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComoLlegar()
        {
            return View();
        }

        public ActionResult ContacUs()
        {
            return View();
        }
    }
}

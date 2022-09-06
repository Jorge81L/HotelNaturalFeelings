using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Admi.API;
using Natural_Feelings_Admi.Models;

namespace Natural_Feelings_Admi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> loginUser(AdmiModel admiModel)
        {
            Console.WriteLine("boton iniciar sesion");
            Admin temp = new Admin();
            AdmiModel aux = (AdmiModel) await temp.getAdmi(admiModel);
            if(aux != null)
            {
                if (aux.Name == admiModel.Name && aux.Authenticated)
                {
                    HttpContext.Session.SetString("User", aux.Name);

                    ViewBag.userName = HttpContext.Session.GetString("User");

                    return View("inicio");
                }
                else
                {
                    ViewBag.validation = false;
                    return View("Index");
                }
            }
            else
            {
                ViewBag.validation = false;
                return View("Index");
            }
        }

        public async Task<IActionResult> logout()
        {
            Console.WriteLine("boton cerrar sesion");

            HttpContext.Session.Remove("User");

            return View("Index");
        }

        public IActionResult Inicio()
        {

            ViewBag.userName = HttpContext.Session.GetString("User");
            return View("Inicio");
        }
    }
}

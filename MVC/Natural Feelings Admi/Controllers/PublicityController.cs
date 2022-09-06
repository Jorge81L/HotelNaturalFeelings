using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Admi.API;
using Natural_Feelings_Admi.Models;
using Natural_Feelings_Hotel.Models;
using System.IO;

namespace Natural_Feelings_Hotel.Controllers
{
    public class PublicityController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public PublicityController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public ActionResult AddAdvertising()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdvertisingModel advertising)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Advertising advertisingAPI = new Advertising();
            if(!advertising.Link.Contains("https://"))
            {
                advertising.Link = "https://" + advertising.Link;
            }
            ViewBag.Response = advertisingAPI.create(advertising).GetAwaiter().GetResult();
            return View("AddAdvertising");
        }
    }
}

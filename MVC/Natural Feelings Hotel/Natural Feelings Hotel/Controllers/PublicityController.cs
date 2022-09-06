using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel.API;
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdvertisingModel advertising)
        {
            Advertising advertisingAPI = new Advertising();
            if(!advertising.Link.Contains("https://"))
            {
                advertising.Link = "https://" + advertising.Link;
            }
            advertising.AdImage = this.SubirImagen(advertising);
            advertising.Image = null;

            ViewBag.Response = advertisingAPI.create(advertising).GetAwaiter().GetResult();
            return View("AddAdvertising");
        }

        private string SubirImagen(AdvertisingModel advertising)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(advertising.Image.ImageForm.FileName);
            string extension = Path.GetExtension(advertising.Image.ImageForm.FileName);
            fileName = fileName + DateTime.Now.ToString("ddMMyyyy-hhmmss")+extension;
            string path = Path.Combine(wwwRootPath+"\\AdvertisingImages", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                advertising.Image.ImageForm.CopyTo(fileStream);
            }
            return "/AdvertisingImages/" + fileName;
        }
    }
}

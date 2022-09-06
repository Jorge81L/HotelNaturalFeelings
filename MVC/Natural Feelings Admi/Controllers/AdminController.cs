using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Admi.API;
using Natural_Feelings_Admi.Models;

namespace Natural_Feelings_Admi.Controllers
{
    public class AdminController : Controller
    {

        public List<SeasonModel> GetSeason()
        {
            Admin admin = new Admin();
            return admin.GetSeason().GetAwaiter().GetResult();
        }

        public List<OfferModel> GetOffer()
        {
            Admin admin = new Admin();
            return admin.GetOffer().GetAwaiter().GetResult();
        }

        public OfferModel GetOfferId(int codigo)
        {
            Admin admin = new Admin();
            return admin.BuscarOferta(codigo).GetAwaiter().GetResult();
        }

        public IActionResult DeleteSeason(SeasonModel season)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin=new Admin();
            ViewBag.result=admin.DeteleSeason(season).Result;
            Console.WriteLine(ViewBag.result);
            ViewBag.Season = this.GetSeason();
            return View("AdminTemporadas");
        }

        public IActionResult DeleteOffer(OfferModel offer)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin = new Admin();
            ViewBag.result = admin.DeteleOffer(offer).Result;
            ViewBag.Offer = this.GetOffer();
            return View("AdminOfertas");
        }

        public IActionResult CreateOffer(OfferModel offer)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin = new Admin();
            ViewBag.resultcrear = admin.CreateOffer(offer).Result;
            ViewBag.Offer = this.GetOffer(); 
            return View("AdminOfertas");
        }

        public IActionResult EditOffer(OfferModel offer)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin = new Admin();
            ViewBag.resultedit = admin.EditOffer(offer).Result;
            ViewBag.Offer = this.GetOffer();
            return View("AdminOfertas");
        }

        public IActionResult EditSeason(SeasonModel season)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin = new Admin();
            ViewBag.resultedit2 = admin.EditSeason(season).Result;
            ViewBag.Season = this.GetSeason();
            return View("AdminTemporadas");
        }

        public IActionResult CreateSeason(SeasonModel season)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Admin admin = new Admin();
            ViewBag.result3 = admin.CreateSeason(season).Result;
            ViewBag.Season = this.GetSeason();
            return View("AdminTemporadas");
        }

        public SeasonModel GetSeasonId(int codigo)
        {
            Admin admin = new Admin();
            return admin.BuscarSeason(codigo).GetAwaiter().GetResult();
        }

        public IActionResult AdminTemporadas()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            ViewBag.Season=this.GetSeason();
            return View();
        }

        public IActionResult AdminOfertas()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            ViewBag.Offer = this.GetOffer();
            return View();
        }
    }
}

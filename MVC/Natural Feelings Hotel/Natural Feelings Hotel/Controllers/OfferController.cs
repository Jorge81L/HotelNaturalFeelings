using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel.API;
using Natural_Feelings_Hotel.Models;

namespace Natural_Feelings_Hotel.Controllers
{

    [Route("[controller]")]
    public class OfferController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public OfferController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public ActionResult Offer(int id)
        {
            Offer offer = new Offer();

            OfferModel offerModel = offer.GetOffer(id).GetAwaiter().GetResult();

            ViewBag.Offer = offerModel;

            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Admi.API;
using Natural_Feelings_Admi.Models;
using Newtonsoft.Json;

namespace Natural_Feelings_Admi.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Manage()
        {
            Room room = new Room();
            ViewBag.Rates = room.GetRates().GetAwaiter().GetResult();
            ViewBag.Rooms = room.GetRooms().GetAwaiter().GetResult();
            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }

        public List<RateModel> GetRate()
        {
            Room room = new Room();
            return room.GetRates().GetAwaiter().GetResult();
        }

        public ActionResult Rate()
        {
            ViewBag.Rate = this.GetRate();
            return View("Rate");
        }


        public bool UpdateRoomState(RoomModel roomModel)
        {
            Room room = new Room();
            return room.UpdateRoomState(roomModel).GetAwaiter().GetResult();
        }

       

        public RateModel GetRateById(RateModel rateModel)
        {
            Room room = new Room();
            return room.GetRateById(rateModel).GetAwaiter().GetResult();
        }
        /*
        public bool UpdateRate()
        {
            Console.WriteLine(JsonConvert.SerializeObject(rateModel));
            Room room = new Room();
            return room.UpdateRate(rateModel).GetAwaiter().GetResult();
        }
        */

        public RoomModel GetRoomById(RoomModel roomModel)
        {
            
            Room roomAPI = new Room();
            return roomAPI.GetRoomById(roomModel).GetAwaiter().GetResult();
        }

        public IActionResult UpdateRoom(RoomModel roomModel)
        {
            Console.WriteLine(JsonConvert.SerializeObject(roomModel));
            Room room = new Room();
            ViewBag.Rates = room.GetRates().GetAwaiter().GetResult();
            ViewBag.Rooms = room.GetRooms().GetAwaiter().GetResult();
            ViewBag.Response = room.UpdateRoom(roomModel).GetAwaiter().GetResult();
            return View("Manage");
        }

        public IActionResult UpdateRate(RateModel rateModel)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(rateModel));
            Room room = new Room();
            ViewBag.Rates = room.GetRates().GetAwaiter().GetResult();
            ViewBag.Rooms = room.GetRooms().GetAwaiter().GetResult();
            ViewBag.Response = room.UpdateRate(rateModel).GetAwaiter().GetResult();
            return View("Manage");
        }
    }
}

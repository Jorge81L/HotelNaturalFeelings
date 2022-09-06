using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Admi.Models;
using Natural_Feelings_Admi.API;
using System.Text;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;

namespace Natural_Feelings_Admi.Controllers
{
    public class HotelController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public HotelController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public ActionResult TodayStatus()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }

        public ActionResult AvailabilityRoomView()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            return View("AvailabilityRoom");
        }
        public string AvailabilityRoom(ReservationModel imput)
        {
            Hotel hotelAPI = new Hotel();
            List<ReservationModel> availabilityRoomModels = hotelAPI.GetAvailabilityRoom(imput).GetAwaiter().GetResult();
            //Console.WriteLine("_______________" + imput.Habitation.IdHabitation);
            string cadena = "";
            foreach (var item in availabilityRoomModels)
            {
                cadena += "<tr>" +
                            "<td scope = 'row'>" + item.Habitation.IdHabitation + "</td>" +
                             "<td>" + item.Habitation.Rate.Name + "</td>" +
                             "<td>" + item.Habitation.Rate.Price + "</td>" +
                         "</tr>";
            }
            Console.WriteLine(availabilityRoomModels);
            return cadena;
        }

        public async Task<ActionResult> PDFReporte()
        {
            try
            {
                Document doc = new Document();
                string nombreDocumento = "Reporte" + DateTime.Now.ToString("dd-MMMM-yy") + ".pdf";
                FileStream file = new FileStream(nombreDocumento, FileMode.OpenOrCreate);
                PdfWriter writer = PdfWriter.GetInstance(doc, file);
                doc.AddTitle("Reporte" + DateTime.Now.ToString("dd - MMMM - yy"));
                doc.Open();

                Hotel jefaturaData = new Hotel();

                List<TodayStatusModel> reportes = await jefaturaData.reporte();
                PdfPTable table = new PdfPTable(3);

                Font fontTitle = FontFactory.GetFont(FontFactory.COURIER_BOLD, 14);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);

                doc.Add(new Paragraph(20, "Reporte de Habitaciones " + DateTime.Now, fontTitle));
                doc.Add(new Chunk("\n"));

                float[] widths = new float[3];
                for (int i = 0; i < 3; i++)
                    widths[i] = 4f;

                table.SetWidths(widths);
                table.WidthPercentage = 100;

                PdfPCell cell = new PdfPCell(new Phrase("columns"));
                cell.Colspan = 3;

                table.AddCell(new Phrase("Numero"));
                table.AddCell(new Phrase("Tipo"));
                table.AddCell(new Phrase("Estado"));

                var status="";
                foreach (TodayStatusModel reporte in reportes)
                {
                    if(String.Compare(reporte.Status, "TRUE") != 0)
                        {
                        status = "Disponible";
                    }
                        else
                    {
                        status = "Ocupado";
                    }
                    table.AddCell(new Phrase(reporte.ID_Habitation + "", font9));
                    table.AddCell(new Phrase(reporte.ID_Rate + "", font9));
                    table.AddCell(new Phrase(status + "", font9));
                }
                doc.Add(table);
                writer.Close();
                doc.Close();
                file.Dispose();
                var pdf = new FileStream(nombreDocumento, FileMode.Open, FileAccess.Read);
                
                return File(pdf, "application/pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ModifyPages()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }
        public async Task<ActionResult> AboutUS()
        {

            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }
        public bool UpdateAboutUs(HotelModel imput)
        {
            HotelModel hotelModel = new HotelModel {
                IdHotel = 0,
                Description = "",
                AboutUS = imput.AboutUS,
                Adress = ""
            };
            Hotel hotelAPI = new Hotel();
            bool response = hotelAPI.UpdateAboutUs(hotelModel).Result;
            if (response)
                return true;
            else
                return false;
        }
        public async Task<ActionResult> HomeModify()
        {

            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }
        public ActionResult UpdateHome(HotelModel hotelModel)
        {
            hotelModel.IdHotel = 0;
            hotelModel.AboutUS = "";
            hotelModel.Adress = "";
            Hotel hotelAPI = new Hotel();
            bool response = hotelAPI.UpdateHome(hotelModel).Result;
            //Console.WriteLine(hotelModel.Description+"///"+hotelModel.Image.ImageForm);
            //bool response = false;
            return RedirectToAction("UpdateHome");
        }

        public IActionResult Facilities()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Facility facility = new Facility();
            ViewBag.facilities = facility.GetFacilities().GetAwaiter().GetResult();
            return View();
        }

        public IActionResult CreateFacility(FacilityModel facilityModel)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Facility facility = new Facility();
            ViewBag.facilities = facility.CreateFacility(facilityModel).GetAwaiter().GetResult();
            ViewBag.facilities = facility.GetFacilities().GetAwaiter().GetResult();
            return View("Facilities");
        }

        public IActionResult UpdateFacility(FacilityModel facilityModel)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Facility facility = new Facility();
            ViewBag.facilities = facility.UpdateFacility(facilityModel).GetAwaiter().GetResult();
            ViewBag.facilities = facility.GetFacilities().GetAwaiter().GetResult();
            return View("Facilities");
        }

        public IActionResult DeleteFacility(FacilityModel facilityModel)
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Facility facility = new Facility();
            ViewBag.facilities = facility.DeleteFacility(facilityModel).GetAwaiter().GetResult();
            ViewBag.facilities = facility.GetFacilities().GetAwaiter().GetResult();
            return View("Facilities");
        }

        public IActionResult ReservationList()
        {
            ViewBag.userName = HttpContext.Session.GetString("User");
            Room room = new Room();
            ViewBag.reservations = room.GetReservations().GetAwaiter().GetResult();
            return View();
        }
        public async Task<ActionResult> Adress()
        {

            ViewBag.userName = HttpContext.Session.GetString("User");
            return View();
        }

        public bool UpdateAdress(HotelModel imput)
        {
            HotelModel hotelModel = new HotelModel
            {
                IdHotel = 0,
                Description = "",
                AboutUS = "",
                Adress = imput.Adress
            };
            Hotel hotelAPI = new Hotel();
            bool response = hotelAPI.UpdateAdress(hotelModel).Result;
            if (response)
                return true;
            else
                return false;
        }
    }
}

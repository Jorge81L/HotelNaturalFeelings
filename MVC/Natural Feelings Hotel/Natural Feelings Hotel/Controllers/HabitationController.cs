using EASendMail;
using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel.API;
using Natural_Feelings_Hotel.Models;
using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Controllers
{
    public class HabitationController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        public HabitationController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Facilities()
        {
            return View();
        }

        public ActionResult Rate()
        {
            return View();
        }

        public ActionResult Reservation()
        {
            Habitation habitation = new Habitation();
            ViewBag.Rates = habitation.ReadRate().GetAwaiter().GetResult();
            return View();
        }

        public HabitationModel ValidateDateRange(ReservationModel reservationModel)
        {

            //Console.WriteLine(reservationModel.StartDate);
            //Console.WriteLine(reservationModel.EndDate);
            Habitation habitation = new Habitation();
            HabitationModel prueba = habitation.ValidateDateRange(reservationModel).GetAwaiter().GetResult();
            return prueba;
        }

        public ActionResult ReserveHabitation(ReservationModel reservationModel)
        {
            //Console.WriteLine(reservationModel.StartDate);
            //Console.WriteLine(reservationModel.EndDate);
            Habitation habitation = new Habitation();
            reservationModel.Habitation.Image = new ImageModel
            {
                IdImage = "",
                ImageContent = null
            };
            HabitationModel room = habitation.GetHabitationById(reservationModel.Habitation).GetAwaiter().GetResult();
            reservationModel.Habitation = room;
            ViewBag.Reservation = reservationModel;

            return View();
        }

        public ActionResult ReservationBill(ReservationModel reservationModel)
        {
            Habitation habitation = new Habitation();
            ReservationModel reservation = habitation.SetReservation(reservationModel).GetAwaiter().GetResult();
            ViewBag.Reservation = reservation;
            if (reservation.IdReservation != 0)
                this.sendEmail(reservation);
            return View();
        }

        public bool sendEmail(ReservationModel reservation)
        {
            try
            {
                string message = "¡Reservación realizada " + reservation.User.Name + "!" +
           "\nGracias, su reservación fue realizada exitosamente." +
            "\nNúmero de reservación: " + reservation.IdReservation + "" +
            "\nGracias por preferirnos!";

                SmtpMail objetoCorreo = new SmtpMail("TryIt");

                objetoCorreo.From = "pruebaing77@gmail.com";
                objetoCorreo.To = reservation.User.EmailAddress;
                objetoCorreo.Subject = "Reservación Natural Feelings Hotel";
                objetoCorreo.TextBody = message;

                SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");

                objetoServidor.User = "pruebaing77@gmail.com";
                objetoServidor.Password = "hktlmfllmyzpzwzh";
                objetoServidor.Port = 587;
                objetoServidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient objetoCliente = new SmtpClient();
                objetoCliente.SendMail(objetoServidor, objetoCorreo);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

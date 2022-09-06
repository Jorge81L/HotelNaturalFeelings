using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;
        private readonly Dictionary<string, string> _sqlProcedures;

        public ReservationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            _sqlProcedures = new Dictionary<string, string>();
            _sqlProcedures.Add("GetReservations", "EXECUTE sp_GET_ALL_RESERVATION");
        }

        [HttpGet]
        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            string sqlQuery = _sqlProcedures["GetReservations"];
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                HabitationController habitationController = new HabitationController(_configuration);
                UserController userController = new UserController(_configuration);
                while (reader.Read())
                {
                    reservations.Add(new Reservation
                    {
                        IdReservation = Int32.Parse(reader["ID_RESERVATION"].ToString()),
                        Habitation = habitationController.GetRoomByIdWithoutImages(new Habitation
                        {
                            IdHabitation = reader.GetInt32("ID_HABITATION"),
                            Image = new Image { ImageContent = null }
                        }),
                        User = userController.GetUser(reader.GetInt32("ID_USER")),
                        StartDate = reader.GetDateTime("START_DATE"),
                        EndDate = DateTime.Parse(reader["END_DATE"].ToString()),
                        TotalPrice = Int32.Parse(reader["TOTAL_PRICE"].ToString())
                    });
                }
                _connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }

            return reservations;
        }

    }
}

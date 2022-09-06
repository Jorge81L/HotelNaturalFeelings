using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HotelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public HotelController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        [HttpGet("aboutus")]
        public Hotel getInfoAboutUs()
        {
            Hotel aux = new Hotel();
            string sqlQuery = $"EXECUTE sp_GET_ABOUT_US_INFO";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    aux = new Hotel
                    {
                        AboutUS = reader["ABOUT_US"].ToString()
                    };
                }
                _connection.Close();
                return aux;
            }
            catch
            {
                return aux;
            }
        }

        [HttpGet("location")]
        public Hotel getInfoLocation()
        {
            Hotel aux = new Hotel();
            string sqlQuery = $"EXECUTE sp_GET_LOCATION_INFO";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    aux = new Hotel
                    {
                        Adress = reader["ADRESS"].ToString()
                    };
                }
                _connection.Close();
                return aux;
            }
            catch
            {
                return aux;
            }
        }

        [HttpGet("home")]
        public Hotel ReadHomeInformation()
        {
            Hotel home = new Hotel();
            string sqlQuery = $"EXECUTE sp_READ_HOME";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    home = new Hotel
                    {
                        IdHotel = Int32.Parse(reader["ID_HOTEL"].ToString()),      
                        Description = reader["DESCRIPTION"].ToString(),
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        }
                    };
                }

                _connection.Close();
                return home;
            }
            catch
            {
                return home;
            }
        }

        [HttpGet("facilities")]
        public List<Facilities> GetFacilities()
        {
            List<Facilities> facilitiesList = new List<Facilities>();
            Facilities facilities = new Facilities();

            try
            {
                if (ModelState.IsValid)
                {
                    string sqlQuery = $"EXECUTE sp_GET_FACILITIES";

                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        facilities = new Facilities
                        {
                            FacilityId = Int32.Parse(reader["ID_FACILITIES"].ToString()),
                            Description = reader["DESCRIPTION"].ToString(),
                            Image = new Image
                            {
                                IdImage = reader["ID_IMAGE"].ToString(),
                                ImageContent = (byte[])reader["IMAGE_CONTENT"],
                                ImageFormat = reader["IMAGE_FORMAT"].ToString()
                            }

                        };
                        facilitiesList.Add(facilities);
                    }
                    _connection.Close();
                }
                return facilitiesList;

            }
            catch
            {
                return facilitiesList;
            }

        }

        [HttpGet("todayStatus")]
        public List<TodayStatus> GetTodayStatus()
        {
            TodayStatus todayStatus = new TodayStatus();
            List<TodayStatus> todayStatusList = new List<TodayStatus>();

            try
            {
                if (ModelState.IsValid)
                {
                    string sqlQuery = $"EXECUTE sp_GET_STATUS_HOTEL";

                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        todayStatus = new TodayStatus
                        {
                            ID_Habitation = Int32.Parse(reader["ID_HABITATION"].ToString()),
                            ID_Rate = (reader["ID_RATE"].ToString()),
                            Status = reader["STATUS"].ToString()
                        };
                        
                        todayStatusList.Add(todayStatus);
                    }

                    _connection.Close();
                }
                return todayStatusList;
            }
            catch
            {
                return todayStatusList;
            }

        }

        [HttpPost("availabilityRooms")]
        public List<Reservation> GetAvailabilityRooms(Reservation input)
        {
            Reservation availabilityRoom = new Reservation();

            List<Reservation> availabilityRoomList = new List<Reservation>();

            var start = input.StartDate.ToString("yyyyMMdd");
            var end = input.EndDate.ToString("yyyyMMdd");
            try
            {
                //if (ModelState.IsValid)
                //{
                    string sqlQuery = $"EXECUTE sp_GET_AVAILABILITY_ROOM '{start}', '{end}', '{input.Habitation.Rate.Id}'";


                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    int rate;

                    while (reader.Read())
                    {
                        availabilityRoom = new Reservation
                        {
                            Habitation = new Habitation
                            {
                                Rate = new Rate()
                            }
                        };
                        availabilityRoom.Habitation.IdHabitation = Int32.Parse(reader["ID_HABITATION"].ToString());
                        rate = Int32.Parse(reader["ID_RATE"].ToString());
                        if (rate == 103)
                        {
                            availabilityRoom.Habitation.Rate.Name = "Preminum";
                        }
                        else
                        {
                            if (rate == 104)
                            {
                                availabilityRoom.Habitation.Rate.Name = "Estandar";
                            }
                            else
                            {
                                availabilityRoom.Habitation.Rate.Name = "Junior";
                            }
                        }
                        availabilityRoom.Habitation.Rate.Price = Int32.Parse(reader["PRICE"].ToString());
                        Console.WriteLine(availabilityRoom.ToString());
                        availabilityRoomList.Add(availabilityRoom);
                    }
                    _connection.Close();
                //}
                return availabilityRoomList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return availabilityRoomList;
            }
        }

        [HttpGet("hotelInfo")]
        public Hotel GetHotelInfo()
        {
            Hotel hotel = null;
            try
            {
                string sqlQuery = $"sp_GET_HOTEL_INFO";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    hotel = new Hotel
                    {
                        IdHotel = Int32.Parse(reader["ID_HOTEL"].ToString()),
                        Description = reader["DESCRIPTION"].ToString(),
                        AboutUS = reader["ABOUT_US"].ToString(),
                        Adress = reader["ADRESS"].ToString(),
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        }
                    };
                }
                _connection.Close();
                return hotel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return hotel;
            }
        }

        [HttpPut("aboutUS")]
        public bool UpdateAboutUs(Hotel hotel)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_ABOUT_US '{hotel.AboutUS}'";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
        }

        [HttpPut("home")]
        public bool UpdateHotel(Hotel hotel)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_HOTEL_HOME '{hotel.Description}', '{hotel.Image.IdImage}'";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
        }

        [HttpPut("adress")]
        public bool UpdateAdress(Hotel hotel)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_ADRESS '{hotel.Adress}'";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();

                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return response;
            }
            return false;
        }
    }
}

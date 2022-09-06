using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Natural_Feelings_Hotel_API.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HabitationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public HabitationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        [HttpGet("rate")]
        public List<Rate> GetRate()
        {
            Rate rate = new Rate();
            List<Rate> costList = new List<Rate>();

            try
            {
                if (ModelState.IsValid)
                {
                    string sqlQuery = $"EXECUTE sp_READ_RATE";

                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        rate = new Rate
                        {
                            Id = Int32.Parse(reader["ID_RATE"].ToString()),
                            Description = reader["DESCRIPTION"].ToString(),
                            Features = reader["FEATURES"].ToString(),
                            Price = Int32.Parse(reader["PRICE"].ToString()),
                            Name = reader["NAME"].ToString(),
                            Image = new Image
                            {
                                IdImage = reader["ID_IMAGE"].ToString(),
                                ImageContent = (byte[])reader["IMAGE_CONTENT"],
                                ImageFormat = reader["IMAGE_FORMAT"].ToString()
                            }
                        };
                        costList.Add(rate);
                    }
                    _connection.Close();
                }
                return costList;

            }
            catch
            {
                return costList;
            }
        }

        [HttpGet("season")]
        public List<Season> GetSeason()
        {
            Season season = new Season();
            List<Season> seasonsList = new List<Season>();

            try
            {
                if (ModelState.IsValid)
                {
                    string sqlQuery = $"sp_GET_SEASON";
                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        season = new Season
                        {

                            Id = Int32.Parse(reader["ID_SEASON"].ToString()),
                            Season_Type = reader["SEASON_TYPE"].ToString(),
                            Start_Date = DateTime.Parse(reader["START_DATE"].ToString()),
                            End_Date = DateTime.Parse(reader["END_DATE"].ToString()),
                            Price = Int32.Parse(reader["PRICE"].ToString())
                        };
                        seasonsList.Add(season);
                    }
                    _connection.Close();

                }
                return seasonsList;
            }
            catch
            {
                return seasonsList;
            }
        }

        [HttpGet("ValidateDateRange")]
        public Habitation ValidateDateRange(Reservation reservation)
        {
            Habitation result = new Habitation
            {
                IdHabitation = 0,
                Rate = new Rate
                {
                    Season = new Season()
                }
            };
            try
            {
                var StartDate = reservation.StartDate.ToString("yyyyMMdd");
                var EndDate = reservation.EndDate.ToString("yyyyMMdd");

                string sqlQuery = $"EXECUTE sp_GET_AVAILABILITY_ROOM '{StartDate}','{EndDate}'," +
                    $"'{reservation.Habitation.Rate.Id}'";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result.IdHabitation = Int32.Parse(reader["ID_HABITATION"].ToString());
                }
                _connection.Close();
                if(result.IdHabitation != 0)
                    result = this.GetRoomById(result);
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        [HttpPost("SetReservation")]
        public async Task<Reservation> SetReservation(Reservation reservation)
        {
            Reservation result = new Reservation
            {
                IdReservation = 0
            };
            try
            {
                string sqlQuery = $"EXECUTE sp_CREATE_RESERVATION '{reservation.Habitation.IdHabitation}','{reservation.User.Name}'" +
                    $", '{reservation.User.LastName}', '{reservation.User.EmailAddress}', '{reservation.StartDate}'" +
                    $", '{reservation.EndDate}', '{reservation.User.CreditCard.CardNumber}'";

                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["RESPONSE"].ToString() == "1")
                    {
                        result.IdReservation = Int32.Parse(reader["ID_RESERVATION"].ToString());
                        result.User.EmailAddress = reader["GMAIL"].ToString();
                        result.User.Name = reader["NAME"].ToString();
                    }

                }
                _connection.Close();
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
            return result;
        }

        [HttpGet("GetHabitations")]
        public List<Habitation> GetHabitations()
        {
            Habitation habitation = new Habitation();
            List<Habitation> habitationList = new List<Habitation>();

            try
            {
                string sqlQuery = $"sp_GET_ROOMS_INFORMATION";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    habitation = new Habitation
                    {
                        IdHabitation = Int32.Parse(reader["ID_HABITATION"].ToString()),
                        Location = reader["LOCATION"].ToString(),
                        //ImageRoute = reader["IMAGE_ROUTE"].ToString(),
                        IsActive = bool.Parse(reader["IS_ACTIVE"].ToString()),
                        Rate = new Rate
                        {
                            Id = Int32.Parse(reader["ID_RATE"].ToString())
                        }
                    };
                    habitationList.Add(habitation);
                }
                _connection.Close();
                return habitationList;
            }
            catch
            {
                return habitationList;
            }
        }

        [HttpPut("UpdateRoomState")]
        public bool UpdateRoomState(Habitation habitation)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_ROOM_STATE '{habitation.IdHabitation}'";
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
            catch
            {
                return response;
            }
        }

        [HttpGet("GetRateById")]
        public Rate GetRateById(Rate rate)
        {
            Rate response = new Rate
            {
                Id = 0
            };
            try
            {
                string sqlQuery = $"sp_GET_RATE_BY_ID '{rate.Id}'";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = new Rate
                    {
                        Id = Int32.Parse(reader["ID_RATE"].ToString()),
                        Description = reader["DESCRIPTION"].ToString(),
                        Features = reader["FEATURES"].ToString(),
                        Price = Int32.Parse(reader["PRICE"].ToString()),
                        Name = reader["NAME"].ToString(),
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        },
                        Season = new Season
                        {
                            Id = Int32.Parse(reader["ID_SEASON"].ToString())
                        }
                    };
                }
                _connection.Close();
                return response;
            }
            catch
            {
                return response;
            }
        }//GetRateById

        [HttpGet("GetRateByIdWithoutImage")]
        public Rate GetRateByIdWithoutImage(Rate rate)
        {
            Rate response = new Rate
            {
                Id = 0
            };
            try
            {
                string sqlQuery = $"sp_GET_RATE_BY_ID '{rate.Id}'";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = new Rate
                    {
                        Id = Int32.Parse(reader["ID_RATE"].ToString()),
                        Description = reader["DESCRIPTION"].ToString(),
                        Features = reader["FEATURES"].ToString(),
                        Price = Int32.Parse(reader["PRICE"].ToString()),
                        Name = reader["NAME"].ToString(),
                        Image = null,
                        Season = new Season
                        {
                            Id = Int32.Parse(reader["ID_SEASON"].ToString())
                        }
                    };
                }
                _connection.Close();
                return response;
            }
            catch
            {
                return response;
            }
        }//GetRateById

        [HttpGet("GetSeasonById")]
        public Season GetSeasonById(Season season)
        {
            Season response = new Season
            {
                Id = 0
            };
            try
            {
                string sqlQuery = $"sp_GET_SEASON_ID '{season.Id}'";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = new Season
                    {
                        Id = Int32.Parse(reader["ID_SEASON"].ToString()),
                        Season_Type = reader["SEASON_TYPE"].ToString(),
                        Start_Date = DateTime.Parse(reader["START_DATE"].ToString()),
                        End_Date = DateTime.Parse(reader["END_DATE"].ToString()),
                        Price = Int32.Parse(reader["PRICE"].ToString()),
                    };
                }
                _connection.Close();
                return response;
            }
            catch
            {
                return response;
            }
        }//GetRateById

        [HttpGet("GetRoomById")]
        public Habitation GetRoomById(Habitation habitation)
        {
            Habitation response = new Habitation
            {
                IdHabitation = 0
            };
            try
            {
                string sqlQuery = $"sp_GET_ROOM_BY_ID '{habitation.IdHabitation}'";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = new Habitation
                    {
                        IdHabitation = Int32.Parse(reader["ID_HABITATION"].ToString()),
                        Location = reader["LOCATION"].ToString(),
                        Rate = new Rate
                        {
                            Id = Int32.Parse(reader["ID_RATE"].ToString())
                        },
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        }
                    };
                }
                _connection.Close();
                if(response.IdHabitation != 0)
                {
                    response.Rate = this.GetRateById(response.Rate);
                    response.Rate.Season = this.GetSeasonById(response.Rate.Season);
                }
                return response;
            }
            catch
            {
                _connection.Close();
                return response;
            }
        }//GetRateById

        [HttpGet("GetRoomByIdWithoutImages")]
        public Habitation GetRoomByIdWithoutImages(Habitation habitation)
        {
            Habitation response = new Habitation
            {
                IdHabitation = 0
            };
            try
            {
                string sqlQuery = $"sp_GET_ROOM_BY_ID '{habitation.IdHabitation}'";
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response = new Habitation
                    {
                        IdHabitation = Int32.Parse(reader["ID_HABITATION"].ToString()),
                        Location = reader["LOCATION"].ToString(),
                        Rate = new Rate
                        {
                            Id = Int32.Parse(reader["ID_RATE"].ToString())
                        },
                        Image = null
                    };
                }
                _connection.Close();
                if(response.IdHabitation != 0)
                {
                    response.Rate = this.GetRateByIdWithoutImage(response.Rate);
                    response.Rate.Season = this.GetSeasonById(response.Rate.Season);
                }
                return response;
            }
            catch
            {
                _connection.Close();
                return response;
            }
        }//GetRateById

        


        [HttpPut("UpdateRate")]
        public bool UpdateRate(Rate rate)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_RATE '{rate.Id}', '{rate.Description}', '{rate.Price}', '{rate.Image.IdImage}'";
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
            catch
            {
                return response;
            }
        }

        [HttpPut("UpdateRoom")]
        public bool UpdateRoom(Habitation habitation)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"sp_UPDATE_ROOM '{habitation.IdHabitation}', " +
                    $"'{habitation.Rate.Id}', '{habitation.Location}', '{habitation.Image.IdImage}'";
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
            catch
            {
                return response;
            }
        }

    }
}

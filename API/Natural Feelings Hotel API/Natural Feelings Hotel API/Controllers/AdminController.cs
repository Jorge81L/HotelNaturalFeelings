using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        [HttpPost]
        public Admi GetAdmi(Admi temp)
        {
            Admi aux = new Admi();
            string sqlQuery = $"EXECUTE sp_GET_ADMIN @param_ADMIN_USER = '{temp.Name}', " +
                $"@param_PASS = '{temp.Password}'";
            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    aux = new Admi
                    {
                        ID_Admin = Int32.Parse(reader["ID_ADMIN"].ToString()),
                        Name = reader["NAME"].ToString(),
                        Last_Name = reader["LAST_NAME"].ToString(),
                        Gmail = reader["GMAIL"].ToString(),
                        Password = reader["PASSWORD"].ToString(),
                        Authenticated = bool.Parse(reader["AUTHENTICATED"].ToString())
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


        [HttpGet("offer")]
        public List<Offer> GetOffer()
        {
            Offer offer = new Offer();
            List<Offer> costList = new List<Offer>();

            try
            {
                if (ModelState.IsValid)
                {
                    string sqlQuery = $"EXECUTE sp_GET_OFFER";

                    using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                    command.CommandType = CommandType.Text;

                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        offer = new Offer
                        {
                            Id_Offer = Int32.Parse(reader["ID_OFFER"].ToString()),
                            Description = reader["DESCRIPTION"].ToString(),
                            DiscountPrice = Int32.Parse(reader["DISCOUNT_PRICE"].ToString()),
                            StartDate = DateTime.Parse(reader["START_DATE"].ToString()),
                            EndDate = DateTime.Parse(reader["END_DATE"].ToString()),
                            Image = new Image
                            {
                                IdImage = reader["ID_IMAGE"].ToString(),
                                ImageContent = (byte[])reader["IMAGE_CONTENT"],
                                ImageFormat = reader["IMAGE_FORMAT"].ToString()
                            }
                        };
                        costList.Add(offer);
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

        [HttpGet("offerid/{codigo}")]
        public Offer BuscarOferta(int codigo)
        {
            string sqlQuery = $"EXECUTE sp_GET_OFFER_ID'{codigo}'";
            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;
            _connection.Open();
            SqlDataReader respuestaReader = command.ExecuteReader();
            Offer oferta = new Offer();
            while (respuestaReader.Read())
            {

                oferta.Id_Offer = Int32.Parse(respuestaReader["ID_OFFER"].ToString());
                oferta.Description = respuestaReader["DESCRIPTION"].ToString();
                oferta.DiscountPrice = Int32.Parse(respuestaReader["DISCOUNT_PRICE"].ToString());
                oferta.StartDate = DateTime.Parse(respuestaReader["START_DATE"].ToString());
                oferta.EndDate = DateTime.Parse(respuestaReader["END_DATE"].ToString());
                oferta.Image.IdImage = respuestaReader["ID_IMAGE"].ToString();
                oferta.Image.ImageContent = (byte[])respuestaReader["IMAGE_CONTENT"];
                oferta.Image.ImageFormat = respuestaReader["IMAGE_FORMAT"].ToString();
            }
            _connection.Close();
            return oferta;
        }
        [HttpPost("createoffer")]
        public bool CreateOffer(Offer offer)
        {
            bool response = false;
            try
            {
                string sqlQuery = $"EXECUTE sp_CREATE_OFFER '{offer.Description}','{offer.StartDate}', '{offer.EndDate}','{offer.Image.IdImage}','{offer.DiscountPrice}' ";

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

        [HttpPut("UpdateOffer")]
        public bool UpdateOffer(Offer offer)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_UPDATE_OFFER '{offer.Id_Offer}', '{offer.Description}'" +
                $", '{offer.StartDate}','{offer.EndDate}', '{offer.Image.IdImage}','{offer.DiscountPrice}'";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = bool.Parse(reader["RESPONSE"].ToString());
                }

                return result;
            }
            catch
            {
                return result;
            }
        }
        [HttpDelete("deleteoffer")]
        public bool DeleteOffer(Offer offer)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_DELETE_OFFER '{offer.Id_Offer}'";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = bool.Parse(reader["RESPONSE"].ToString());
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        [HttpPut("updateseason")]
        public bool UpdateSeason(Season season)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_UPDATE_SEASON '{season.Id}', '{season.Season_Type}', '{season.Start_Date}', '{season.End_Date}', '{season.Price}'";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = bool.Parse(reader["RESPONSE"].ToString());
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        [HttpDelete("deleteseason")]
        public bool DeleteSeason(Season season)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_DELETE_SEASON '{season.Id}'";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = bool.Parse(reader["RESPONSE"].ToString());
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        [HttpPost("addseason")]
        public bool AddSeason(Season season)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_ADD_SEASON '{season.Season_Type}', '{season.Start_Date}', '{season.End_Date}', '{season.Price}'";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = bool.Parse(reader["RESPONSE"].ToString());
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        [HttpGet("getseasons")]
        public List<Season> GetSeason()
        {
            Season season = new Season();
            List<Season> sesonList = new List<Season>();

            try
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

                    sesonList.Add(season);

                }
                _connection.Close();

                return sesonList;
            }
            catch
            {
                return sesonList;
            }
        }


        [HttpGet("seasonid/{codigo}")]
        public Season BuscarSeason(int codigo)
        {
            string sqlQuery = $"EXECUTE sp_GET_SEASON_ID'{codigo}'";
            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;
            _connection.Open();
            SqlDataReader respuestaReader = command.ExecuteReader();
            Season season = new Season();
            while (respuestaReader.Read())
            {


                season.Id = Int32.Parse(respuestaReader["ID_SEASON"].ToString());
                season.Season_Type = respuestaReader["SEASON_TYPE"].ToString();
                season.Start_Date = DateTime.Parse(respuestaReader["START_DATE"].ToString());
                season.End_Date = DateTime.Parse(respuestaReader["END_DATE"].ToString());
                season.Price = Int32.Parse(respuestaReader["PRICE"].ToString());

            }
            _connection.Close();
            return season;
        }



    }
}

using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicityController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public PublicityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        [HttpGet("allpublicitys")]
        public List<Advertising>  GetPulbicitys()
        {
            Advertising advertising = new Advertising();
            List<Advertising> advertisingList = new List<Advertising>();

            string sqlQuery = $"EXECUTE sp_GET_PUBLICITYS";

            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    advertising = new Advertising
                    {
                        Id = Int32.Parse(reader["ID_ADVERTISING"].ToString()),
                        Link = reader["LINK"].ToString(),
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        }
                    };
                    advertisingList.Add(advertising);
                }
                _connection.Close();
                return advertisingList;
            }
            catch
            {
                return advertisingList;
            }
        }

        [HttpPost]
        public bool Create(Advertising advertising)
        {
            bool result = false;
            string sqlQuery = $"EXECUTE sp_CREATE_ADVERTISING '{advertising.Image.IdImage}', '{advertising.Link}'";

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
                _connection.Close();
                return result;
            }
            catch
            {
                return result;
            }
        }//Create


    }
}

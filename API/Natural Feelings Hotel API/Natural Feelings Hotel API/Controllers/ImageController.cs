using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {

        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        [HttpPost("Create")]
        public string Create(Image image)
        {
            string result = "";
            string sqlQuery = $"EXECUTE sp_CREATE_IMAGE @DATA, '{image.ImageFormat}'";
            using SqlCommand command = new SqlCommand(sqlQuery, _connection);
            command.Parameters.AddWithValue("@DATA", SqlDbType.VarBinary).Value = image.ImageContent;
            command.CommandType = CommandType.Text;

            try
            {
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = reader["RESPONSE"].ToString();
                }
                _connection.Close();
                return result;
            }
            catch
            {
                _connection.Close();
                return result;
            }
        }//Create
    }
}

using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Natural_Feelings_Hotel_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private SqlConnection _connection;
        private readonly Dictionary<string, string> _sqlProcedures;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            _sqlProcedures = new Dictionary<string, string>();
            _sqlProcedures.Add("GetUser", "EXECUTE sp_GET_USER_BY_ID");
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            User user = new User();
            string sqlQuery = _sqlProcedures["GetUser"] + $"'{id}'";
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User
                    {
                        Id = reader.GetInt32("ID_USER"),
                        CreditCard = null,
                        Name = reader.GetString("NAME"),
                        LastName = reader.GetString("LAST_NAME"),
                        EmailAddress = reader.GetString("GMAIL")
                    };
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }

            return user;
        }
    }
}

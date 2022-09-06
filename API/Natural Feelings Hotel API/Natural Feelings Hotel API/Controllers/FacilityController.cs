using Microsoft.AspNetCore.Mvc;
using Natural_Feelings_Hotel_API.Models;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Natural_Feelings_Hotel_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;
        private readonly Dictionary<string, string> _sqlProcedures;

        public FacilityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            _sqlProcedures = new Dictionary<string, string>();
            _sqlProcedures.Add("GetFacilities", "EXECUTE sp_GET_FACILITIES");
            _sqlProcedures.Add("GetFacility", "EXECUTE sp_GET_FACILITY_BY_ID");
            _sqlProcedures.Add("PostFacility", "EXECUTE sp_CREATE_FACILITY");
            _sqlProcedures.Add("PutFacility", "EXECUTE sp_UPDATE_FACILITY");
            _sqlProcedures.Add("DeleteFacility", "EXECUTE sp_DELETE_FACILITY");
        }

        [HttpGet]
        public List<Facilities> GetFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            string sqlQuery = _sqlProcedures["GetFacilities"];
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    facilities.Add(new Facilities
                    {
                        FacilityId = Int32.Parse(reader["ID_FACILITIES"].ToString()),
                        Description = reader["DESCRIPTION"].ToString(),
                        Image = new Image
                        {
                            IdImage = reader["ID_IMAGE"].ToString(),
                            ImageContent = (byte[])reader["IMAGE_CONTENT"],
                            ImageFormat = reader["IMAGE_FORMAT"].ToString()
                        }
                    });
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }

            return facilities;
        }

        [HttpGet("{id}")]
        public Facilities GetFacility(int id)
        {
            Facilities facility = new Facilities();
            string sqlQuery = _sqlProcedures["GetFacility"]+$"'{id}'";
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    facility = new Facilities
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
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }

            return facility;
        }

        [HttpPost]
        public bool Post([FromBody] Facilities facility)
        {
            bool response = false;
            ImageController imageController = new ImageController(_configuration);
            facility.Image.IdImage = imageController.Create(facility.Image);

            string sqlQuery = _sqlProcedures["PostFacility"] + $"'{facility.Description}', '{facility.Image.IdImage}'";
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }
            return response;
        }

        [HttpPut("{id}")]
        public bool PutFacility(int id, [FromBody] Facilities facility)
        {
            bool response = false;
            ImageController imageController = new ImageController(_configuration);
            if(facility.Image.ImageContent != null)
                facility.Image.IdImage = imageController.Create(facility.Image);

            string sqlQuery = _sqlProcedures["PutFacility"] +$"'{id}', '{facility.Description}', '{facility.Image.IdImage}'";
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }
            return response;
        }

        [HttpDelete("{id}")]
        public bool DeleteFacility(int id)
        {
            bool response = false;
            string sqlQuery = _sqlProcedures["DeleteFacility"] + $"'{id}'";
            try
            {
                using SqlCommand command = new SqlCommand(sqlQuery, _connection);
                command.CommandType = CommandType.Text;

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    response = bool.Parse(reader["RESPONSE"].ToString());
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.Close();
            }
            return response;
        }
    }
}

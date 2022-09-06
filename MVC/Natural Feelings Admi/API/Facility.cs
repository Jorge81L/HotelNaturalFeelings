using Natural_Feelings_Admi.Models;
using Newtonsoft.Json;
using System.Text;

namespace Natural_Feelings_Admi.API
{
    public class Facility : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<List<FacilityModel>> GetFacilities()
        {
            string route = Connection + "Facility";
            List<FacilityModel> response = new List<FacilityModel>();

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage apiResponse = await _httpClient.SendAsync(request);

            if (apiResponse.IsSuccessStatusCode)
            {
                var dbResult = apiResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<List<FacilityModel>>(dbResult);
            }

            return response;
        }

        public async Task<bool> CreateFacility(FacilityModel facilityModel)
        {
            string route = Connection + "Facility";
            bool response = false;
            Image image = new Image();
            facilityModel.Image.ImageContent = await image.GetBytes(facilityModel.Image);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(facilityModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage apiResponse = await _httpClient.SendAsync(request);

            if (apiResponse.IsSuccessStatusCode)
            {
                var dbResult = apiResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return response;
        }

        public async Task<bool> UpdateFacility(FacilityModel facilityModel)
        {
            string route = Connection + "Facility/" + facilityModel.FacilityId;
            bool response = false;
            if (facilityModel.Image.IdImage == "changed")
            {
                Image image = new Image();
                facilityModel.Image.ImageContent = await image.GetBytes(facilityModel.Image);
            }

            HttpContent content = new StringContent(JsonConvert.SerializeObject(facilityModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage apiResponse = await _httpClient.SendAsync(request);

            if (apiResponse.IsSuccessStatusCode)
            {
                var dbResult = apiResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return response;
        }

        public async Task<bool> DeleteFacility(FacilityModel facilityModel)
        {
            string route = Connection + "Facility/" + facilityModel.FacilityId;
            bool response = false;

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage apiResponse = await _httpClient.SendAsync(request);

            if (apiResponse.IsSuccessStatusCode)
            {
                var dbResult = apiResponse.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return response;
        }
    }
}

using Natural_Feelings_Hotel.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using Natural_Feelings_Admi.Models;

namespace Natural_Feelings_Admi.API
{
    public class Advertising : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public Advertising() { }

        public async Task<bool> create(AdvertisingModel advertisingModel)
        {
            string route = Connection + "Publicity";
            bool result = false;
            Image image = new Image();
            advertisingModel.Image.IdImage = image.Create(advertisingModel.Image);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(advertisingModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };
            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<bool>(dbResult);
                }

                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}

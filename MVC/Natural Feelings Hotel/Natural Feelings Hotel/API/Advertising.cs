using Natural_Feelings_Hotel.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.API
{
    public class Advertising : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public Advertising() { }

        public async Task<bool> create(AdvertisingModel advertisingModel)
        {
            string route = Connection + "Publicity";
            bool result = false;

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

        public async Task<List<AdvertisingModel>> GetAds()
        {
            List<AdvertisingModel> advertisingModels = new List<AdvertisingModel>();
            string route = Connection + "Publicity/allpublicitys";

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };
            try
            {

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    foreach (AdvertisingModel item in JsonConvert.DeserializeObject<List<AdvertisingModel>>(dbResult))
                    {
                        using (FileStream fileStream = new FileStream("wwwroot/AdvertisingImages/" + item.Image.IdImage + item.Image.ImageFormat, FileMode.Create))
                        {
                            for (int i = 0; i < item.Image.ImageContent.Length; i++)
                            {
                                fileStream.WriteByte(item.Image.ImageContent[i]);
                            }
                            fileStream.Seek(0, SeekOrigin.Begin);
                        }
                        advertisingModels.Add(item);
                    }
                }

                return advertisingModels;
            }
            catch
            {
                return advertisingModels;
            }
        }
    }
}

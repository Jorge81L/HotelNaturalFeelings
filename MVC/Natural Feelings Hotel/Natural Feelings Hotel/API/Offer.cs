using System.Text;
using Natural_Feelings_Hotel.Models;
using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.API

{
    public class Offer : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<OfferModel> GetOffer(int id)
        {
            OfferModel result = new OfferModel();

            string route = Connection + "Admin/offerid/" + id;

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<OfferModel>(dbResult);

            }

            return result;
        }

        public async Task<List<OfferModel>> GetOffer()
        {
            string route = Connection + "Admin/offer";

            List<OfferModel> result = new List<OfferModel>();

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<List<OfferModel>>(dbResult);

            }

            return result;
        }
    }
}

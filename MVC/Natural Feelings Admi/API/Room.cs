using Natural_Feelings_Admi.Models;
using Newtonsoft.Json;
using System.Text;

namespace Natural_Feelings_Admi.API
{
    public class Room : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<List<RateModel>> GetRates()
        {
            string route = Connection + "Habitation/rate";
            List<RateModel> result = new List<RateModel>();

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
                result = JsonConvert.DeserializeObject<List<RateModel>>(dbResult);
            }

            return result;
        }

        public async Task<RateModel> GetRateById(RateModel rate)
        {
            string route = Connection + "Habitation/GetRateById";
            RateModel result = new RateModel();

            HttpContent content = new StringContent(JsonConvert.SerializeObject(rate), Encoding.UTF8, "application/json");
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
                result = JsonConvert.DeserializeObject<RateModel>(dbResult);
            }

            using (FileStream fileStream = new FileStream("wwwroot/img/" + result.Image.IdImage + result.Image.ImageFormat, FileMode.Create))
            {
                for (int i = 0; i < result.Image.ImageContent.Length; i++)
                {
                    fileStream.WriteByte(result.Image.ImageContent[i]);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
            }
            return result;
        }

        public async Task<RoomModel> GetRoomById(RoomModel room)
        {
            string route = Connection + "Habitation/GetRoomById";
            RoomModel result = new RoomModel();

            HttpContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
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
                result = JsonConvert.DeserializeObject<RoomModel>(dbResult);
            }
            using (FileStream fileStream = new FileStream("wwwroot/img/" + result.Image.IdImage + result.Image.ImageFormat, FileMode.Create))
            {
                for (int i = 0; i < result.Image.ImageContent.Length; i++)
                {
                    fileStream.WriteByte(result.Image.ImageContent[i]);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
            }
            return result;

        }

        public async Task<List<RoomModel>> GetRooms()
        {
            string route = Connection + "Habitation/GetHabitations";
            List<RoomModel> result = new List<RoomModel>();

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
                result = JsonConvert.DeserializeObject<List<RoomModel>>(dbResult);
            }

            return result;
        }

        public async Task<bool> UpdateRoomState(RoomModel room)
        {
            string route = Connection + "Habitation/UpdateRoomState";
            bool result = false;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return result;
        }

        public async Task<bool> UpdateRate(RateModel rate)
        {
            string route = Connection + "Habitation/UpdateRate";
            bool result = false;

            if (rate.Image.IdImage == "changed")
                rate.Image.IdImage = this.CreateImage(rate.Image);
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(rate), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return result;
        }

        public async Task<bool> UpdateRoom(RoomModel room)
        {
            string route = Connection + "Habitation/UpdateRoom";
            bool result = false;

            if (room.Image.IdImage == "changed")
                room.Image.IdImage = this.CreateImage(room.Image);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return result;
        }

        public string CreateImage(ImageModel image)
        {
            Image imageAPI = new Image();
            return imageAPI.Create(image);
        }

        public async Task<List<ReservationModel>> GetReservations()
        {
            string route = Connection + "Reservation";
            List<ReservationModel> response = new List<ReservationModel>();

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
                response = JsonConvert.DeserializeObject<List<ReservationModel>>(dbResult);
            }
            return response;
        }

    }
}

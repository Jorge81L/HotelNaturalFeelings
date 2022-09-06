using Natural_Feelings_Hotel.Models;
using System.Text;
using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.API
{
    public class Habitation : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<List<RateModel>> ReadRate()
        {
            RateModel rateModel = new RateModel();
            List<RateModel> rateModelList = new List<RateModel>();

            string route = Connection + "Habitation/rate";

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                rateModel.Description = "Error";

            }
            else
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                foreach (RateModel item in JsonConvert.DeserializeObject<List<RateModel>>(dbResult))
                {
                    using (FileStream fileStream = new FileStream("wwwroot/galery/" + item.Image.IdImage + item.Image.ImageFormat, FileMode.Create))
                    {
                        for (int i = 0; i < item.Image.ImageContent.Length; i++)
                        {
                            fileStream.WriteByte(item.Image.ImageContent[i]);
                        }
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }
                    rateModelList.Add(item);
                }
            }
            return rateModelList;
        }

        public async Task<HabitationModel> ValidateDateRange(ReservationModel reservationModel)
        {
            HabitationModel result = new HabitationModel();
            
            string route = Connection + "Habitation/ValidateDateRange";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(reservationModel), Encoding.UTF8, "application/json");
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode){
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<HabitationModel>(dbResult);
            }

            return result;
        }
        
        public async Task<ReservationModel> SetReservation(ReservationModel reservationModel)
        {
            ReservationModel result = new ReservationModel
            {
                IdReservation = 0

            };

            string route = Connection + "Habitation/SetReservation";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(reservationModel), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<ReservationModel>(dbResult);
            }

            return result;

        }

        public async Task<HabitationModel> GetHabitationById(HabitationModel habitationModel)
        {
            HabitationModel result = new HabitationModel
            {
                IdHabitation = 0

            };

            string route = Connection + "Habitation/GetRoomById";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(habitationModel), Encoding.UTF8, "application/json");

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
                result = JsonConvert.DeserializeObject<HabitationModel>(dbResult);
                using (FileStream fileStream = new FileStream("wwwroot/galery/" + result.Image.IdImage + result.Image.ImageFormat, FileMode.Create))
                {
                    for (int i = 0; i < result.Image.ImageContent.Length; i++)
                    {
                        fileStream.WriteByte(result.Image.ImageContent[i]);
                    }
                    fileStream.Seek(0, SeekOrigin.Begin);
                }
            }


            return result;
        }

    }
}




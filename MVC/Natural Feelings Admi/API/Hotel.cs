using Natural_Feelings_Admi.Models;
using System.Text;
using Newtonsoft.Json;
using Natural_Feelings_Hotel_API.Models;

namespace Natural_Feelings_Admi.API
{
    public class Hotel : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<bool> create(HotelModel aboutUsModel)
        {
            string route = Connection + "Hotel/aboutus";
            bool result = true;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(aboutUsModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                result = false;
            }
            else
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return result;
        }

        public async Task<bool> createHotel(HotelModel homeModel)
        {
            string route = Connection + "Hotel/home";
            bool result = true;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(homeModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                result = false;
            }
            else
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<bool>(dbResult);
            }

            return result;
        }

        public async Task<HotelModel> ReadHome()
        {
            HotelModel homeModel = new HotelModel();
            string route = Connection + "Hotel/home";

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
                    homeModel.Description = Connection;
                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    homeModel = JsonConvert.DeserializeObject<HotelModel>(dbResult);
                }

                return homeModel;

            }
            catch
            {
                homeModel.Description = "El contenido no está disponible :(";
                return homeModel;
            }
        }
        public async Task<HotelModel> getInfoLocation()
        {
            HotelModel locationModel = new HotelModel();
            string route = Connection + "Hotel/location";

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
                    locationModel.Description = "Error";
                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    locationModel = JsonConvert.DeserializeObject<HotelModel>(dbResult);
                }

                return locationModel;
            }
            catch
            {
                locationModel.Adress = "El contenido no está disponible :(";
                return locationModel;
            }
        }

        public async Task<List<TodayStatusModel>> GetTodayStatus()
        {
            List<TodayStatusModel> todayStatusList = new List<TodayStatusModel>();

            string route = Connection + "Hotel/todayStatus";

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
                todayStatusList = JsonConvert.DeserializeObject<List<TodayStatusModel>>(dbResult);
            }

            return todayStatusList;
        }

        public async Task<List<ReservationModel>> GetAvailabilityRoom(ReservationModel imput)
        {
            List<ReservationModel> availabilityRoomList = new List<ReservationModel>();
            string route = Connection + "Hotel/availabilityRooms";
            //imput.IdHabitation = 0;
            //imput.Type = "";
            //imput.Cost = "";
            HttpContent content = new StringContent(JsonConvert.SerializeObject(imput), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,

                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            Console.WriteLine("response" + response);
            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                availabilityRoomList = JsonConvert.DeserializeObject<List<ReservationModel>>(dbResult);
                Console.WriteLine(dbResult);
            }

            return availabilityRoomList;
        }
        
        public async Task<List<TodayStatusModel>> reporte()
        {
            string route = Connection + "Hotel/todayStatus";

            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            List<TodayStatusModel> todayStatusList = new List<TodayStatusModel>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
		            RequestUri = new Uri(route),
                Content = content
            };
		        HttpResponseMessage response = await _httpClient.SendAsync(request);
		        if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsStringAsync().Result;
                todayStatusList = JsonConvert.DeserializeObject<List<TodayStatusModel>>(respuesta);
            }
            Console.WriteLine(todayStatusList.Count);
            return todayStatusList;

        }

        public async Task<HotelModel> GetHotel()
        {
            HotelModel hotel = new HotelModel();
            string route = Connection + "Hotel/hotelInfo";
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            Console.WriteLine("response" + response);
            if (response.IsSuccessStatusCode)
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                hotel = JsonConvert.DeserializeObject<HotelModel>(dbResult);
                using (FileStream fileStream = new FileStream("wwwroot/img/HomeImg/" + hotel.Image.IdImage + hotel.Image.ImageFormat, FileMode.Create))
                {
                    for (int i = 0; i < hotel.Image.ImageContent.Length; i++)
                    {
                        fileStream.WriteByte(hotel.Image.ImageContent[i]);
                    }
                    fileStream.Seek(0, SeekOrigin.Begin);
                }
            }

            return hotel;
        }

        public async Task<bool> UpdateAboutUs(HotelModel hotelModel)
        {
            string route = Connection + "Hotel/aboutUS";
            bool result = false;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(hotelModel), Encoding.UTF8, "application/json");

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

        public async Task<bool> UpdateHome(HotelModel hotelModel)
        {
            string route = Connection + "Hotel/home";
            bool result = false;

            Image image = new Image();

            if (hotelModel.Image.ImageForm is null)
            {
                HotelModel hotel = GetHotel().GetAwaiter().GetResult();
                hotelModel.Image.IdImage = hotel.Image.IdImage;
            }
            else
            {
                hotelModel.Image.IdImage = image.Create(hotelModel.Image);
            }
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(hotelModel), Encoding.UTF8, "application/json");

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

        public async Task<bool> UpdateAdress(HotelModel hotelModel)
        {
            string route = Connection + "Hotel/adress";
            bool result = false;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(hotelModel), Encoding.UTF8, "application/json");

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
    }
}


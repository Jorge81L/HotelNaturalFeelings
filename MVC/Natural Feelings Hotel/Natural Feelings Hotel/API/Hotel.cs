using Natural_Feelings_Hotel.Models;
using System.Text;
using Newtonsoft.Json;
namespace Natural_Feelings_Hotel.API
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

        public async Task<HotelModel> getInfoAboutUs()
        {
            HotelModel aboutUsModel = new HotelModel();
            string route = Connection + "Hotel/aboutus";

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
                    aboutUsModel.Description = "Error";
                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    aboutUsModel = JsonConvert.DeserializeObject<HotelModel>(dbResult);
                }

                return aboutUsModel;
            }
            catch
            {
                aboutUsModel.Description = "El contenido no está disponible :(";
                return aboutUsModel;
            }
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
                    using (FileStream fileStream = new FileStream("wwwroot/galery/" + homeModel.Image.IdImage + homeModel.Image.ImageFormat, FileMode.Create))
                    {

                        fileStream.Write(homeModel.Image.ImageContent);
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }
                   
                }

                return homeModel;

            }
            catch
            {
                homeModel.Description = "El contenido no está disponible :(";
                return homeModel;
            }
        }

        public async Task<List<FacilitiesModel>> ReadFacilities()
        {
            FacilitiesModel facilitiesModel = new FacilitiesModel();
            List<FacilitiesModel> facilitiesList = new List<FacilitiesModel>();

            string route = Connection + "Hotel/facilities";

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
                    facilitiesModel.Description = "El contenido no está disponible :(";

                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    List<FacilitiesModel> facilitiesImages = JsonConvert.DeserializeObject<List<FacilitiesModel>>(dbResult);
                    foreach (var item in facilitiesImages)
                    {
                        using (FileStream fileStream = new FileStream("wwwroot/galery/" + item.Image.IdImage + item.Image.ImageFormat, FileMode.Create))
                        {
                            for (int i = 0; i < item.Image.ImageContent.Length; i++)
                            {
                                fileStream.WriteByte(item.Image.ImageContent[i]);
                            }
                            fileStream.Seek(0, SeekOrigin.Begin);
                        }
                        facilitiesList.Add(item);
                    }
                }

                return facilitiesList;
            }
            catch
            {
                facilitiesModel.Description = "El contenido no está disponible :(";
                facilitiesList.Add(facilitiesModel);

                return facilitiesList;
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
    }
}


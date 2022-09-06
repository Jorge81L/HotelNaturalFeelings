using Natural_Feelings_Admi.Models;
using Newtonsoft.Json;
using System.Text;

namespace Natural_Feelings_Admi.API
{
    public class Admin : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<AdmiModel> getAdmi(AdmiModel temp)
        {
            string route = Connection + "Admin";
            AdmiModel result = new AdmiModel();
            temp.Gmail = "";
            temp.Last_Name = "";
            HttpContent content = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                result = null;
            }
            else
            {
                var dbResult = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(dbResult);
                result = JsonConvert.DeserializeObject<AdmiModel>(dbResult);
            }
            Console.WriteLine(result);
            return result;
        }

        public async Task<List<SeasonModel>> GetSeason()
        {
            string route = Connection + "Admin/getseasons";
            List<SeasonModel> result = new List<SeasonModel>();

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
                result = JsonConvert.DeserializeObject<List<SeasonModel>>(dbResult);
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

        public async Task<OfferModel> BuscarOferta(int ofertaid)
        {

            OfferModel offerModel = new OfferModel();
            string route = Connection + "Admin/offerid/" + ofertaid;

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
                    offerModel.Description = Connection;
                }
                else
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    offerModel = JsonConvert.DeserializeObject<OfferModel>(dbResult);
                    using (FileStream fileStream = new FileStream("wwwroot/img/" + offerModel.Image.IdImage + offerModel.Image.ImageFormat, FileMode.Create))
                    {

                        fileStream.Write(offerModel.Image.ImageContent);
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }

                }

                return offerModel;

            }
            catch
            {
                offerModel.Description = "El contenido no está disponible :(";
                return offerModel;
            }
        }

        public async Task<SeasonModel> BuscarSeason (int seasonid)
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            string route = Connection + "Admin/seasonid/" + seasonid;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(route),
                Content = content
            };
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            SeasonModel seasons = new SeasonModel();

            if (response.IsSuccessStatusCode)
            {
                var season = response.Content.ReadAsStringAsync().Result;
                seasons = JsonConvert.DeserializeObject<SeasonModel>(season);
            }
            return seasons;
        }


        public async Task<string> DeteleSeason(SeasonModel seasonModel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(seasonModel), Encoding.UTF8, "application/json");

            string route = Connection + "Admin/deleteseason";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(route),
                Content = content
            };
           
            HttpResponseMessage response = await _httpClient.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                var respuestaAPI = response.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<string>(respuestaAPI);
                return respuesta;
            }
            return response.StatusCode + "";

        }

        public async Task<string> DeteleOffer(OfferModel offerModel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(offerModel), Encoding.UTF8, "application/json");
            
            string route = Connection + "Admin/deleteoffer";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            Console.WriteLine();

            if (response.IsSuccessStatusCode)
            {
                var respuestaAPI = response.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<string>(respuestaAPI);
                return respuesta;
            }
            return response.StatusCode + "";

        }

        public async Task<string> CreateSeason(SeasonModel seasonModel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(seasonModel), Encoding.UTF8, "application/json");

            string route = Connection + "Admin/addseason";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            Console.WriteLine();

            if (response.IsSuccessStatusCode)
            {
                var respuestaAPI = response.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<string>(respuestaAPI);
                return respuesta;
            }
            return response.StatusCode + "";

        }

        public async Task<string> CreateOffer(OfferModel offerModel)
        {
            Image image = new Image();
            offerModel.Image.IdImage = image.Create(offerModel.Image);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(offerModel), Encoding.UTF8, "application/json");

            string route = Connection + "Admin/createoffer";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            Console.WriteLine();

            if (response.IsSuccessStatusCode)
            {
                var respuestaAPI = response.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<string>(respuestaAPI);
                return respuesta;
            }
            return response.StatusCode + "";

        }

        public async Task<bool> EditOffer(OfferModel offerModel)
        {
            string route = Connection + "Admin/UpdateOffer";
            bool result = false;

            if (offerModel.Image.IdImage == "changed")
                offerModel.Image.IdImage = this.CreateImage(offerModel.Image);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(offerModel), Encoding.UTF8, "application/json");
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

        public async Task<string> EditSeason(SeasonModel seasonModel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(seasonModel), Encoding.UTF8, "application/json");

            string route = Connection + "Admin/updateseason";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(route),
                Content = content
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            Console.WriteLine();

            if (response.IsSuccessStatusCode)
            {
                var respuestaAPI = response.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<string>(respuestaAPI);
                return respuesta;
            }
            return response.StatusCode + "";

        }

    }
}

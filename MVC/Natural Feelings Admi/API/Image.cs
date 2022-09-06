using Natural_Feelings_Admi.Models;
using Newtonsoft.Json;
using System.Text;

namespace Natural_Feelings_Admi.API
{
    public class Image : ApiConnection
    {
        static HttpClient _httpClient = new HttpClient();

        public Image() { }

        public string Create(ImageModel imageModel)
        {
            string route = Connection + "Image/Create";
            string result = "";
            imageModel.ImageContent = this.GetBytes(imageModel).GetAwaiter().GetResult();
            imageModel.ImageFormat = Path.GetExtension(imageModel.ImageForm.FileName);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(imageModel), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(route),
                Content = content
            };
            try
            {
                HttpResponseMessage response = _httpClient.Send(request);

                if (response.IsSuccessStatusCode)
                {
                    var dbResult = response.Content.ReadAsStringAsync().Result;
                    result = dbResult;
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        public async Task<byte[]> GetBytes(ImageModel imageModel)
        {
            using (var dataStream = new MemoryStream())
            {
                await imageModel.ImageForm.CopyToAsync(dataStream);
                return dataStream.ToArray();
            }
        }
    }
}

using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class AdvertisingModel
    {
        public int Id { get; set; } = 0;
        public string Link { get; set; } = string.Empty;
        public string AdImage { get; set; } = string.Empty;
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

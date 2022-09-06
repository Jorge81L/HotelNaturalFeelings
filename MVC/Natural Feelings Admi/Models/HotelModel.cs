using Newtonsoft.Json;
namespace Natural_Feelings_Admi.Models
{
    public class HotelModel
    {
        public int IdHotel { get; set; }
        public string Description { get; set; }
        public string AboutUS { get; set; }
        public string Adress { get; set; }
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

using Newtonsoft.Json;
namespace Natural_Feelings_Hotel.Models
{
    public class HotelModel
    {
        public int IdHotel { get; set; }
        public ImageModel Image { get; set; } = new ImageModel();
        public string Description { get; set; }
        public string AboutUS { get; set; }
        public string Adress { get; set; }
    }
}

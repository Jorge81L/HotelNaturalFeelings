using Newtonsoft.Json;

namespace Natural_Feelings_Hotel_API.Models
{
    public class Habitation
    {
        public int IdHabitation { get; set; } = 0;
        //public string? ImageRoute { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; } = false;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public Reservation? Reservation { get; set; } =new Reservation();
        public Rate? Rate { get; set; } = new Rate();
        public Image Image { get; set; } = new Image();
    }
}

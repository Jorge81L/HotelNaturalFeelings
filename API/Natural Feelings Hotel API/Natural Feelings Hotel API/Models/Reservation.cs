using Newtonsoft.Json;

namespace Natural_Feelings_Hotel_API.Models
{
    public class Reservation
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? IdReservation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalPrice { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; } = new User();
        public Habitation Habitation { get; set; } = new Habitation();
    }
}

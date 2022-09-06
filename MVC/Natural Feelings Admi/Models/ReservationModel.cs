using Newtonsoft.Json;

namespace Natural_Feelings_Admi.Models
{
    public class ReservationModel
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
        public UserModel User { get; set; } = new UserModel();

        public RoomModel Habitation { get; set; }
    }
}

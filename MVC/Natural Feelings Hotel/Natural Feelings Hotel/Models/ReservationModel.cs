using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class ReservationModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int IdReservation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalPrice { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public HabitationModel Habitation { get; set; } = new HabitationModel();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserModel User { get; set; }



        virtual public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}

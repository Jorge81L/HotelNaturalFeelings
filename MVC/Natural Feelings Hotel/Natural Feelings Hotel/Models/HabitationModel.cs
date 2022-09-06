using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class HabitationModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? IdHabitation { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public string? ImageRoute { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Location { get; set; }
        /*
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ReservationModel? Reservation { get; set; }
        */
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RateModel? Rate { get; set; }

        public ImageModel? Image { get; set; } = new ImageModel { IdImage = "", ImageContent = null };

        virtual public string ToString()
		{
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}
    }
}

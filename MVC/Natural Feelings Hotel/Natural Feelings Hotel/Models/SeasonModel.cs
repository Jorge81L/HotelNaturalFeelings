using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class SeasonModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Season_Type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Start_Date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string End_Date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Price { get; set; }
    }
}

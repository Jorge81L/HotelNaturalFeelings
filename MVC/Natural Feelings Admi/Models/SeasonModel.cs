using Newtonsoft.Json;

namespace Natural_Feelings_Admi.Models
{
    public class SeasonModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Season_Type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Start_Date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime End_Date { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }
    }
}

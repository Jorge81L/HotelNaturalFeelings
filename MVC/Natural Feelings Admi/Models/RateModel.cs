using Newtonsoft.Json;

namespace Natural_Feelings_Admi.Models
{
    public class RateModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Features { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ImageModel? Image { get; set; } = new ImageModel();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SeasonModel? Season { get; set; }
    }
}

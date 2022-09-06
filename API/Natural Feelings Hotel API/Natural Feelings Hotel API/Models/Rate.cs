using Newtonsoft.Json;

namespace Natural_Feelings_Hotel_API.Models
{
    public class Rate
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
        public string? Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Season? Season { get; set; }
        public Image? Image { get; set; } = new Image();
    }
}

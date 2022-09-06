using Newtonsoft.Json;

namespace Natural_Feelings_Admi.Models
{
    public class CreditCardModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CardNumber { get; set; } = string.Empty;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CV { get; set; } = string.Empty;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Date { get; set; }
    }
}

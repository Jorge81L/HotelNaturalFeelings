using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class CreditCardModel
    {
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string CardNumber { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? CV { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Date { get; set; }
	}
}

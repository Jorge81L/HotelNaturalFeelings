using Newtonsoft.Json;

namespace Natural_Feelings_Hotel.Models
{
    public class UserModel
    {
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? Id { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? Name { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string LastName { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string EmailAddress { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public CreditCardModel CreditCard { get; set; }	
	}
}

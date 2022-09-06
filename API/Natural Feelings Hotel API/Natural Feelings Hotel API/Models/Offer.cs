using Newtonsoft.Json;

namespace Natural_Feelings_Hotel_API.Models
{
    public class Offer
    {
        public int Id_Offer { get; set; }
        public string Description { get; set; }
        public int DiscountPrice { get; set; }
        public DateTime? StartDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? EndDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Image Image { get; set; } = new Image();
    }
}

using Newtonsoft.Json;
namespace Natural_Feelings_Hotel.Models
{
    public class FacilitiesModel
    {
        public int FacilityId { get; set; }
        public string Description { get; set; }
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

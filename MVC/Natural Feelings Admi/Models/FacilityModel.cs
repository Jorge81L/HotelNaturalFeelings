namespace Natural_Feelings_Admi.Models
{
    public class FacilityModel
    {
        public int FacilityId { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

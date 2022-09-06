namespace Natural_Feelings_Hotel_API.Models
{
    public class Facilities
    {
        public int FacilityId { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; } = new Image();
    }
}

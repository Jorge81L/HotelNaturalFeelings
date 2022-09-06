namespace Natural_Feelings_Hotel_API.Models
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string Description { get; set; } = string.Empty;
        public string AboutUS { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public Image Image { get; set; } = new Image();
    }
}

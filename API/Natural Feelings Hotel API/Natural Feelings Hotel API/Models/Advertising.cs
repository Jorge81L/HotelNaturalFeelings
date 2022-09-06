namespace Natural_Feelings_Hotel_API.Models
{
    public class Advertising
    {
        public int Id { get; set; } = 0;
        public string Link { get; set; } = string.Empty;
        public Image Image { get; set; } = new Image();
    }
}

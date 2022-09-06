namespace Natural_Feelings_Admi.Models
{
    public class RoomModel
    {
        public int IdHabitation { get; set; } = 0;
        //public string? ImageRoute { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; } = false;
        //public Reservation? Reservation { get; set; } =new Reservation();
        public RateModel? Rate { get; set; } = new RateModel();
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

namespace Natural_Feelings_Admi.Models
{
    public class OfferModel
    {
        public int Id_Offer { get; set; }
        public string Description { get; set; }
        public int DiscountPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ImageModel Image { get; set; } = new ImageModel();
    }
}

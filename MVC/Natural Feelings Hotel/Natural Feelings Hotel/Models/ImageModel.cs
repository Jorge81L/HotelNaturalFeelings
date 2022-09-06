namespace Natural_Feelings_Hotel.Models
{
    public class ImageModel
    {
        public string? IdImage { get; set; } = string.Empty;
        public byte[]? ImageContent { get; set; } = null;
        public string? ImageFormat { get; set; } = string.Empty;
        public IFormFile? ImageForm { get; set; }
    }
}
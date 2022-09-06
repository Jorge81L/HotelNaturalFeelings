namespace Natural_Feelings_Hotel_API.Models
{
    public class Admi
    {
        public int ID_Admin { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }
        public bool Authenticated { get; set; }
    }
}

using Newtonsoft.Json;
namespace Natural_Feelings_Admi.Models
{
    public class AdmiModel
    {
        public int ID_Admin { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }
        public bool Authenticated { get; set; }
    }
}

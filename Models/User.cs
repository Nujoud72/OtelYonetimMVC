namespace OtelYonetimMVC.Models
{
    public class User
    {
        public int Id { get; set; }              // PK
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";   // "Yonetici" / "Resepsiyon"
    }
}

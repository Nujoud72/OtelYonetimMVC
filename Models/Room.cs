using System.ComponentModel.DataAnnotations;

namespace OtelYonetimMVC.Models
{
    public class Room
    {
        public int Id { get; set; }          // IDENTITY
        public int RoomNumber { get; set; }  // normal int, unique
        public string Status { get; set; } = "Boş";
    }


}


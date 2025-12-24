using Microsoft.AspNetCore.Mvc.Rendering;

namespace OtelYonetimMVC.Models
{
    public class CreateReservationVM
    {
        public Reservation Reservation { get; set; } = new Reservation();
        public List<SelectListItem> AvailableRooms { get; set; } = new();
    }
}

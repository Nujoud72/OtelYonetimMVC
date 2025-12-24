using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OtelYonetimMVC.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtelYonetimMVC.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        // 🔑 FOREIGN KEY
        [Required]
        public int RoomId { get; set; }

        // 🔗 NAVIGATION PROPERTY
        public Room? Room { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.CheckIn;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public DateTime? PaidAt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }



    }
}

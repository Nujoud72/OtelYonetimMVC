using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtelYonetimMVC.Data;
using OtelYonetimMVC.Models;

namespace OtelYonetimMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelDbContext _context;

        public HomeController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // === DASHBOARD KARTLARI ===
            var totalRooms = await _context.Rooms.CountAsync();

            var occupiedRooms = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.CheckIn)
                .Select(r => r.RoomId)
                .Distinct()
                .CountAsync();

            var cleaningRooms = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.CheckOut)
                .Select(r => r.RoomId)
                .Distinct()
                .CountAsync();

            var availableRooms = totalRooms - occupiedRooms - cleaningRooms;
            if (availableRooms < 0)
                availableRooms = 0;

            var vm = new DashboardVM
            {
                TotalRooms = totalRooms,
                OccupiedRooms = occupiedRooms,
                CleaningRooms = cleaningRooms,
                AvailableRooms = availableRooms
            };

            // === SON SÝSTEM HAREKETLERÝ (HESAPLANMIÞ ÖZET) ===

            var todayActive = await _context.Reservations
                .Where(r =>
                    r.Status == ReservationStatus.CheckIn &&
                    r.CheckInDate.Date == DateTime.Today)
                .CountAsync();

            var cleaningCount = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.CheckOut)
                .Select(r => r.RoomId)
                .Distinct()
                .CountAsync();

            var lastReservation = await _context.Reservations
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            vm.SystemActivities = new List<string>
            {
                todayActive > 0
                    ? $"Bugün {todayActive} aktif rezervasyon bulunmaktadýr."
                    : "Bugün aktif rezervasyon bulunmamaktadýr.",

                cleaningCount > 0
                    ? $"{cleaningCount} oda þu anda temizliktedir."
                    : "Temizlikte oda bulunmamaktadýr.",

                lastReservation != null
                    ? $"Son rezervasyon: {lastReservation.CustomerName} (#{lastReservation.Id})"
                    : "Henüz rezervasyon kaydý yok."
            };

            return View(vm);
        }
    }
}

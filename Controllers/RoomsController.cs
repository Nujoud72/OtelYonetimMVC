using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtelYonetimMVC.Data;
using OtelYonetimMVC.Models;

namespace OtelYonetimMVC.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // ✅ LIST (Durum rezervasyonlardan hesaplanır)
        public async Task<IActionResult> Index(string search)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            // 1) Odaları çek
            var roomsQuery = _context.Rooms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                // Oda numarası filtreleme (durumu sonra filtreleyeceğiz)
                roomsQuery = roomsQuery.Where(r => r.RoomNumber.ToString().Contains(search));
            }

            var rooms = await roomsQuery
                .OrderBy(r => r.RoomNumber)
                .Select(r => r.RoomNumber)
                .ToListAsync();

            // 2) Dolu odalar (CheckIn)
            var occupiedRoomNumbers = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.CheckIn)
                .Select(r => r.Room.RoomNumber)   // Room navigation var
                .Distinct()
                .ToListAsync();

            // 3) Temizlikte odalar (CheckOut)
            var cleaningRoomNumbers = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.CheckOut)
                .Select(r => r.Room.RoomNumber)
                .Distinct()
                .ToListAsync();

            // 4) VM oluştur (durumu hesapla)
            var list = new List<RoomStatusVM>();
            foreach (var rn in rooms)
            {
                string status = "Boş";

                if (occupiedRoomNumbers.Contains(rn))
                    status = "Dolu";
                else if (cleaningRoomNumbers.Contains(rn))
                    status = "Temizlikte";

                list.Add(new RoomStatusVM
                {
                    RoomNumber = rn,
                    Status = status
                });
            }

            // 5) Eğer kullanıcı "dolu/boş/temizlikte" yazdıysa durumdan da filtreleyelim
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                if (s == "dolu" || s == "boş" || s == "bos" || s == "temizlikte")
                {
                    string normalized = s == "bos" ? "boş" : s;

                    list = list
                        .Where(x => x.Status.ToLower() == normalized)
                        .ToList();
                }
            }

            return View(list);
        }

        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            if (room.RoomNumber <= 0)
            {
                ViewBag.Error = "Oda numarası geçerli olmalıdır.";
                return View(room);
            }

            bool exists = _context.Rooms.Any(r => r.RoomNumber == room.RoomNumber);
            if (exists)
            {
                ViewBag.Error = "Bu oda numarası zaten mevcut.";
                return View(room);
            }

            // DB'de status kalsın ama Index'te hesaplanan gösterilecek
            if (string.IsNullOrWhiteSpace(room.Status))
                room.Status = "Boş";

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int roomNumber)
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            var existing = _context.Rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);
            if (existing == null) return NotFound();

            existing.Status = room.Status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int roomNumber)
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int roomNumber)
        {
            if (!IsAdmin())
                return RedirectToAction("Index");

            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool IsAdmin()
            => HttpContext.Session.GetString("Role") == "Yonetici";
    }
}

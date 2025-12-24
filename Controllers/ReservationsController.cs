using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtelYonetimMVC.Data;
using OtelYonetimMVC.Models;

namespace OtelYonetimMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HotelDbContext _context;

        public ReservationsController(HotelDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Where(r => r.Status != ReservationStatus.Completed)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();

            return View(reservations);
        }

        // CREATE GET
        public async Task<IActionResult> Create()
        {
            await LoadRoomsAsync();
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            // 🔴 TARİH KONTROLÜ
            if (reservation.CheckOutDate <= reservation.CheckInDate)
            {
                ModelState.AddModelError("", "Çıkış tarihi, giriş tarihinden sonra olmalıdır.");
            }

            if (!ModelState.IsValid)
            {
                await LoadRoomsAsync(reservation.RoomId);
                return View(reservation);
            }

            reservation.Status = ReservationStatus.CheckIn;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            await LoadRoomsAsync(reservation.RoomId);
            return View(reservation);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            // 🔴 TARİH KONTROLÜ
            if (reservation.CheckOutDate <= reservation.CheckInDate)
            {
                ModelState.AddModelError("", "Çıkış tarihi, giriş tarihinden sonra olmalıdır.");
            }

            if (!ModelState.IsValid)
            {
                await LoadRoomsAsync(reservation.RoomId);
                return View(reservation);
            }

            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return View(reservation);
        }

        // DELETE POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // CHECK-OUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            reservation.Status = ReservationStatus.CheckOut;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // COMPLETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            reservation.Status = ReservationStatus.Completed;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // helper
        private async Task LoadRoomsAsync(int? selectedRoomId = null)
        {
            // Aktif rezervasyonu olan oda ID'leri
            var occupiedRoomIds = await _context.Reservations
                .Where(r => r.Status != ReservationStatus.Completed)
                .Select(r => r.RoomId)
                .Distinct()
                .ToListAsync();

            // Sadece boş odalar
            var rooms = await _context.Rooms
                .Where(r => !occupiedRoomIds.Contains(r.Id) || r.Id == selectedRoomId)
                .OrderBy(r => r.RoomNumber)
                .ToListAsync();

            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", selectedRoomId);
        }

        public async Task<IActionResult> Payment(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            // Sadece CheckOut durumunda ödeme alınsın
            if (reservation.Status != ReservationStatus.CheckOut)
                return RedirectToAction(nameof(Index));

            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(int id, decimal totalPrice, PaymentMethod paymentMethod)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            if (reservation.Status != ReservationStatus.CheckOut)
                return RedirectToAction(nameof(Index));

            if (totalPrice <= 0)
            {
                ModelState.AddModelError("", "Toplam tutar 0'dan büyük olmalıdır.");
                return View(reservation);
            }

            reservation.TotalPrice = totalPrice;
            reservation.PaymentMethod = paymentMethod;
            reservation.PaymentStatus = PaymentStatus.Paid;
            reservation.PaidAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

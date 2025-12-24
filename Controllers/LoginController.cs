using Microsoft.AspNetCore.Mvc;
using OtelYonetimMVC.Data;

namespace OtelYonetimMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HotelDbContext _context;

        public LoginController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ViewBag.Error = "Kullanıcı adı zorunludur.";
                return View();
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı";
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // 🔐 ROLE GÖRE YÖNLENDİRME (TEK KRİTİK DEĞİŞİKLİK)
            if (user.Role == "Yonetici")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Resepsiyon direkt iş ekranına gitsin
                return RedirectToAction("Index", "Reservations");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

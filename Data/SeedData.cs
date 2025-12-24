using OtelYonetimMVC.Models;

namespace OtelYonetimMVC.Data
{
    public static class SeedData
    {
        public static void Initialize(HotelDbContext context)
        {
            // Kullanıcı yoksa ekle
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "admin", Role = "Yonetici" },
                    new User { Username = "resepsiyon", Role = "Resepsiyon" }
                );

                context.SaveChanges();
            }
        }
    }
}

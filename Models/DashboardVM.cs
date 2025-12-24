namespace OtelYonetimMVC.Models
{
    public class DashboardVM
    {
        public int TotalRooms { get; set; }
        public int OccupiedRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int CleaningRooms { get; set; }

        public List<string> SystemActivities { get; set; } = new();
    }
}

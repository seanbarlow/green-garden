namespace green_garden_server.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Device Device { get; set; }
        public int OnSeconds { get; set; }
        public int OffSeconds { get; set; }
        public Frequency Frequency { get; set; }

    }
}

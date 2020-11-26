namespace green_garden_server.Models
{
    public class Schedule : BaseModel
    { 
        public int DevcieId { get; set; }
        public Device Device { get; set; }
        public int OnSeconds { get; set; }
        public int OffSeconds { get; set; }
        public int FrequencyId { get; set; }
        public Lookup Frequency { get; set; }

    }
}

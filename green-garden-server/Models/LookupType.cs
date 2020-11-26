using System.Collections.Generic;

namespace green_garden_server.Models
{
    public class LookupType : BaseModel
    {
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Lookup> Lookups { get; set; }
    }
}



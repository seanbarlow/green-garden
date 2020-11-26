using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models
{
    public class Lookup : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LookupTypeId { get; set; }
        public LookupType LookupType { get; set; }
    }
}

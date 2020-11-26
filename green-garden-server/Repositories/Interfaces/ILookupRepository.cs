using green_garden_server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface ILookupRepository : IBaseRepository
    {
        public Task<Lookup> GetLookupAsync(string lookupTypeUniqueId, string lookupUniqueId);
    }
}

using green_garden_server.Data;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Repositories
{
    public class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(GreenGardenContext context) : base(context)
        {
        }

        public async Task<Lookup> GetLookupAsync(string lookupTypeUniqueId, string lookupUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId.ToLower().Trim();
            lookupUniqueId = lookupUniqueId.ToLower().Trim();

            return await _context.Lookups
                .Include(x => x.LookupType)
                .SingleAsync(x => x.LookupType.UniqueId == lookupTypeUniqueId && x.Name == lookupUniqueId);
        }
    }
}

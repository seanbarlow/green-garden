using green_garden_server.Data;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories
{
    public class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(GreenGardenContext context) : base(context) { }

        private async Task<int> GetNextIdAsync(int lookupTypeId)
        {
            var lookup = await _context.Lookups
                .OrderByDescending(x => x.Id)
                .FirstAsync(x => x.LookupTypeId == lookupTypeId);
            return lookup.Id + 1;
        }

        private async Task<int> GetNextLookupTypeIdAsync()
        {
            var lookupType = await _context.LookupTypes
                .OrderByDescending(x => x.Id)
                .FirstAsync();
            return lookupType.Id + 100;
        }

        public async Task<Lookup> GetAsync(string lookupTypeUniqueId, string lookupUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .ToLower()
                .Trim()
                .Replace(" ", "");
            lookupUniqueId = lookupUniqueId
                .ToLower()
                .Trim()
                .Replace(" ", "");

            return await _context.Lookups
                .Include(x => x.LookupType)
                .SingleAsync(x => x.LookupType.UniqueId == lookupTypeUniqueId && x.UniqueId == lookupUniqueId);
        }

        public async Task<Lookup> GetAsync(int id)
        {
            return await _context.Lookups
                .SingleAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Lookup>> GetAllAsync(int id)
        {
            return await _context.Lookups
                .Where(x => x.LookupTypeId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lookup>> GetAllAsync(string lookupTypeUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            return await _context.Lookups
                .Where(x => x.LookupType.UniqueId == lookupTypeUniqueId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LookupType>> GetAllTypesAsync()
        {
            return await _context.LookupTypes
                .ToListAsync();
        }

        public Task<LookupType> GetTypeAsync(string lookupTypeUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            throw new NotImplementedException();
        }

        public async Task<LookupType> GetTypeAsync(int id)
        {
            return await _context.LookupTypes
                .SingleAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(LookupType lookupType)
        {
            var lookupTypeTracked = await GetAsync(lookupType.Id);
            lookupTypeTracked.Name = lookupType.Name;
            lookupTypeTracked.Description = lookupType.Description;
            lookupTypeTracked.Updated = DateTime.UtcNow;
        }

        public async Task AddAsync(int lookupTypeId, Lookup lookup)
        {
            lookup.Id = await GetNextIdAsync(lookupTypeId);
            lookup.Created = DateTime.UtcNow;
            lookup.Updated = DateTime.UtcNow;
            await _context.Lookups.AddAsync(lookup);
        }

        public async Task DeleteAsync(int lookupTypeId, int lookupId)
        {
            var lookup = await _context.Lookups.SingleAsync(x => x.LookupTypeId == lookupTypeId && x.Id == lookupId);
            lookup.Deleted = true;
            lookup.Updated = DateTime.UtcNow;
        }

        public async Task UpdateAsync(int lookupTypeId, Lookup lookup)
        {
            var lookupTracked = await _context.Lookups.SingleAsync(x => x.LookupTypeId == lookupTypeId && x.Id == lookup.Id);
            lookupTracked.Name = lookup.Name;
            lookupTracked.Description = lookup.Description;
            lookupTracked.Updated = DateTime.UtcNow;
        }

        public async Task AddAsync(LookupType lookupType)
        {
            lookupType.Id = await GetNextLookupTypeIdAsync();
            lookupType.Created = DateTime.UtcNow;
            lookupType.Updated = DateTime.UtcNow;
            await _context.LookupTypes.AddAsync(lookupType);
        }

        public async Task DeleteAsync(int lookupTypeId)
        {
            var lookupType = await _context.LookupTypes.SingleAsync(x => x.Id == lookupTypeId);
            lookupType.Deleted = true;
            lookupType.Updated = DateTime.UtcNow;
        }
    }
}

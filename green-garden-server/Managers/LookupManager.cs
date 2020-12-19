using green_garden_server.Managers.Interfaces;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Managers
{
    public class LookupManager : ILookupManager
    {
        private readonly ILookupRepository _lookupRepository;

        public LookupManager(ILookupRepository lookupRepository)
        {
            this._lookupRepository = lookupRepository;
        }

        public async Task<IEnumerable<LookupType>> GetTypesAsync()
        {
            return await _lookupRepository
                .GetAllTypesAsync();
        }

        public async Task<LookupType> GetTypeAsync(string uniqueId)
        {
            return await _lookupRepository
                .GetTypeAsync(uniqueId);
        }

        public async Task<LookupType> GetTypeAsync(int id)
        {
            return await _lookupRepository
                .GetTypeAsync(id);
        }

        public async Task<Lookup> GetAsync(int id)
        {
            return await _lookupRepository
                .GetAsync(id);
        }

        public async Task<Lookup> GetAsync(string lookupTypeUniqueId, string lookupUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            lookupUniqueId = lookupUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            return await _lookupRepository
                .GetAsync(lookupTypeUniqueId, lookupUniqueId);
        }
        public async Task<IEnumerable<Lookup>> GetAllAsync(string lookupTypeUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            return await _lookupRepository
                .GetAllAsync(lookupTypeUniqueId);
        }

        public async Task<IEnumerable<Lookup>> GetAllAsync(int lookupTypeId)
        {
            return await _lookupRepository
                .GetAllAsync(lookupTypeId);
        }

        public async Task UpdateAsync(LookupType lookupType)
        {
            await _lookupRepository.UpdateAsync(lookupType);
            await _lookupRepository.SaveAsync();
        }

        public async Task AddAsync(LookupType lookupType)
        {
            await _lookupRepository.AddAsync(lookupType);
        }

        public async Task DeleteAsync(int lookupTypeId)
        {
            await _lookupRepository.DeleteAsync(lookupTypeId);
            await _lookupRepository.SaveAsync();
        }

        public async Task UpdateAsync(int lookupTypeId, Lookup lookup)
        {
            await _lookupRepository.UpdateAsync(lookupTypeId, lookup);
            await _lookupRepository.SaveAsync();
        }

        public async Task AddAsync(int lookupTypeId, Lookup lookup)
        {
            await _lookupRepository.AddAsync(lookupTypeId, lookup);
            await _lookupRepository.SaveAsync();
        }

        public async Task DeleteAsync(int lookupTypeId, int lookupId)
        {
            await _lookupRepository.DeleteAsync(lookupTypeId, lookupId);
            await _lookupRepository.SaveAsync();
        }
    }
}

using green_garden_server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface ILookupRepository : IBaseRepository
    {
        Task<Lookup> GetAsync(string lookupTypeUniqueId, string lookupUniqueId);
        Task<Lookup> GetAsync(int id);
        Task<IEnumerable<Lookup>> GetAllAsync(int lookupId);
        Task<IEnumerable<Lookup>> GetAllAsync(string lookupTypeUniqueId);
        Task<IEnumerable<LookupType>> GetAllTypesAsync();
        Task<LookupType> GetTypeAsync(string lookupTypeUniqueId);
        Task<LookupType> GetTypeAsync(int lookupTypeId);
        Task AddAsync(LookupType lookupType);
        Task AddAsync(int lookupTypeId, Lookup lookup);
        Task DeleteAsync(int lookupTypeId);
        Task DeleteAsync(int lookupTypeId, int lookupId);
        Task UpdateAsync(LookupType lookupType);
        Task UpdateAsync(int lookupTypeId, Lookup lookup);
    }
}

using green_garden_server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Managers.Interfaces
{
    public interface ILookupManager
    {
        Task<IEnumerable<Lookup>> GetAllAsync(int lookupTypeId);
        Task<IEnumerable<Lookup>> GetAllAsync(string lookupTypeUniqueId);
        Task<Lookup> GetAsync(int lookupId);
        Task<Lookup> GetAsync(string lookupTypeUniqueId, string lookupUniqueId);
        Task<LookupType> GetTypeAsync(int lookupTypeId);
        Task<LookupType> GetTypeAsync(string lookupTypeUniqueId);
        Task<IEnumerable<LookupType>> GetTypesAsync();
        Task UpdateAsync(LookupType lookupType);
        Task UpdateAsync(int lookupTypeId, Lookup lookup);
        Task AddAsync(int lookupTypeId, Lookup lookup);
        Task AddAsync(LookupType lookupType);
        Task DeleteAsync(int lookupTypeId);
        Task DeleteAsync(int lookupTypeId, int lookupId);
    }
}
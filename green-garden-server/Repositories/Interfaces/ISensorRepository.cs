using green_garden_server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface ISensorRepository : IBaseRepository
    {
        Task<IEnumerable<Sensor>> GetAllAsync(int deviceId);
        Task<Sensor> GetAsync(int deviceId, int id);
        Task AddAsync(Sensor sensor);
        Task UpdateAsync(Sensor sensor);
        Task DeleteAsync(int deviceId, int sensorId);
    }
}
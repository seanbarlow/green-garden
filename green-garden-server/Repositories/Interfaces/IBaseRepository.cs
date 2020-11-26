using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task SaveAsync();
    }
}
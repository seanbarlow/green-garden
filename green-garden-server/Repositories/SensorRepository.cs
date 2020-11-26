using green_garden_server.Data;
using green_garden_server.Repositories.Interfaces;

namespace green_garden_server.Repositories
{
    public class SensorRepository : BaseRepository, ISensorRepository
    {
        public SensorRepository(GreenGardenContext context) : base(context)
        {
        }
    }
}

using green_garden_server.Data;
using green_garden_server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly GreenGardenContext _context;

        public BaseRepository(GreenGardenContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

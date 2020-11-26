using green_garden_server.Data;
using green_garden_server.Events;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories
{
    public class CommandRepository: BaseRepository, ICommandsRepository
    {
        public CommandRepository(GreenGardenContext context) : base(context)
        {
        }

        public Command Add(Command entity)
        {
            return _context.Commands.Add(entity).Entity;
        }

        public async Task<Command> FindAsync(int id)
        {
            return await _context.Commands
                .Include(p => p.ActionType)
                .Include(p => p.SensorType)
                .FirstAsync(p => p.Id == id);
        }

        public async Task MarkSent(int id)
        {
            var command = await FindAsync(id);
            command.Sent = true;
        }

        public async Task DeleteAsync(int id)
        {
            var command = await FindAsync(id);
            command.Deleted = true;
            command.Updated = DateTime.Now;
        }
    }
}

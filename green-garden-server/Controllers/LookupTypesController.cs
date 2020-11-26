using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using green_garden_server.Data;
using green_garden_server.Models;

namespace green_garden_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupTypesController : ControllerBase
    {
        private readonly GreenGardenContext _context;

        public LookupTypesController(GreenGardenContext context)
        {
            _context = context;
        }

        // GET: api/LookupTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupType>>> GetLookupTypes()
        {
            return await _context.LookupTypes.ToListAsync();
        }
        // GET: api/LookupTypes/DeviceType/Lookups
        [HttpGet("{lookupTypeUniqueId}/Lookups")]
        public async Task<ActionResult<IEnumerable<Lookup>>> GetLookups(string lookupTypeUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            return await _context.Lookups
                .Where(x => x.LookupType.UniqueId == lookupTypeUniqueId)
                .ToListAsync();
        }


        // GET: api/LookupTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupType>> GetLookupType(int id)
        {
            var lookupType = await _context.LookupTypes.FindAsync(id);

            if (lookupType == null)
            {
                return NotFound();
            }

            return lookupType;
        }

        // PUT: api/LookupTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookupType(int id, LookupType lookupType)
        {
            if (id != lookupType.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookupType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LookupTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LookupType>> PostLookupType(LookupType lookupType)
        {
            lookupType.UniqueId = lookupType.UniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            _context.LookupTypes.Add(lookupType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookupType", new { id = lookupType.Id }, lookupType);
        }

        // DELETE: api/LookupTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupType(int id)
        {
            var lookupType = await _context.LookupTypes.FindAsync(id);
            if (lookupType == null)
            {
                return NotFound();
            }

            _context.LookupTypes.Remove(lookupType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupTypeExists(int id)
        {
            return _context.LookupTypes.Any(e => e.Id == id);
        }
    }
}

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
    public class LookupController : ControllerBase
    {
        private readonly GreenGardenContext _context;

        public LookupController(GreenGardenContext context)
        {
            _context = context;
        }

        // GET: api/Lookups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lookup>> GetLookup(int id)
        {
            var lookup = await _context.Lookups.FindAsync(id);

            if (lookup == null)
            {
                return NotFound();
            }

            return lookup;
        }

        // PUT: api/Lookups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookup(int id, Lookup lookup)
        {
            if (id != lookup.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupExists(id))
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

        // POST: api/Lookups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lookup>> PostLookup(Lookup lookup)
        {
            _context.Lookups.Add(lookup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookup", new { id = lookup.Id }, lookup);
        }

        // DELETE: api/Lookups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookup(int id)
        {
            var lookup = await _context.Lookups.FindAsync(id);
            if (lookup == null)
            {
                return NotFound();
            }

            _context.Lookups.Remove(lookup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupExists(int id)
        {
            return _context.Lookups.Any(e => e.Id == id);
        }
    }
}

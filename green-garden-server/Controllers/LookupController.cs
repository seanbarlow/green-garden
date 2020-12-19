using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using green_garden_server.Models;
using green_garden_server.Managers.Interfaces;

namespace green_garden_server.Controllers
{
    [Route("api/LookupType/{lookupTypeId}/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupManager _lookupManager;

        public LookupController(ILookupManager lookupManager)
        {
            _lookupManager = lookupManager;
        }

        // GET: api/Lookups/5
        [HttpGet("{lookupId}")]
        public async Task<ActionResult<Lookup>> GetLookup(int lookupTypeId, int lookupId)
        {
            var lookup = await _lookupManager.GetAsync(lookupId);

            if (lookup == null)
            {
                return NotFound();
            }

            return lookup;
        }

        // PUT: api/Lookups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{lookupId}")]
        public async Task<IActionResult> PutLookup(int lookupTypeId, int lookupId, Lookup lookup)
        {
            if (lookupId != lookup.Id)
            {
                return BadRequest();
            }
            try
            {
                await _lookupManager.UpdateAsync(lookupTypeId, lookup);
            }
            catch (DbUpdateConcurrencyException)
            {

                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/Lookups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lookup>> PostLookup(int lookupTypeId, Lookup lookup)
        {
            await _lookupManager.AddAsync(lookupTypeId, lookup);
            
            return CreatedAtAction("GetLookup", new { id = lookup.Id }, lookup);
        }

        // DELETE: api/Lookups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookup(int lookupTypeId, int lookupId)
        {
            await _lookupManager.DeleteAsync(lookupTypeId, lookupId);

            return NoContent();
        }
    }
}

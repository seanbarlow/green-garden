using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using green_garden_server.Data;
using green_garden_server.Models;
using green_garden_server.Managers.Interfaces;

namespace green_garden_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupTypesController : ControllerBase
    {
        private readonly ILookupManager _lookupManager;

        public LookupTypesController(ILookupManager lookupManager)
        {
            _lookupManager = lookupManager;
        }

        // GET: api/LookupTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupType>>> GetLookupTypes()
        {
            return (await _lookupManager.GetTypesAsync()).ToList();
        }
        // GET: api/LookupTypes/DeviceType/Lookups
        [HttpGet("{lookupTypeUniqueId}/Lookups")]
        public async Task<ActionResult<IEnumerable<Lookup>>> GetLookupsAsync(string lookupTypeUniqueId)
        {
            lookupTypeUniqueId = lookupTypeUniqueId
                .Trim()
                .ToLower()
                .Replace(" ", "");
            return (await _lookupManager.GetAllAsync(lookupTypeUniqueId)).ToList();
        }


        // GET: api/LookupTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupType>> GetLookupType(int id)
        {
            var lookupType = await _lookupManager.GetTypeAsync(id);

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

            try
            {
                await _lookupManager.UpdateAsync(lookupType);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
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
            await _lookupManager.AddAsync(lookupType);

            return CreatedAtAction("PostLookupType", new { id = lookupType.Id }, lookupType);
        }

        // DELETE: api/LookupTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupType(int id)
        {
            await _lookupManager.DeleteAsync(id);

            return NoContent();
        }
    }
}

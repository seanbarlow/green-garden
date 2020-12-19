using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using green_garden_server.Data;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;

namespace green_garden_server.Controllers
{
    [Route("api/Devices/{deviceId}/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorRepository _sensorsRepository;

        public SensorsController(ISensorRepository sensorsRepository)
        {
            _sensorsRepository = sensorsRepository;
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorsAsync(int deviceId)
        {
            var sensors = await this._sensorsRepository.GetAllAsync(deviceId);
            return sensors.ToList();
        }

        // GET: api/Sensors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensorAsync(int deviceId, int id)
        {
            var sensor = await _sensorsRepository.GetAsync(deviceId, id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        // PUT: api/Sensors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorAsync(int deviceId, int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }
            try
            {
                await _sensorsRepository.UpdateAsync(sensor);
                await _sensorsRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Sensors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensorAsync(int deviceId, Sensor sensor)
        {
            await _sensorsRepository.AddAsync(sensor);
            await _sensorsRepository.SaveAsync();
            return CreatedAtAction("GetSensor", new { id = sensor.Id, deviceId = sensor.DeviceId }, sensor);
        }

        // DELETE: api/Sensors/5
        [HttpDelete("{sensorId}")]
        public async Task<IActionResult> DeleteSensorAsync(int deviceId, int sensorId)
        {
            await _sensorsRepository.DeleteAsync(deviceId, sensorId);
            await _sensorsRepository.SaveAsync();

            return NoContent();
        }
    }
}

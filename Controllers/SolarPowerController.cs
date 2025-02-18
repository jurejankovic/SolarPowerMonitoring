using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarMonitoringAPI.Data;
using SolarPowerMonitoringApi.Data.DTOs;
using SolarPowerMonitoringApi.Data.Models;

namespace SolarPowerMonitoringApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolarPowerController : ControllerBase
    {

        private readonly SolarMonitoringDbContext _context;
        private readonly ILogger<SolarPowerController> _logger;


        public SolarPowerController(SolarMonitoringDbContext context, ILogger<SolarPowerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants()
        {
            var plants = await _context.SolarPowerPlants.ToListAsync();
            return Ok(plants);
        }


        [HttpGet("paged/{pageSize}")]
        public async Task<IActionResult> GetAllPlantsPaged(int pageSize)
        {
            var plants = await _context.SolarPowerPlants.Take(pageSize).ToListAsync();
            return Ok(plants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlant(int id)
        {
            var plant = await _context.SolarPowerPlants
                .Include(p => p.ProductionRecords)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlant([FromBody] SolarPowerPlantDto newPlantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SolarPowerPlant newPlant = MapToSolarPowerPlant(newPlantDto);

            _context.SolarPowerPlants.Add(newPlant);
            await _context.SaveChangesAsync();
            return Ok(newPlant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] SolarPowerPlantDto updatedPlant)
        {
            if (id != updatedPlant.Id) 
            { 
                return BadRequest("ID mismatch."); 
            }

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            var plant = await _context.SolarPowerPlants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            plant.Name = updatedPlant.Name;
            plant.InstalledPower = updatedPlant.InstalledPower;
            plant.DateOfInstallation = updatedPlant.DateOfInstallation;
            plant.Latitude = updatedPlant.Latitude;
            plant.Longitude = updatedPlant.Longitude;

            await _context.SaveChangesAsync();
            return Ok(plant);
        }

        [HttpPut("{plantId}/productionrecords/{productionRecordId}")]
        public async Task<IActionResult> UpdatePlantProductionRecords(int plantId, int productionRecordId, [FromBody] ProductionRecordDto updatedRecord)
        {
            var plant = await _context.SolarPowerPlants
                .Include(p => p.ProductionRecords)
                .FirstOrDefaultAsync(p => p.Id == plantId);
            
            if (plant == null)
            {
                return NotFound();
            }

            var existingRecord = plant.ProductionRecords.FirstOrDefault(r => r.Id == productionRecordId);
            
            if (existingRecord == null)
            {
                return NotFound();
            }

            existingRecord.Timestamp = updatedRecord.Timestamp;
            existingRecord.Value = updatedRecord.Value;
            existingRecord.IsForecast = updatedRecord.IsForecast;

            await _context.SaveChangesAsync();
            return Ok(existingRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _context.SolarPowerPlants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.SolarPowerPlants.Remove(plant);
            await _context.SaveChangesAsync();
            return Ok($"Solar power plant with ID {id} was deleted successfully.");
        }

        private static SolarPowerPlant MapToSolarPowerPlant(SolarPowerPlantDto newPlantDto)
        {
            return new SolarPowerPlant
            {
                Name = newPlantDto.Name,
                InstalledPower = newPlantDto.InstalledPower,
                DateOfInstallation = newPlantDto.DateOfInstallation,
                Latitude = newPlantDto.Latitude,
                Longitude = newPlantDto.Longitude
            };
        }
    }
}

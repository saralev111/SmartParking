using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartParking.Core.DTOs;
using SmartParking.Core.Entities;
using SmartParking.Core.Services;
using SmartParking.Entities;
using SmartParking.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartParking.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly ISpotService _spotService;
        private readonly IMapper _mapper;

        public SpotsController(ISpotService spotService, IMapper mapper)
        {
            _spotService = spotService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SpotDTO>> Get()
        {
            var spots = await _spotService.GetAllAsync();
            return _mapper.Map<List<SpotDTO>>(spots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var spot = await _spotService.GetByIdAsync(id);
            if (spot == null) return NotFound();
            return Ok(_mapper.Map<SpotDTO>(spot));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SpotPostModel value)
        {
            var spotToAdd = new Spot
            {
                Spot_number = value.Spot_number,
                ParkingId = value.ParkingId,
                Is_occupied = false, 
            };

            var s = await _spotService.AddAsync(spotToAdd);
            return Ok(_mapper.Map<SpotDTO>(s));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SpotPutModel value)
        {
            var existingSpot = await _spotService.GetByIdAsync(id);
            if (existingSpot == null) return NotFound();

            existingSpot.Is_occupied = value.Is_occupied;

            var updatedSpot = await _spotService.UpdateAsync(id, existingSpot);
            return Ok(_mapper.Map<SpotDTO>(updatedSpot));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var spot = await _spotService.GetByIdAsync(id);
            if (spot == null) return NotFound();

            await _spotService.DeleteAsync(id); 
            return NoContent();
        }
    }
}

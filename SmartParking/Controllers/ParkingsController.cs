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
    public class ParkingsController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        private readonly IMapper _mapper;

        public ParkingsController(IParkingService parkingService, IMapper mapper)
        {
            _parkingService = parkingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ParkingDTO>> Get()
        {
            var parkings = await _parkingService.GetAllAsync();
            return _mapper.Map<List<ParkingDTO>>(parkings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var parking = await _parkingService.GetByIdAsync(id);
            if (parking == null) return NotFound();
            return Ok(_mapper.Map<ParkingDTO>(parking));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParkingPostModel value)
        {
            var parkingToAdd = new Parking
            {
                Name = value.Name,
                Location = value.Location,
                Total_spots = value.Total_spots,
                Available_spots = value.Total_spots, 
                Price_per_hour = value.Price_per_hour
            };

            var p = await _parkingService.AddAsync(parkingToAdd);
            return Ok(_mapper.Map<ParkingDTO>(p));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParkingPutModel value)
        {
            var existingParking = await _parkingService.GetByIdAsync(id);
            if (existingParking == null) return NotFound();

            // עדכון שדות רלוונטיים
            if (!string.IsNullOrEmpty(value.Name)) existingParking.Name = value.Name;
            if (value.Price_per_hour > 0) existingParking.Price_per_hour = value.Price_per_hour;

            var updatedParking = await _parkingService.UpdateAsync(id, existingParking);
            return Ok(_mapper.Map<ParkingDTO>(updatedParking));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var parking = await _parkingService.GetByIdAsync(id);
            if (parking == null) return NotFound();

            await _parkingService.DeleteAsync(id);
            return NoContent();
        }
    }
}

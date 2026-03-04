using Microsoft.AspNetCore.Mvc;
using SmartParking.Core.Services;  // עבור ICarService
using SmartParking.Core.Entities;
using AutoMapper;
using SmartParking.Core.DTOs;
using SmartParking.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartParking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        // GET: api/Cars
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CarDTO>> Get()
        {
            var cars = await _carService.GetAllAsync();

            var carsDto = _mapper.Map<List<CarDTO>>(cars);

            return carsDto;
        }
        // GET api/Cars/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CarDTO>(car));
        }

        //POST api/Cars
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CarPostModel value)
        {
            var carToAdd = new Car
            {
                License_num = value.License_num,
                Owner_name = value.Owner_name,
            };
            //2. בדיקה אם יש רכב עם אותו מספר רישוי כבר קיים במערכת
            var existingCar = await _carService.GetByLicenseNumAsync(value.License_num);

            if (existingCar == null)
            {
                var s = await _carService.AddAsync(carToAdd, value.ParkingId);
                return Ok(_mapper.Map<CarDTO>(s));
            }

            return Conflict();
        }

        // PUT api/Cars/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CarPutModel value)
        {
            var existingCar = await _carService.GetByIdAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(value.Owner_name))
                existingCar.Owner_name = value.Owner_name;


            var updatedCar = await _carService.UpdateAsync(id, existingCar);
            return Ok(_mapper.Map<CarDTO>(updatedCar));
        }
        // DELETE api/Cars/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            // קריאה לפונקציה ומקבלים חזרה את המחיר
            double payment = await _carService.DeleteAsync(id);

            //  הודעה מסודרת עם המחיר
            return Ok(new
            {
                Message = "Car exited successfully",
                PaymentDue = payment.ToString("F2") + " NIS"
            });
        }
    }

}
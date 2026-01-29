using Microsoft.AspNetCore.Mvc;
using SmartParking.Core.Services;  // עבור ICarService
using SmartParking.Core.Entities;
using AutoMapper;
using SmartParking.Core.DTOs;
using SmartParking.Models;

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
        [HttpGet]
        public async Task<IEnumerable<CarDTO>> Get() 
        {
            var cars = await _carService.GetAllAsync();

            var carsDto = _mapper.Map<List<CarDTO>>(cars);

            return carsDto;
        }
        // GET api/Cars/5
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

            // 2. בדיקה אם הרכב כבר קיים (לפי מספר רישוי למשל)
            // , אם לא - ניתן לחפש לפי ID
            var existingCar = await _carService.GetByLicenseNumAsync(value.License_num);

            if (existingCar == null)
            {
                var s = await _carService.AddAsync(carToAdd);
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
            //חישוב עלות החנייה, צריך גם לחשב  לעדכן את זמן היציאה
            //existingCar.Total_payment = CalculatePayment(existingCar.Entry_time, existingCar.Exit_time.Value);

            var updatedCar = await _carService.UpdateAsync(id, existingCar);
            return Ok(_mapper.Map<CarDTO>(updatedCar));
        }
    }
    }
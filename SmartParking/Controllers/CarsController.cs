using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartParking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        public static List<Car> cars = new List<Car> { new Car { Id = 1, License_num = "123456789", Owner_name = "hadas", Entry_time = new DateTime(), Exit_time = new DateTime(), Total_payment = 200 } };
        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return cars;
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var c = cars.Find(e => e.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            return Ok(c);
        }
            

        // POST api/<CarController>
        [HttpPost]
        public ActionResult Post([FromBody] Car value)
        {
            var car=cars.Find(c=> c.Id == value.Id);
            if (car == null)
            {
                cars.Add(value);
                return Ok(value);
            }
            return Conflict();
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Car value)
        {
            var car = cars.Find(c => c.Id == value.Id);
            if(car == null)
            {
                return BadRequest();
            }
            else
            {
                car.License_num = value.License_num;
                car.Owner_name = value.Owner_name;
                car.Entry_time = value.Entry_time;
                car.Exit_time = value.Exit_time;
                car.Total_payment = value.Total_payment;
                return Ok(car);

            }
       
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var car = cars.Find(e => e.Id == id);
            if (car == null)
                return BadRequest();
            else
            {
                cars.Remove(car);
                return Ok();

            }
        }
    }
}
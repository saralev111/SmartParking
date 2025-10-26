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
        public Car Get(int id)
        {
            return cars.Find(e => e.Id == id);
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] Car value)
        {
            cars.Add(value);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car value)
        {
            var index = cars.FindIndex(e => e.Id == id);
            cars[index].License_num = value.License_num;
            cars[index].Owner_name = value.Owner_name;
            cars[index].Entry_time = value.Entry_time;
            cars[index].Exit_time = value.Exit_time;
            cars[index].Total_payment = value.Total_payment;
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelite = cars.Find(e => e.Id == id);
            cars.Remove(toDelite);
        }
    }
}
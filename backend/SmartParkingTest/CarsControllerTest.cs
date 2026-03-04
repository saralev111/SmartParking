using Microsoft.AspNetCore.Mvc;
using SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartParking.Core.Entities;

namespace SmartParkingTest
{
    public class CarsControllerTest
    {
        private FakeContext fakeContext = new FakeContext();

        [Fact]
        public void Get_ReturnList()
        {
            var controller =new CarsController(fakeContext);
            var result = controller.Get();

            Assert.IsType<List<Car>> (result);
        }
        [Fact]
        public void Get_ReturnByIdOk()
        {
            var id = 1;
            var controller = new CarsController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void GetById_returnNotFound()
        {
            var id = 2;
            var controller = new CarsController(fakeContext);
            var result = controller.Get(id);
            Assert.IsType<NotFoundResult>(result);

        }
    }
    
  
    
}

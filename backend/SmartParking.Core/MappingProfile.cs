using AutoMapper;
using SmartParking.Core.DTOs;
using SmartParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Spot, SpotDTO>().ReverseMap();
            CreateMap<Parking, ParkingDTO>().ReverseMap();


        }
    }
}

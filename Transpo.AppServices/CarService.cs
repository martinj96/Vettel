using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.AppServices
{
    public class CarService
    {
        private ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public Car AddCar(CarDto c)
        {
            var car = new Car();
            car.Brand = c.Brand;
            car.Color = c.Color;
            car.Comfort = c.Comfort;
            _carRepository.Add(car);
            _carRepository.Save();
            return car;
        }
        public void DeleteCar(int id)
        {
            var car = _carRepository.GetById(id);
            _carRepository.Delete(car);
            _carRepository.Save();
        }
        public Car GetById(int id)
        {
            return _carRepository.GetById(id);
        }
    }
}

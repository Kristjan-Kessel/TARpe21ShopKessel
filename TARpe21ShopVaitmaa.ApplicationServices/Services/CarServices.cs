using Microsoft.EntityFrameworkCore;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;

namespace TARpe21ShopVaitmaa.ApplicationServices.Services
{
    public class CarServices : ICarServices
    {
        private readonly TARpe21ShopVaitmaaContext _context;
        private readonly IFilesServices _filesServices;
        public CarServices
            (
            TARpe21ShopVaitmaaContext context,
            IFilesServices filesServices
            )
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<Car> Create(CarDto dto)
        {
            Car car = new();

            car.Id= dto.Id;
            car.Brand= dto.Brand;
            car.Year= dto.Year;
            car.IsUsed= dto.IsUsed;
            car.FilesToApi= dto.FilesToApi;
            car.CreatedAt= DateTime.Now;
            car.ModifiedAt= DateTime.Now;

            //_filesServices.FilesToApi(dto, car);


            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }
        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .Include(x => x.FilesToApi)
                .FirstOrDefaultAsync(x => x.Id == id);

            //to do: remove associated files/images

            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }
        public async Task<Car> Update(CarDto dto)
        {
            Car car = new Car();

            car.Id = dto.Id;
            car.Brand = dto.Brand;
            car.Year = dto.Year;
            car.IsUsed = dto.IsUsed;
            car.FilesToApi = dto.FilesToApi;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;
            //_filesServices.FilesToApi(dto, realEstate);

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}

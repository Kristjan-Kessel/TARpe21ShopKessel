using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.ApplicationServices.Services;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;
using TARpe21ShopVaitmaa.Models.Car;
using TARpe21ShopVaitmaa.Models.RealEstate;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class CarsController : Controller
    {
        private readonly TARpe21ShopVaitmaaContext _context;
        private readonly ICarServices _cars;

        public CarsController (TARpe21ShopVaitmaaContext context, ICarServices cars)
        {
            _context = context;
            _cars = cars;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderBy(x => x.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Year = x.Year,
                    IsUsed = x.IsUsed 
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = Guid.NewGuid(),
                Brand = vm.Brand,
                Model = vm.Model,
                Year = vm.Year,
                IsUsed = vm.IsUsed,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now

            };
            var result = await _cars.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.ApplicationServices.Services;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;
using TARpe21ShopVaitmaa.Models.Car;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class CarsController : Controller
    {
        private readonly TARpe21ShopVaitmaaContext _context;

        public CarsController (TARpe21ShopVaitmaaContext context)
        {
            _context = context;
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
    }
}

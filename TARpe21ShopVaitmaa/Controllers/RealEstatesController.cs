using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARpe21ShopVaitmaa.ApplicationServices.Services;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Core.Dto;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Data;
using TARpe21ShopVaitmaa.Models.RealEstate;
using TARpe21ShopVaitmaa.Models.Spaceship;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstateServices _realEstates;
        private readonly TARpe21ShopVaitmaaContext _context;

        public RealEstatesController(IRealEstateServices realEstateServices, TARpe21ShopVaitmaaContext context)
        {
            _realEstates = realEstateServices;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .OrderBy(x => x.CreatedAt)
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    County = x.County,
                    Country = x.Country,
                    SquareMeters = x.SquareMeters,
                    Price = x.Price,
                    isSold = x.isSold
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = Guid.NewGuid(),
                Address = vm.Address,
                City = vm.City, 
                County = vm.County,
                Country = vm.Country,
                SquareMeters = vm.SquareMeters,
                Price = vm.Price,
                PostalCode = vm.PostalCode,
                PhoneNumber = vm.PhoneNumber,
                FaxNumber = vm.FaxNumber,
                ListingDescription = vm.ListingDescription,
                BuildDate = vm.BuildDate,
                FloorCount = vm.FloorCount,
                EstateFloor = vm.EstateFloor,
                Bathrooms = vm.Bathrooms,
                Bedrooms = vm.Bedrooms,
                hasElectricity = vm.hasElectricity,
                hasParkingSpace = vm.hasParkingSpace,
                hasWater = vm.hasWater,
                Type = vm.Type,
                IsPropertyNewDevelopment = vm.IsPropertyNewDevelopment,
                isSold = vm.isSold,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FilesToApiDtos = vm.FileToApiViewModels
                .Select(z => new FileToApiDto
                {
                    Id = z.ImageId,
                    ExistingFilePath = z.FilePath,
                    RealEstateId = z.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstates.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.hasParkingSpace = realEstate.hasParkingSpace;
            vm.hasElectricity = realEstate.hasElectricity;
            vm.hasWater = realEstate.hasWater;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.isSold = realEstate.isSold;
            vm.CreatedAt = DateTime.Now;
            vm.ModifiedAt = DateTime.Now;

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = (Guid)vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                County = vm.County,
                SquareMeters = vm.SquareMeters,
                Price = vm.Price,
                PostalCode = vm.PostalCode,
                PhoneNumber = vm.PhoneNumber,
                FaxNumber = vm.FaxNumber,
                ListingDescription = vm.ListingDescription,
                BuildDate = vm.BuildDate,
                RoomCount = vm.RoomCount,
                FloorCount = vm.FloorCount,
                EstateFloor = vm.EstateFloor,
                Bathrooms = vm.Bathrooms,
                Bedrooms = vm.Bedrooms,
                hasParkingSpace = vm.hasParkingSpace,
                hasElectricity = vm.hasElectricity,
                hasWater = vm.hasWater,
                Type = vm.Type,
                IsPropertyNewDevelopment = vm.IsPropertyNewDevelopment,
                isSold = vm.isSold,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FilesToApiDtos = vm.FileToApiViewModels
                .Select(z => new FileToApiDto
                {
                    Id = z.ImageId,
                    ExistingFilePath = z.FilePath,
                    RealEstateId = z.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstates.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.hasParkingSpace = realEstate.hasParkingSpace;
            vm.hasElectricity = realEstate.hasElectricity;
            vm.hasWater = realEstate.hasWater;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.isSold = realEstate.isSold;

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.hasParkingSpace = realEstate.hasParkingSpace;
            vm.hasElectricity = realEstate.hasElectricity;
            vm.hasWater = realEstate.hasWater;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.isSold = realEstate.isSold;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }

    }
}

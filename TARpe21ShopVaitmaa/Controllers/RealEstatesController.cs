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
        private readonly IFilesServices _filesServices;

        public RealEstatesController(IRealEstateServices realEstateServices, TARpe21ShopVaitmaaContext context, IFilesServices filesServices)
        {
            _realEstates = realEstateServices;
            _context = context;
            _filesServices = filesServices;
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };
            var image = await _filesServices.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
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

            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

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
            vm.FileToApiViewModels.AddRange(images);

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

            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDeleteDetailsViewModel();

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
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
            vm.isDeleting = false;

            return View("DetailsDelete", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDeleteDetailsViewModel();

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
            vm.FileToApiViewModels.AddRange(images);
            vm.isDeleting = true;

            return View("DetailsDelete", vm);
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

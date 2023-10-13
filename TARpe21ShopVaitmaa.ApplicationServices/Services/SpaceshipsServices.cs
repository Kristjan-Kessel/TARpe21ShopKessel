using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Domain.Spaceship;
using TARpe21ShopVaitmaa.Data;

namespace TARpe21ShopVaitmaa.ApplicationServices.Services
{
    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly TARpe21ShopVaitmaaContext _context;
        public SpaceshipsServices(TARpe21ShopVaitmaaContext context) { 
            _context = context;
        }

        public async Task<Spaceship> Add(SpaceshipDto dto)
        {
            var domain = new Spaceship()
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                PassengerCount = dto.PassengerCount,
                Dimensions = dto.Dimensions,
                CrewCount = dto.CrewCount,
                CargoWeight = dto.CargoWeight,
                MaxSpeedInVaccuum = dto.MaxSpeedInVaccuum,
                BuiltAtDate = dto.BuiltAtDate,
                MaidenLaunch = dto.MaidenLaunch,
                Manufacturer = dto.Manufacturer,
                IsSpaceshipPreviouslyOwned = dto.IsSpaceshipPreviouslyOwned,
                FullTripsCount = dto.FullTripsCount,
                EnginePower = dto.EnginePower,
                FuelConsumptionPerDay = dto.FuelConsumptionPerDay,
                MaintenanceCount = dto.MaintenanceCount,
                LastMaintenance = dto.LastMaintenance,
            };

            await _context.spaceships.AddAsync(domain);
            await _context.SaveChangesAsync();
        }

    }
}

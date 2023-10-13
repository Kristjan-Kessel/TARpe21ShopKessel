using TARpe21ShopVaitmaa.Core.Domain.Spaceship;
using TARpe21ShopVaitmaa.Core.Dto;

namespace TARpe21ShopVaitmaa.ApplicationServices.Services
{
    public interface ISpaceshipsServices
    {

        Task<Spaceship> Add(SpaceshipDto dto);


    }
}

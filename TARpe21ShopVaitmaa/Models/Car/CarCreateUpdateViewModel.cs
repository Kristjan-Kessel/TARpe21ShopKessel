using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.Core.Domain;
using TARpe21ShopVaitmaa.Models.File;

namespace TARpe21ShopVaitmaa.Models.Car
{
    public class CarCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsUsed { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<FileToApiViewModel> FileToApiViewModels { get; set; } = new List<FileToApiViewModel>();

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

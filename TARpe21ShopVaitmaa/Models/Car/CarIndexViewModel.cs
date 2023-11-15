using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.Core.Domain;

namespace TARpe21ShopVaitmaa.Models.Car
{
    public class CarCreateUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsUsed { get; set; }

        //public IEnumerable<FileToApi> FilesToApi { get; set; } = new List<FileToApi>(); //files to be added to the api

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

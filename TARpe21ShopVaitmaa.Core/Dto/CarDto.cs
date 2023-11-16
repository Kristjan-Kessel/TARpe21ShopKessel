using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe21ShopVaitmaa.Core.Domain;

namespace TARpe21ShopVaitmaa.Core.Dto
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsUsed { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDto> FilesToApiDtos { get; set; } = new List<FileToApiDto>(); //files to be added to the api

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

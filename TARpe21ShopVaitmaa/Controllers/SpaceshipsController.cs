﻿using Microsoft.AspNetCore.Mvc;
using TARpe21ShopVaitmaa.Data;
using TARpe21ShopVaitmaa.Models;
using TARpe21ShopVaitmaa.Models.Spaceship;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly TARpe21ShopVaitmaaContext _context;

        public SpaceshipsController(TARpe21ShopVaitmaaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.spaceships
                .OrderBy(s => s.CreatedAt)
                .Select(s => new SpaceshipIndexViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                    PassengerCount = s.PassengerCount,
                    EnginePower = s.EnginePower,

                });
            return View(result);
        }

        public IActionResult Add()
        {
            return View("Edit");
        }

    }
}
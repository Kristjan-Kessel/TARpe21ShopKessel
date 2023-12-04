using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TARpe21ShopVaitmaa.Core.Dto.WeatherDtos;
using TARpe21ShopVaitmaa.Core.ServiceInterface;
using TARpe21ShopVaitmaa.Models.OpenWeather;

namespace TARpe21ShopVaitmaa.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IWeatherForecastsServices _openWeatherServices;
        public OpenWeatherController(IWeatherForecastsServices weatherForecastServices)
        {
            _openWeatherServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            OpenWeatherViewModel vm = new OpenWeatherViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult ShowWeather()
        {
            string city = Request.Form["City"];

            if (!string.IsNullOrEmpty(city))
            {
                TempData["City"] = city;
                return RedirectToAction("City");
            }

            return View();
        }

        [HttpGet]
        public IActionResult City()
        {
            OpenWeatherResultDto dto = new();

            if (TempData.TryGetValue("City", out object city))
            {
                dto.City = city.ToString();
                _openWeatherServices.OpenWeatherDetail(dto);

                OpenWeatherViewModel vm = new()
                {
                    City = dto.City,
                    Timezone = dto.Timezone,
                    Temperature = dto.Temperature,
                    Pressure = dto.Pressure,
                    Humidity = dto.Humidity,
                    Lon = dto.Lon,
                    Lat = dto.Lat,
                    Main = dto.Main,
                    Description = dto.Description,
                    Speed = dto.Speed
                };

                return View(vm);
            }

            // Handle the case where TempData["City"] is not available.
            return RedirectToAction("ShowWeather");
        }
    }
}

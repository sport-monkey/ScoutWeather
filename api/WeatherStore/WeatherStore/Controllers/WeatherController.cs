using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeatherStore.Models;

namespace WeatherStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ReadingContext _readingContext;

        private static Dictionary<string, int> _macs = new Dictionary<string, int>
        {
            { "0:11:22:33:44:55", 1 },
            { "f:ee:dd:cc:bb:aa", 7 }
        };

        public WeatherController(ReadingContext readingContext)
        {
            _readingContext = readingContext;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Data data)
        {
            if (!_macs.ContainsKey(data.PhysicalAddress))
            {
                return NotFound();
            }

            var reading = new Reading
            {
                Device = _macs[data.PhysicalAddress],
                Temperature = data.Temperature,
                Humidity = data.Humidity,
                Pressure = data.Pressure,
                Created = DateTime.UtcNow
            };

            _readingContext.Readings.Add(reading);
            _readingContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reading>> Get()
        {
            return _readingContext.Readings;
        }
    }
}
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace InfnetEcommerceContext.Notification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly IBus bus;

        public WeatherForecastController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return null;
        }
    }
}

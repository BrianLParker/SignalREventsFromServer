using Microsoft.AspNetCore.Mvc;
using SignalREventsFromServer.Server.Brokers.HubContexts;
using SignalREventsFromServer.Shared;

namespace SignalREventsFromServer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IHubContextBroker hubContext;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IHubContextBroker hubContext,
            ILogger<WeatherForecastController> logger)
        {
            this.hubContext = hubContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await this.hubContext.SendMessageToAllClients(sender: "Server Weather API Controller", message: "Get called!");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SignalREventsFromServer.Server.Services.Events;
using SignalREventsFromServer.Shared;

namespace SignalREventsFromServer.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IEventsService eventsService;
    private readonly ILogger<WeatherForecastController> _logger;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController(
        IEventsService eventsService,
        ILogger<WeatherForecastController> logger)
    {
        this.eventsService = eventsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await this.eventsService.SendMessageToGroupAsync(
            groupName: "APIMonitor",
            sender: "Server Weather API Controller",
            message: "Get called!");

        await this.eventsService.NotifyGetRequestAsync(controller: "WeatherForecastController");


        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

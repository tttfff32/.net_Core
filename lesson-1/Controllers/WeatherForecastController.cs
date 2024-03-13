using Microsoft.AspNetCore.Mvc;

namespace lesson_1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{id}", Name = "GetWeatherForecastById")]
    public IActionResult GetById(int id)
    {
        if (id < 0 || id >= Summaries.Length)
        {
            return BadRequest("Invalid ID");
        }

        var weatherForecast = new WeatherForecast
        {
            Date = DateTime.Now.AddDays(id),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[id]
        };

        return Ok(weatherForecast);
    }

}

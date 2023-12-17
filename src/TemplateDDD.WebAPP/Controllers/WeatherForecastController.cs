using Microsoft.AspNetCore.Mvc;

namespace TemplateDDD.APP.Controllers
{
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
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] WeatherForecast article)
        {
            try
            {
                if (string.IsNullOrEmpty(article.Summary) || article == null)
                    return BadRequest();

                var response = ""; // _repository.Get(id);

                if (response == null)
                    return NotFound();


                var ob = true;// _repository.Update(article);

                if (ob)
                    return Ok(ob);

            }
            catch (Exception ex)
            {
                // Log exception here...
                //_logger.WriteLine(ex.Message);
            }

            return Ok();
        }
    }
}
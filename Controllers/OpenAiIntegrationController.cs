using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProDecide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAiIntegrationController : ControllerBase
    {
      
        private readonly ILogger<OpenAiIntegrationController> _logger;

        public OpenAiIntegrationController(ILogger<OpenAiIntegrationController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<string> Post([FromBody] string query)
        {

            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var openaiService = new OpenAIService(apiKey);

            // Get response from OpenAI API
            var response = await openaiService.GetResponseAsync(query);

            return response;
        }
    }
}
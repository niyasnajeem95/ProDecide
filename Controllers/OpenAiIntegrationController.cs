using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ProDecide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAiIntegrationController : ControllerBase
    {
      
        private readonly ILogger<OpenAiIntegrationController> _logger;
        private readonly IConfiguration _configuration;

        public OpenAiIntegrationController(ILogger<OpenAiIntegrationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<string> Post([FromBody] string query)
        {

            string apiKey = _configuration.GetSection("API_KEY").Value; 
            var openaiService = new OpenAIService(apiKey);

            // Get response from OpenAI API
            var response = await openaiService.GetResponseAsync(query);

            return response;
        }
    }
}
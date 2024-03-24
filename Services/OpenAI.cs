
using Newtonsoft.Json;
using ProDecide.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAIService
{
    private readonly string _apiKey;

    public OpenAIService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        // Create HttpClient instance
        HttpClient httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        // API endpoint
        string requestUri = "https://api.openai.com/v1/chat/completions";

        // Example requestBody object
        var requestBody = new SearchModel
        {
            model = "gpt-3.5-turbo",
            messages = new List<ContentData>
            {
            new ContentData { role = "user", content = prompt }
            }
        };

        try
        {

            // Serialize the requestBody object to JSON
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody);

            // Create StringContent with JSON content
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Make the HTTP POST request
            var response = await httpClient.PostAsync(requestUri, content);

            // Check if the request was successful (status code 200)
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                ChatCompletion chatCompletion = JsonConvert.DeserializeObject<ChatCompletion>(responseContent);

                return chatCompletion.Choices[0].Message.Content;
            }
            else
            {
                Console.WriteLine($"Failed to make request. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Dispose the HttpClient instance to release resources
            httpClient.Dispose();
        }
        return null;
    }

}

using System.Text.Json;
using GithubUserActivity.Models;

namespace GithubUserActivity.Services;

public class GithubService
{
    private readonly HttpClient _httpClient;
    public GithubService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add(
            "User-Agent",
            "GithubActivityApp"
        );

    }
    public async Task<List<GithubEvent>> GetUserActivity(string username)
    {
        string url = $"https://api.github.com/users/{username}/events";
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Github User Not Found");
        }
       // Console.WriteLine(response.StatusCode);
        string json = await response.Content.ReadAsStringAsync();

        var activities = JsonSerializer.Deserialize<List<GithubEvent>>(
            json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return activities.Take(10).ToList() ?? new List<GithubEvent>();
    }
}
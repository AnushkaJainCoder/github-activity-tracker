using System.Text.Json.Serialization;

namespace GithubUserActivity.Models;

public class GithubEvent
{
    public string Type { get; set; } = string.Empty;
    public Repo Repo { get; set; } = new();

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}

public class Repo
{
    public string Name { get; set; } = string.Empty;
}
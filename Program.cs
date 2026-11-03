// // See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using GithubUserActivity.Models;
using GithubUserActivity.Services;

//Console.WriteLine($"Args Count: {args.Length}");

if(args.Length == 0)
{
    Console.WriteLine("Please provide valid username");
    return;
}

var service = new GithubService();
// var username = Console.ReadLine();
string username = args[0];
if (string.IsNullOrWhiteSpace(username))
{
    Console.WriteLine("Username Cannot be Empty");
    return;
}

try
{
    var activities =
        await service.GetUserActivity(username);

    foreach (var activity in activities)
    {
        Console.WriteLine(GetMessage(activity));
    }

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

static string GetMessage(GithubEvent activity)
{
    return activity.Type switch
    {
        "PushEvent" => $"Pushed to {activity.Repo.Name}  on {activity.CreatedAt:dd MMM yyyy}",
        "WatchEvent" => $"""Starred {activity.Repo.Name } on {activity.CreatedAt:dd MMM yyyy}""",
        "ForkEvent" => $"""Forked {activity.Repo.Name} on {activity.CreatedAt:dd MMM yyyy}""",
        "CreateEvent" => $"""Created {activity.Repo.Name} on {activity.CreatedAt:dd MMM yyyy}""",
        
        
        _ => $"{activity.Type} in {activity.Repo.Name}"
    };
}


// Console.WriteLine(json);
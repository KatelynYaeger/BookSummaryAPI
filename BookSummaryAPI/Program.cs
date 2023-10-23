using BookSummaryAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SummaryAPI;

var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

var googleServices = new ServiceCollection()
   .AddOptions()
   .Configure<GoogleBooksApi>(config.GetSection("GoogleBooksApi"))
   .BuildServiceProvider();

var googleSettings = googleServices.GetService<IOptions<GoogleBooksApi>>();

var googleKey = googleSettings.Value.ApiKey;

var nytServices = new ServiceCollection()
   .AddOptions()
   .Configure<NYTimesApi>(config.GetSection("NYTimesApi"))
   .BuildServiceProvider();

var nytSettings = nytServices.GetService<IOptions<NYTimesApi>>();

var nytKey = nytSettings.Value.ApiKey;

var client = new HttpClient();

Console.WriteLine("Welcome!");

bool cont = true;

while (cont)
{
    Console.WriteLine($"Which book would you like a summary for?");

    var userTitle = Console.ReadLine();

    var summaryUrl = $"https://api.nytimes.com/svc/books/v3/reviews.json?title={userTitle}&api-key={nytKey}";

    var summaryResponse = client.GetStringAsync(summaryUrl).Result;

    var summaryObject = JObject.Parse(summaryResponse);

    var summaryResult = summaryObject.ToObject(typeof(JResult)) as JResult;

    var googleURL = $"https://www.googleapis.com/books/v1/volumes?q={userTitle}&key={googleKey}";

    var googleResponse = client.GetStringAsync(googleURL).Result;

    var googleObject = JObject.Parse(googleResponse);

    var _result = googleObject.ToObject(typeof(GoogleResult)) as GoogleResult;

    string answer;

    Console.WriteLine("----");

    summaryResult.NYTMethod();

    if (summaryResult.num_results != 0 && summaryResult.num_results != null)
    {
        Console.WriteLine("Is this the book you're looking for?");

        answer = Console.ReadLine();

        if (answer.ToLower() == "no")
        {
            _result.GoogleMethod();
        }

    }
    else
    {
        Console.WriteLine("I'm sorry, but the New York Times doesn't have a summary for this book.");

        _result.GoogleMethod();

    }

    Console.WriteLine("Would you like to search for a different book? Yes or No");

    answer = Console.ReadLine();

    if (answer.ToLower() == "no")
    {
        cont = false;
    }

}

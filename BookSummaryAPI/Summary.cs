using System;
using Newtonsoft.Json.Linq;

namespace SummaryAPI
{
    public class Summary
    {
        private static GoogleResult? _result = null;

        private static void answerMethod()
        {
            Console.WriteLine("Let's check Google");

            Console.WriteLine();

            if (_result != null) _result.GoogleMethod();

            Console.WriteLine("Here are the results!");
        }


        public static void BookSummary()
        {

            var client = new HttpClient();

            var apiKey = "YourNYTimesAPIKey";   

            Console.WriteLine("Welcome!");

            bool cont = true;

            while (cont)
            {
                Console.WriteLine($"Which book would you like a summary for?");

                var userTitle = Console.ReadLine();

                var summaryUrl = $"https://api.nytimes.com/svc/books/v3/reviews.json?title={userTitle}&api-key={apiKey}";

                var summaryResponse = client.GetStringAsync(summaryUrl).Result;

                var summaryObject = JObject.Parse(summaryResponse);

                var summaryResult = summaryObject.ToObject(typeof(JResult)) as JResult;

                var googleAPIKey = "YourGoogleBooksAPIKey"; 

                var googleURL = $"https://www.googleapis.com/books/v1/volumes?q={userTitle}&key={googleAPIKey}";

                var googleResponse = client.GetStringAsync(googleURL).Result;

                var googleObject = JObject.Parse(googleResponse);

                _result = googleObject.ToObject(typeof(GoogleResult)) as GoogleResult;

                string answer;

                Console.WriteLine("----");

                summaryResult.NYTMethod();

                if (summaryResult.num_results != 0 && summaryResult.num_results != null)
                {
                    Console.WriteLine("Is this the book you're looking for?");

                    answer = Console.ReadLine();

                    if (answer.ToLower() == "no")
                    {
                        answerMethod();
                    }

                }
                else
                {
                    Console.WriteLine("I'm sorry, but the New York Times doesn't have a summary for this book.");

                    answerMethod();

                }

                Console.WriteLine("Would you like to search for a different book? Yes or No");

                answer = Console.ReadLine();

                if (answer.ToLower() == "no")
                {
                    cont = false;
                }

            }
        }
    }
}

using System;
namespace SummaryAPI
{
    public class JResult
    {
        public JResult()
        {
        }

        public string? status { get; set; }
        public int? num_results { get; set; }
        public string? copyright { get; set; }
        public Result[]? results { get; set; }


        public void NYTMethod()
        {
            foreach (var result in results)
            {
                Console.WriteLine($"{result.summary}");

                Console.WriteLine();
            }
        }
    }
    public class Result
    {
        public Result()
        {
        }
        public string? book_title { get; set; }
        public string? summary { get; set; }
    }


}

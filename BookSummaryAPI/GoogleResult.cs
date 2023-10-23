using System;

namespace SummaryAPI
{
    public class GoogleResult
    {
        public GoogleResult()
        {
        }

        public Items[]? items { get; set; }

        public void GoogleMethod()
        {
            try
            {
                Console.WriteLine("Let's check Google");

                Console.WriteLine();

                foreach (var item in items)
                    {
                        Console.WriteLine($"{item.volumeInfo.title}");

                        Console.WriteLine($"{item.volumeInfo.description}");

                        Console.WriteLine();
                    }

                Console.WriteLine("Here are the results!");
            }
            catch (Exception e)
            {
                Console.WriteLine("That doesn't look right...");
            }
        }
    }


    public class Items
    {
        public Items()
        {
        }

        public VolumeInfo volumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        public VolumeInfo()
        {
        }

        public string description { get; set; }
        public string? title { get; set; }

    }

}


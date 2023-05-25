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
            foreach (var item in items)
            {
                Console.WriteLine($"{item.volumeInfo.title}");

                Console.WriteLine($"{item.volumeInfo.description}");

                Console.WriteLine();
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


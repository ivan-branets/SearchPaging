using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchPaging
{
    class Program
    {
        static void Main()
        {
            var service = new Service();
            var data = new List<Entity>();

            const int maxResults = 5;

            for (var i = 0; i < 100000; i++)
            {
                data = MakeRequest(data, service, maxResults);
                Output(data);

                Console.ReadKey();
            }
        }

        private static void Output(List<Entity> data)
        {
            data.ForEach(Console.WriteLine);
            Console.WriteLine("\n{0}\n", data.Count);
        }

        private static List<Entity> MakeRequest(IList<Entity> data, Service service, int maxResults)
        {
            var lastValue = data.Any() ? data.Last().Value : 0;
            var lastId = data.Any() ? data.Last().Id : 0;

            return data.Union(service.GetData(lastValue, lastId, maxResults)).ToList();
        }
    }
}

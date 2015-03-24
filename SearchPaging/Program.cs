using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
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

    class Service
    {
        private const int ElementToDelete = 3;
        private const int RequestOnDelete = 2;

        private int RequestCount { get; set; }

        private static List<Entity> _list;
        private static List<Entity> DataContext
        {
            get
            {
                if (_list == null)
                {
                    var rand = new Random();
                    var currentId = 0;

                    _list = new List<Entity>();

                    for (var i = 0; i < 100; i++)
                    {
                        _list.Add(new Entity(++currentId, rand.Next(100)));
                    }                    
                }
                
                return _list;                
            }
        }

        public IEnumerable<Entity> GetData(int lastValue, int lastId, int count)
        {
            RequestCount++;

            if (RequestCount == RequestOnDelete)
            {
                DataContext.RemoveAt(ElementToDelete);
            }

            return DataContext
                .Where(d => d.Value > lastValue || (d.Value == lastValue && d.Id > lastId))
                .OrderBy(d => d.Value).ThenBy(d => d.Id)
                .Take(count);
        }
    }

    class Entity
    {
        public Entity(int id, int value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", Id, Value);
        }
    }
}

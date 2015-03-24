using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchPaging
{
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
}

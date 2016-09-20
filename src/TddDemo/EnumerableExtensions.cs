using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TddDemo
{
    static class EnumerableExtensions
    {
        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> items)
        {
            var list = new LinkedList<T>();
            foreach (var item in items)
            {
                list.Add(item);
            }

            return list;
        }
    }
}

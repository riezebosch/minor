using System;

namespace TddDemo
{
    class Stack<T>
    {
        private LinkedList<T> items = new LinkedList<T>();

        public void Put(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            T result = items[items.Count - 1];
            items.Remove(items.Count - 1);
            return result;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }
    }
}
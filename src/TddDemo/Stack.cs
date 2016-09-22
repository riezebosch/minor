using System;

namespace TddDemo
{
    class Stack<T>
    {
        private LinkedList<T> items = new LinkedList<T>();

        public void Put(T item)
        {
            items.Insert(0, item);
        }

        public T Pop()
        {
            T result = items[0];
            items.Remove(0);
            return result;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }
    }
}
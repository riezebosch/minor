using System;

namespace TddDemo
{
    class Queue<T>
    {
        private LinkedList<T> items = new LinkedList<T>();

        public void Enqueue(T input)
        {
            items.Add(input);
        }

        public T Dequeue()
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
using System.Collections.Generic;

namespace TddDemo
{
    interface ILinkedList<out T>
    {
        IEnumerator<T> GetEnumerator();

    }
}